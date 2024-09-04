using BE.NET.As.LMS.DTOs.Output.Base;
using System;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class UserDetailOutput : BaseOutput
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDeleted { get; set; } = false;
        public RoleOutput Role { get; set; }
    }
}
