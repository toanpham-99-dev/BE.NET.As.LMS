using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class CategoriesAdminOutput
    {
        public List<CategoryOutput> Categories { get; set; }
        public SelectList ParentCategories { get; set; }
    }
}
