using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class LessonOutput : BaseOutput
    {
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkVideo { get; set; }
    }
}
