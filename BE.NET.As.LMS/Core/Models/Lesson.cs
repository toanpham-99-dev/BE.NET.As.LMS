using BE.NET.As.LMS.Core.Models.Base;
using System;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Lesson : BaseModel
    {
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkVideo { get; set; }
        public int Priority { get; set; }
        public long SectionId { get; set; }
        public virtual Section Section { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Assignment> Assignments { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
