using System;
using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class RegisterInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{6,20}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        public string FullName { get; set; }
        [StringLength(250, MinimumLength = 6, ErrorMessage = "the {0} Can not be longer than 250 and shorter 6 charaters")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
