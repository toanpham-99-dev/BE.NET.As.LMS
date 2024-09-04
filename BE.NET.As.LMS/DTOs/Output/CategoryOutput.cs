using BE.NET.As.LMS.DTOs.Output.Base;
using System;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class CategoryOutput : BaseOutput
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
        public string ParentHashCode { get; set; }
        public string ParentCategoryTitle { get; set; }
        public List<CategoryOutput> SubCategories { get; set; }
    }
}
