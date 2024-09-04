using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Category : BaseModel
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public long? ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual List<Category> SubCategories { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}
