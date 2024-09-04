using BE.NET.As.LMS.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class QuizInput
    {
        [Required]
        public string HashCodeLesson { get; set; }
        [Required]
        public string QuizContent { get; set; }
        [Required]
        [Range(0, 100)]
        public int Score { get; set; }
        public List<AnswerInput> Answer { get; set; }


    }
}
