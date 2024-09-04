using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Section : BaseModel
    {
        public string Description { get; set; }
        public long CourseId { get; set; }
        public int Priority { get; set; }
        public virtual Course Course { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
    }
}
