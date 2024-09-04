using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Response;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<ApiResponse<string>> Register(RegisterInput registerInput);
        Task<ApiResponse<string>> Authenticate(LoginInput loginInput);
        Task<ApiResponse<string>> LoginWithPhoneNumber(LoginInput loginInput);
        Task<ApiResponse<string>> ExternalLogin();
        Task<bool> ConfirmEmail(string email, string code);
        Task<bool> ForgetPassword(string email);
        Task<bool> ResetPassword(ResetPasswordInput resetPasswordInput);
        Task<bool> Logout(string userHashCode);
    }
}
