using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class NoteInput
    {
        [Required]
        public double TimeSpan { get; set; }
        public string Content { get; set; }
    }
}
