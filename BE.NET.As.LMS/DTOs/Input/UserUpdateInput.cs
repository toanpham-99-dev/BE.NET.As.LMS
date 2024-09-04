using BE.NET.As.LMS.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class UserUpdateInput
    {
        [Required]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        public string FullName { get; set; }
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        public string PhoneNumber { get; set; }
        public IFormFile NewAvatar { get; set; }
        public bool isDeleted { get; set; } = false;
        public RoleInput Role { get; set; }
    }
}
