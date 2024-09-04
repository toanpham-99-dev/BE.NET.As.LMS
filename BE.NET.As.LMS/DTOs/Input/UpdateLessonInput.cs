using System;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class UpdateLessonInput
    {
        [Required]
        public string SectionHashCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string LinkVideo { get; set; }
        [RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0-0][0-0]:[0-0][0-0]$",
            ErrorMessage = "Invalid time format and hh:mm:ss values.")]
        public TimeSpan Duration { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Priority { get; set; }
    }
}
