using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly IUnitOfWork _uow;
        public CommentServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> AddComment(long currentUserId, CommentInput commentInput)
        {
            try
            {
                Comment parentComment = new Comment();
                if (commentInput.ParentHashCode != null)
                    parentComment = await GetByHashCode(commentInput.ParentHashCode);
                if (parentComment == null)
                    return -1;
                Lesson lesson = _uow.GetRepository<Lesson>()
                    .AsQueryable()
                    .FirstOrDefault(_ => _.HashCode == commentInput.LessonHashCode &&
                                    _.isDeleted == false);
                if (lesson == null)
                    return -1;
                var comment = new Comment
                {
                    Title = commentInput.Title,
                    Content = commentInput.Content,
                    LikeCount = 0,
                    ParentId = commentInput.ParentHashCode != null ? parentComment.Id : null,
                    LessonId = lesson.Id,
                    UserId = currentUserId,
                };
                _uow.GetRepository<Comment>().Add(comment);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> DeleteComment(string hashCode)
        {
            try
            {
                var comment = await GetByHashCode(hashCode);
                if (comment == null)
                    return -1;
                if (await IsCommentHaveChild(hashCode))
                    return 0;
                comment.isDeleted = true;
                _uow.GetRepository<Comment>().Update(comment);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<CommentOutput>> GetAllRow()
        {
            return await _uow.GetRepository<Comment>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false &&
                       (_.ParentComment.isDeleted == false || _.ParentComment == null) &&
                       _.Lesson.isDeleted == false &&
                       _.User.isDeleted == false)
                .Take(30)
                .Select(c => new CommentOutput
                {
                    HashCode = c.HashCode,
                    Title = c.Title,
                    Content = c.Content,
                    LikeCount = c.LikeCount,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Status = c.Status,
                    ParentHashCode = c.ParentComment == null ? null : c.ParentComment.HashCode,
                    LessonHashCode = c.Lesson.HashCode,
                    LessonName = c.Lesson.Name,
                    CreateBy = c.User.UserName
                })
                .ToListAsync();
        }

        public async Task<List<CommentOutput>> GetAllByParentHashCode(string parentHashCode)
        {
            Comment parentComment = await GetByHashCode(parentHashCode);
            if (parentComment == null)
                return null;
            return (await GetAllRow()).Where(_ => _.ParentHashCode == parentHashCode).ToList();
        }

        public async Task<List<CommentOutput>> GetAllPagingSortSearch(string searchString,
            int pageSize, int pageIndex, bool sortByLikeCount = true)
        {
            IEnumerable<CommentOutput> comments = await GetAllRow();
            if (!String.IsNullOrEmpty(searchString))
                comments = comments.Where(_ => _.Content.Contains(searchString));
            comments = Helper.Paging<CommentOutput>(comments.ToList(), pageSize, pageIndex);
            if(sortByLikeCount == false)
                return comments.OrderBy(_ => _.LikeCount).ToList();
            return comments.OrderByDescending(_ => _.LikeCount).ToList();
        }

        public async Task<Comment> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<Comment>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == hashCode &&
                                     _.isDeleted == false);
        }

        public async Task<List<CommentOutput>> GetParentByLessonHashCode(string hashCode)
        {
            Lesson lesson = _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == hashCode && x.isDeleted == false);
            if (lesson == null)
                return null;
            return (await GetAllRow()).Where(_ => _.ParentHashCode == null &&
                                          _.LessonHashCode == hashCode).ToList();
        }

        public async Task<bool> IsCommentHaveChild(string hashCode)
        {
            Comment comment = await GetByHashCode(hashCode);
            return await _uow.GetRepository<Comment>()
                .AsQueryable()
                .AnyAsync(_ => _.ParentId == comment.Id &&
                          _.isDeleted == false);
        }

        public async Task<bool> IsCommentHaveParent(string hashCode)
        {
            return (await GetByHashCode(hashCode)).ParentId == null ? false : true;
        }

        public async Task<int> LikeComment(string hashCode)
        {
            Comment comment = await GetByHashCode(hashCode);
            if (comment == null)
                return -1;
            try
            {
                comment.LikeCount += 1;
                _uow.GetRepository<Comment>().Update(comment);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<CommentOutput>> GetAllByLessonAsTreeFormat(string lessonHashCode, long? parentId)
        {
            List<CommentOutput> commentOutputs = new List<CommentOutput>();
            List<Comment> comments = await _uow.GetRepository<Comment>()
                .AsQueryable()
                .Include(_ => _.User)
                .Where(_ => _.isDeleted == false &&
                       _.Lesson.HashCode == lessonHashCode &&
                       _.ParentId == parentId)
                .ToListAsync();
            foreach (var c in comments)
            {
                commentOutputs.Add(new CommentOutput
                {
                    HashCode = c.HashCode,
                    Title = c.Title,
                    Content = c.Content,
                    LikeCount = c.LikeCount,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Status = c.Status,
                    ParentHashCode = c.ParentComment == null ? null : c.ParentComment.HashCode,
                    LessonHashCode = lessonHashCode,
                    LessonName = c.Lesson.Name,
                    CreateBy = c.User.UserName,
                    SubComments = (await GetAllByLessonAsTreeFormat(lessonHashCode, c.Id)).ToList()
                });
            }
            return commentOutputs;
        }
    }
}
