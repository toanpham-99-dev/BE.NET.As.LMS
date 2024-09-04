﻿using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class SearchCategoryOutput : BaseOutput
    {
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public virtual List<Category> SubCategories { get; set; }
    }
}
