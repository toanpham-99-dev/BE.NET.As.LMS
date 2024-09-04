using BE.NET.As.LMS.DTOs.Output.Base;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class AssignmentOutput : BaseOutput
    {
        public string AssignmentName { get; set; }
        public string LessonName { get; set; }
    }
}
