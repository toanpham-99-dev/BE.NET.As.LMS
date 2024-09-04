using BE.NET.As.LMS.DTOs.Output.Base;
using System;
using System.Collections.Generic;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class GuestCourseDetailOutput : BaseOutput
    {
        public string Summary { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public double? Rating { get; set; }
        public TimeSpan CourseDuration { get; set; }
        public EnumCourseLevel Level { get; set; }
        public string Syllabus { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public string CategoryName { get; set; }
        public List<SectionOutput> Section { get; set; }
    }
}
