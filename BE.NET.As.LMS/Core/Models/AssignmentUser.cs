
using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class AssignmentUser
    {
        public long UserId { get; set; }
        public long AssignmentId { get; set; }
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public int Point { get; set; }
        public string Link { get; set; }
        public virtual User User { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}
