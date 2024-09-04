using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class OrderDetailOutput : BaseOutput
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CourseName { get; set; }
        public string CourseHashCode { get; set; }
        public string OrderHashCode { get; set; }
    }
}
