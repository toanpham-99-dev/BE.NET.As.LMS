using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class CategoryUpdateInput
    {
        [MaxLength(250)]
        public string HashCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public string Description { get; set; }
        [Required]
        public int Status { get; set; }
        [MaxLength(250)]
        public string ParentCategoryHashCode { get; set; }
    }
}
