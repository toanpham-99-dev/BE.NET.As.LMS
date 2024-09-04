using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Assignment : BaseModel
    {
        public string AssignmentName { get; set; }
        public long LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual List<AssignmentUser> AssignmentUsers { get; set; }
    }
}
