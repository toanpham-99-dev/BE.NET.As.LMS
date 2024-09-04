using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface INotificationUserService
    {
        Task<int> SendNotification(string userHashCode, string notificationHashCode);
    }
}
