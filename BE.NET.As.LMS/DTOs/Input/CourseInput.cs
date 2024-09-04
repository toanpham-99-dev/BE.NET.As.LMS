using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class CourseInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile ImageURL { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public EnumCourseLevel Level { get; set; }
        [Required]
        [RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$",
        ErrorMessage = "{0} must be a Number.")]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(100)]
        public string Syllabus { get; set; }
        [Required]
        [MaxLength(250)]
        public string Summary { get; set; }
        [Required]
        public string CategoryHashCode { get; set; }
    }
}
