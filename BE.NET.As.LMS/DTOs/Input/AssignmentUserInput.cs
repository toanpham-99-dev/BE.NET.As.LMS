using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class AssignmentUserInput
    {
        [Required]
        public string Link { get; set; }
        [Required]
        public string AssignmentHashCode { get; set; }
    }
}
