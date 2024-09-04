using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageServices _fileStorageServices;
        public UserServices(IUnitOfWork uow, IFileStorageServices fileStorageServices)
        {
            _uow = uow;
            _fileStorageServices = fileStorageServices;
        }
        public async Task<User> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<User>().AsQueryable()
              .FirstOrDefaultAsync(x => x.HashCode == hashCode && x.isDeleted == false);
        }
        public async Task<UserOutput> GetDetailByHashCode(string hashCode)
        {
            var user = await GetByHashCode(hashCode);
            return new UserOutput
            {
                FullName = user.FullName,
                Email = user.Email,
                Avatar = user.Avatar,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
            };
        }
        public async Task<bool> UpdateUser(UserInput userInput, string hashCode)
        {
            var user = await GetByHashCode(hashCode);
            if (hashCode != userInput.HashCode || user == null)
            {
                return false;
            }
            user.FullName = userInput.FullName;
            if (userInput.Avatar != null)
            {
                var newAvatarUrl = await _fileStorageServices.SaveFileAsync(userInput.Avatar);
                user.Avatar = newAvatarUrl;
            }
            user.DateOfBirth = userInput.DateOfBirth;
            user.Email = userInput.Email;
            user.PhoneNumber = userInput.PhoneNumber;
            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();
            return true;
        }
        public async Task<List<UserNotificationOutput>> NotificationUser(string hashCode)
        {
            return await _uow.GetRepository<NotificationUser>().AsQueryable()
                .Where(_ => _.HashCode == hashCode)
                .Include(_ => _.Notification)
                .Where(_ => _.Notification.isDeleted == false)
                .Include(_ => _.User)
                .Select(_ => new UserNotificationOutput
                {
                    HashCode = _.HashCode,
                    Content = _.Notification.Content,
                    CreatedAt = _.Notification.CreatedAt,
                    Status = _.Notification.Status
                }).ToListAsync();
        }

        public async Task<bool> UserEnroll(string userHashCode, string courseHashCode)
        {
            User user = await GetByHashCode(userHashCode);
            Course course = await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.HashCode == courseHashCode && _.isDeleted == false && _.Status == 1)
                .FirstOrDefaultAsync();
            if (user == null || course == null)
            {
                return false;
            }

            var userCourse1 = await _uow.GetRepository<UserCourse>().AsQueryable()
                 .Where(_ => _.CourseHashCode == courseHashCode && _.UserHashCode == userHashCode)
                 .FirstOrDefaultAsync();
            if (userCourse1 != null)
            {
                return false;
            }
            var userCourse = new UserCourse()
            {
                UserHashCode = userHashCode,
                CourseHashCode = courseHashCode,
                CourseId = course.Id,
                UserId = user.Id,
            };
            _uow.GetRepository<UserCourse>().Add(userCourse);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<List<SectionOutput>> GetSectionsByUserCourse(string courseHashCode, string userHashCode)
        {
            User user = await _uow.GetRepository<User>().AsQueryable()
                   .Where(x => x.HashCode == userHashCode && x.isDeleted == false)
                   .FirstOrDefaultAsync();
            var course = GetByHashCode(courseHashCode);
            if (course == null || user == null)
            {
                return null;
            }
            return await _uow.GetRepository<Section>()
                .AsQueryable()
                .Include(x => x.Lessons)
                .Include(x => x.Course)
                .Where(x => x.Course.HashCode == courseHashCode && x.isDeleted == false)
                .OrderBy(x => x.Priority)
                .Select(x => new SectionOutput
                {
                    Description = x.Description,
                    HashCode = x.HashCode,
                    Lesson = x.Lessons
                    .Where(x => x.isDeleted == false)
                    .Select(x => new LessonOutput
                    {
                        Name = x.Name,
                        Duration = x.Duration,
                        Description = x.Description,
                        LinkVideo = x.LinkVideo,
                        HashCode = x.HashCode
                    })
                    .ToList()
                })
                .ToListAsync();
        }
    }
}
