using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class AssignmentServices : IAssignmentServices
    {
        private readonly IUnitOfWork _uow;
        public AssignmentServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Assignment GetByHashCode(string hashCode)
        {
            return _uow.GetRepository<Assignment>().AsQueryable()
               .FirstOrDefault(x => x.HashCode == hashCode);
        }

        public async Task<List<AssignmentOutput>> GetAll()
        {
            return await _uow.GetRepository<Assignment>().AsQueryable()
                .Where(_ => _.isDeleted == false)
                .Select(_ => new AssignmentOutput
                {
                    AssignmentName = _.AssignmentName,
                    LessonName = _.Lesson.Name,
                }).ToListAsync();
        }

        public async Task<List<AssignmentOutput>> GetAllByLesson(string hashCode)
        {
            return await _uow.GetRepository<Assignment>().AsQueryable()
                .Where(_ => _.Lesson.HashCode == hashCode && _.isDeleted == false)
                .Where(_ => _.isDeleted == false)
                .Select(_ => new AssignmentOutput
                {
                    AssignmentName = _.AssignmentName,
                    LessonName = _.Lesson.Name,
                }).ToListAsync();
        }

        public async Task<List<AssignmentUserOutput>> GetAllByUser(string hashCode)
        {
            return await _uow.GetRepository<AssignmentUser>().AsQueryable()
                .Include(_ => _.Assignment)
                .ThenInclude(_ => _.Lesson)
                .Where(_ => _.User.HashCode == hashCode && _.User.isDeleted == false)
                .Select(_ => new AssignmentUserOutput
                {
                    AssignmentName = _.Assignment.AssignmentName,
                    LessonName = _.Assignment.Lesson.Name,
                    UserName = _.User.FullName,
                }).ToListAsync();
        }

        public async Task<List<AssignmentUserOutput>> GetAllCurrentByUser(long currentUserId)
        {
            return await _uow.GetRepository<AssignmentUser>().AsQueryable()
                .Include(_ => _.Assignment)
                .ThenInclude(_ => _.Lesson)
                .Where(_ => _.User.Id == currentUserId && _.User.isDeleted == false)
                .Select(_ => new AssignmentUserOutput
                {
                    AssignmentName = _.Assignment.AssignmentName,
                    LessonName = _.Assignment.Lesson.Name,
                    UserName = _.User.FullName,
                }).ToListAsync();
        }

        public async Task<int> AddAssignmentUser(long currentUserId, AssignmentUserInput assignmentUserInput)
        {
            var assignment = GetByHashCode(assignmentUserInput.AssignmentHashCode);
            if (assignment == null)
            {
                return -1;
            }
            var assignmentUser = new AssignmentUser
            {
                Link = assignmentUserInput.Link,
                UserId = currentUserId,
                Assignment = assignment,
            };
            _uow.GetRepository<AssignmentUser>().Add(assignmentUser);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> UpdateAssignmentUser(long currentUserId, string hashCode, AssignmentUserInput assignmentUserInput)
        {
            var assignment = GetByHashCode(assignmentUserInput.AssignmentHashCode);
            AssignmentUser assignmentUser = _uow.GetRepository<AssignmentUser>().AsQueryable()
                    .FirstOrDefault(_ => _.HashCode == hashCode);
            if (assignmentUser == null)
            {
                return -1;
            }
            assignmentUser.AssignmentId = assignment.Id;
            assignmentUser.Link = assignmentUserInput.Link;
            assignmentUser.UserId = currentUserId;
            _uow.GetRepository<AssignmentUser>().Update(assignmentUser);
            return await _uow.SaveChangesAsync();
        }
        public async Task<int> AddAssignment(AssignmentInput assignmentInput)
        {
            var lesson = _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == assignmentInput.LessonHashCode && x.isDeleted == false);
            if (lesson == null)
            {
                return -1;
            }
            var newAssignment = new Assignment
            {
                AssignmentName = assignmentInput.AssignmentName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Lesson = lesson,
            };
            _uow.GetRepository<Assignment>().Add(newAssignment);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> UpdateAssignment(string hashCode, AssignmentInput assignmentInput)
        {
            Assignment assignment = _uow.GetRepository<Assignment>().AsQueryable()
                    .FirstOrDefault(_ => _.HashCode == hashCode);
            if (assignment == null)
            {
                return -1;
            }
            assignment.AssignmentName = assignmentInput.AssignmentName;
            assignment.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Assignment>().Update(assignment);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> DeleteAssignment(string hashCode)
        {
            Assignment assignment = _uow.GetRepository<Assignment>().AsQueryable()
                    .FirstOrDefault(_ => _.HashCode == hashCode);
            if (assignment == null)
            {
                return -1;
            }
            assignment.isDeleted = true;
            _uow.GetRepository<Assignment>().Update(assignment);
            return await _uow.SaveChangesAsync();
        }

    }
}
