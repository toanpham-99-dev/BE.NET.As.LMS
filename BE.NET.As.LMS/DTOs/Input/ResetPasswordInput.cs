using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class ResetPasswordInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{6,20}$", ErrorMessage = "Invalid Password")]
        public string NewPassword { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
