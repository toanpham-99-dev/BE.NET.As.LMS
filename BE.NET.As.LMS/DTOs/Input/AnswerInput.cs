using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class AnswerInput
    {
        [Required]
        public string AnswerContent { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }
}