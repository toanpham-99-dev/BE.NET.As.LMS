using System;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class NotificationInput
    {
        [MaxLength(250)]
        public string HashCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string UserHashCode { get; set; }
    }
}
