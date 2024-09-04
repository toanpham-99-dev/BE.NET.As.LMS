using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class NotificationOutput : BaseOutput
    {
        public string Content { get; set; }
        public string Link { get; set; }
        public bool IsRead { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
