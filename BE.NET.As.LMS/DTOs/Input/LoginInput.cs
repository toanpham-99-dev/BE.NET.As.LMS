using System.ComponentModel.DataAnnotations;

namespace BE.NET.As.LMS.DTOs.Input
{
    public class LoginInput
    {
        [Required]
        [MaxLength(255, ErrorMessage = "the {0} can not longer than {1} charaters")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "the {0} can not longer than {1} charaters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
