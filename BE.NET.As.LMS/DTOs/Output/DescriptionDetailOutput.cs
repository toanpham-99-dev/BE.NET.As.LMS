using BE.NET.As.LMS.DTOs.Output.Base;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class DescriptionDetailOutput : BaseOutput
    {
        public string Description { get; set; }
        public string CourseHashCode { get; set; }
        public string CourseName { get; set; }
    }
}
