using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class AuthenticationController : BaseAPIController
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationController(IAuthenticationServices authenticationServices, SignInManager<User> signInManager)
        {
            _authenticationServices = authenticationServices;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterInput registerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticationServices.Register(registerInput);
            if (string.IsNullOrEmpty(result.Data))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginInput loginInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticationServices.Authenticate(loginInput);
            if (string.IsNullOrEmpty(result.Data))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("login-with-phonenumber")]
        public async Task<IActionResult> LoginWithPhoneNumber([FromForm] LoginInput loginInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticationServices.LoginWithPhoneNumber(loginInput);
            if (string.IsNullOrEmpty(result.Data))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _authenticationServices.Logout(userHashCode);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Message = "Logout failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>()
            {
                Message = "Logout successfully!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("comfirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, string token)
        {
            var result = await _authenticationServices.ConfirmEmail(email, token);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Message = "Confirm Email failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>()
            {
                Message = "Confirm Email succesfully!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _authenticationServices.ForgetPassword(email);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Message = "Forget Password failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>()
            {
                Message = "Please Check Email!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordInput resetPasswordInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Message = "Reset Password Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _authenticationServices.ResetPassword(resetPasswordInput);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Message = "Reset Password Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>()
            {
                Message = "Reset Password Successfully!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var provider = "Facebook";
            var redirectUrl = "api/1/Authentication/call-back";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var provider = "Google";
            var redirectUrl = "api/1/Authentication/call-back";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [HttpGet("call-back")]
        public async Task<IActionResult> CallBack(string remoteError = null)
        {
            var rs = await _authenticationServices.ExternalLogin();
            return Ok(rs);
        }
    }
}
