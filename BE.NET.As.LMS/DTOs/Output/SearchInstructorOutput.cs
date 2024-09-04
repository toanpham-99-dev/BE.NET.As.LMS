using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class SearchInstructorOutput : BaseOutput
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
