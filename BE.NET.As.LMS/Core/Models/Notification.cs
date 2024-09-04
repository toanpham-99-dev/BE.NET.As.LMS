using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Notification : BaseModel
    {
        public string Content { get; set; }
        public string Link { get; set; }
        public bool IsRead { get; set; }
        public virtual List<NotificationUser> NotificationUsers { get; set; }
    }
}
