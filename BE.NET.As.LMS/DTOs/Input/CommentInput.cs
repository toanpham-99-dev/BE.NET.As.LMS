using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class CommentInput
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string ParentHashCode { get; set; }
        [Required]
        public string LessonHashCode { get; set; }
    }
}
