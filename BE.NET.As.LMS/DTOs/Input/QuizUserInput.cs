using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class QuizUserInput
    {
        [Required]
        public string QuizHashCode { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
