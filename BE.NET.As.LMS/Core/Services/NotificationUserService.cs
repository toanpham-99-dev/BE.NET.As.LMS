using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class NotificationUserService : INotificationUserService
    {
        private readonly IUnitOfWork _uow;
        private IUserServices _userServices;
        public NotificationUserService(IUnitOfWork uow, IUserServices userServices)
        {
            _uow = uow;
            _userServices = userServices;
        }

        public async Task<int> SendNotification(string userHashCode, string notificationHashCode)
        {
            try
            {
                User user = await _userServices.GetByHashCode(userHashCode);
                Notification notification = await _uow.GetRepository<Notification>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(_ => _.HashCode == notificationHashCode && _.isDeleted == false);
                if (user == null || notification == null)
                    return -1;
                NotificationUser notificationUser = new NotificationUser();
                notificationUser.UserId = user.Id;
                notificationUser.NotificationId = notification.Id;
                _uow.GetRepository<NotificationUser>().Add(notificationUser);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }
    }
}
