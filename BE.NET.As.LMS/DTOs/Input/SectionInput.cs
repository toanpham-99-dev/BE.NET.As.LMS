using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class SectionInput
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public string CourseHashCode { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Priority { get; set; }
    }
}
