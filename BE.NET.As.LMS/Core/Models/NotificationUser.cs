using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class NotificationUser
    {
        public long NotificationId { get; set; }
        public long UserId { get; set; }
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public virtual User User { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
