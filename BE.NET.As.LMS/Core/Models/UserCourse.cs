using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class UserCourse
    {
        public long UserId { get; set; }
        public long CourseId { get; set; }
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public string UserHashCode { get; set; }
        public string CourseHashCode { get; set; }
        public int TotalLesson { get; set; }
        public int Completed { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}
