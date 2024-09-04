using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class AssignmentInput
    {
        [Required]
        public string AssignmentName { get; set; }
        [Required]
        public string LessonHashCode { get; set; }
    }
}
