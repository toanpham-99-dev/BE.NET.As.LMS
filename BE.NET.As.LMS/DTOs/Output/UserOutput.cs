using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class UserOutput : BaseOutput
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
