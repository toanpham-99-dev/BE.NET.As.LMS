using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class NotificationUpdateDetailInput
    {
        [MaxLength(250)]
        public string HashCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool IsRead { get; set; }
        [Required]
        public int Status { get; set; }
        
    }
}
