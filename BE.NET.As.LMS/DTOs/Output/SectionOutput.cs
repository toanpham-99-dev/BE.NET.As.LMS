using BE.NET.As.LMS.DTOs.Output.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class SectionOutput : BaseOutput
    {
        public string Description { get; set; }
        public List<LessonOutput> Lesson { get; set; }
    }
}
