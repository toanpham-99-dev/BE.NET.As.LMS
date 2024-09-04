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
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork _uow;
        private IUserServices _userServices;
        public NotificationServices(IUnitOfWork uow, IUserServices userServices)
        {
            _uow = uow;
            _userServices = userServices;
        }

        public async Task<int> AddNotification(NotificationInput notificationInput)
        {
            try
            {
                var notification = new Notification
                {
                    Content = notificationInput.Content,
                    Link = notificationInput.Link,
                    IsRead = false,
                    CreatedAt = DateTime.Now,
                    Status = notificationInput.Status,
                    isDeleted = false,
                };
                _uow.GetRepository<Notification>().Add(notification);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> DeleteNotification(string hashCode)
        {
            try
            {
                var notification = await GetByHashCode(hashCode);
                if (notification == null)
                    return -1;
                notification.isDeleted = true;
                _uow.GetRepository<Notification>().Update(notification);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<NotificationOutput>> GetAllByUserHashCode(string userHashCode)
        {
            User user = await _userServices.GetByHashCode(userHashCode);
            if (user == null)
                return null;
            return await _uow.GetRepository<Notification>()
                .AsQueryable()
                .Include(_ => _.NotificationUsers)
                .Where(_ => _.isDeleted == false && _.NotificationUsers.Any(_ => _.UserId == user.Id))
                .Select(_ => new NotificationOutput
                {
                    Content = _.Content,
                    Link = _.Link,
                    IsRead = _.IsRead,
                    Status = _.Status,
                    CreatedAt = _.CreatedAt,
                    HashCode = _.HashCode
                }).Take(20).ToListAsync();
        }

        public async Task<List<NotificationOutput>> GetAllPagingFilterSearch(string searchString, int pageSize,
            int pageIndex, bool? isRead)
        {
            IEnumerable<Notification> notifications = await _uow.GetRepository<Notification>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .Take(50)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
                notifications = notifications.Where(_ => _.Content.Contains(searchString));
            if (isRead != null)
                notifications = notifications.Where(_ => _.IsRead == isRead);
            notifications = Helper.Paging<Notification>(notifications.ToList(), pageSize, pageIndex);
            return notifications.Select(_ => new NotificationOutput
            {
                Content = _.Content,
                Link = _.Link,
                IsRead = _.IsRead,
                Status = _.Status,
                CreatedAt = _.CreatedAt,
                HashCode = _.HashCode
            }).ToList();
        }

        public async Task<Notification> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<Notification>().AsQueryable()
                .FirstOrDefaultAsync(c => c.HashCode == hashCode &&
                                     c.isDeleted == false);
        }

        public async Task<NotificationOutput> GetNotification(string hashCode)
        {
            var notification = await GetByHashCode(hashCode);
            if (notification == null)
                return null;
            var notificationOuput = new NotificationOutput
            {
                Content = notification.Content,
                Link = notification.Link,
                IsRead = notification.IsRead,
                Status = notification.Status,
                CreatedAt = notification.CreatedAt,
                HashCode = notification.HashCode
            };
            return notificationOuput;
        }

        public async Task<int> UpdateDetailNotification(NotificationUpdateDetailInput notificationUpdateDetailInput)
        {
            try
            {
                var existNotification = await GetByHashCode(notificationUpdateDetailInput.HashCode);
                if (existNotification == null)
                    return -1;
                existNotification.Content = notificationUpdateDetailInput.Content;
                existNotification.Link = notificationUpdateDetailInput.Link;
                existNotification.Status = notificationUpdateDetailInput.Status;
                existNotification.IsRead = notificationUpdateDetailInput.IsRead;
                existNotification.isDeleted = notificationUpdateDetailInput.IsDeleted;
                existNotification.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Notification>().Update(existNotification);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> UpdateInlineNotification(NotificationInlineInput notificationInlineInput)
        {
            try
            {
                var notification = await GetByHashCode(notificationInlineInput.HashCode);
                if (notification == null)
                    return -1;
                notification.Content = notificationInlineInput.Content;
                notification.Link = notificationInlineInput.Link;
                notification.IsRead = notificationInlineInput.IsRead;
                notification.CreatedAt = notificationInlineInput.CreatedAt;
                notification.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Notification>().Update(notification);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }
    }
}
