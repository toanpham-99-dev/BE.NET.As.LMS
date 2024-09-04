using BE.NET.As.LMS.Core.Models.Base;
using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class Note : BaseModel
    {
        public string JsonContent { get; set; }
        public string LessonHashCode { get; set; }
        public long LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public string UserHashCode { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
