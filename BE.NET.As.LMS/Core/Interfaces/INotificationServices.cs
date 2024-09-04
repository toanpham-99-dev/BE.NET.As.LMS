using BE.NET.As.LMS.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface INotificationServices
    {
        Task<List<NotificationOutput>> GetAllPagingFilterSearch(string searchString, int pageSize,
            int pageIndex, bool? isRead);
        Task<List<NotificationOutput>> GetAllByUserHashCode(string userHashCode);
        Task<Notification> GetByHashCode(string hashCode);
        Task<int> AddNotification(NotificationInput notificationInput);
        Task<int> UpdateDetailNotification(NotificationUpdateDetailInput notificationUpdateDetailInput);
        Task<int> UpdateInlineNotification(NotificationInlineInput notificationInlineInput);
        Task<int> DeleteNotification(string hashCode);
        Task<NotificationOutput> GetNotification(string hashCode);
    }
}
