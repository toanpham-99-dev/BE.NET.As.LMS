using BE.NET.As.LMS.Core.Models.Base;
using System;
using System.Collections.Generic;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Core.Models
{
    public class Course : BaseModel
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
        public string InstructorHashCodeCreated { get; set; }
        public string UpdatedBy { get; set; }
        public string PublishedBy { get; set; }
        public string DeletedBy { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<Section> Sections { get; set; }
        public long? DescriptionDetailId { get; set; }
        public virtual DescriptionDetail DescriptionDetail { get; set; }
    }
}
