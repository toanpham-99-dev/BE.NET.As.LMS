using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output.Base;
using System;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class CourseAdminOutput : BaseOutput
    {
        public string Summary { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public double? Rating { get; set; }
        public TimeSpan Duration { get; set; }
        public EnumCourseLevel Level { get; set; }
        public string Syllabus { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public string CreatedBy { get; set; }
        public virtual Category Category { get; set; }

    }
}
