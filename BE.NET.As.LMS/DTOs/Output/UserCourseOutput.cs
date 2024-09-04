using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class UserCourseOutput : BaseOutput
    {
        public string Summary { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public string CreatedBy { get; set; }
        public int TotalLesson { get; set; }
        public int Completed { get; set; } = 0;
    }
}
