using BE.NET.As.LMS.DTOs.Output.Base;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class AssignmentUserOutput : BaseOutput
    {
        public string AssignmentName { get; set; }
        public string LessonName { get; set; }
        public string UserName { get; set; }
    }
}
