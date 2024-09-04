using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface ICommentServices
    {
        Task<List<CommentOutput>> GetAllRow();
        Task<List<CommentOutput>> GetAllPagingSortSearch(string searchString, int pageSize,
            int pageIndex, bool sortByLikeCount = true);
        Task<List<CommentOutput>> GetAllByLessonAsTreeFormat(string lessonHashCode, long? parentId);
        Task<List<CommentOutput>> GetParentByLessonHashCode(string hashCode);
        Task<List<CommentOutput>> GetAllByParentHashCode(string parentHashCode);
        Task<Comment> GetByHashCode(string hashCode);
        Task<int> AddComment(long currentUserId, CommentInput commentInput);
        Task<int> LikeComment(string hashCode);
        Task<int> DeleteComment(string hashCode);
        Task<bool> IsCommentHaveParent(string hashCode);
        Task<bool> IsCommentHaveChild(string hashCode);
    }
}
