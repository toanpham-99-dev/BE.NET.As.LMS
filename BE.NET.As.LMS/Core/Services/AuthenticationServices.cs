using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Core.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        public AuthenticationServices(IUnitOfWork uow, UserManager<User> userMananger, SignInManager<User> signInManager, IConfiguration config, IEmailSender emailSender)
        {
            _uow = uow;
            _userManager = userMananger;
            _signInManager = signInManager;
            _config = config;
            _emailSender = emailSender;
        }

        public async Task<ApiResponse<string>> Authenticate(LoginInput loginInput)
        {
            var user = _uow.GetRepository<User>().AsQueryable()
                .Where(_ => _.UserName == loginInput.UserName || _.Email == loginInput.UserName)
                .FirstOrDefault();
            if (user == null)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Account does not exist",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.PasswordHash, loginInput.Password) != PasswordVerificationResult.Success)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Wrong account or password",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            if (!user.EmailConfirmed)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Please confirm email",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            return new ApiResponse<string>()
            {
                Data = await GenerateTokenAsync(user),
                Message = "Logged in successfully",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Logout(string userHashCode)
        {
            if (string.IsNullOrEmpty(userHashCode))
                return false;
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<ApiResponse<string>> Register(RegisterInput registerInput)
        {
            var user = await _userManager.FindByEmailAsync(registerInput.Email);
            if (user != null)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Email has been registered",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            user = await _userManager.FindByNameAsync(registerInput.UserName);
            if (user != null)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Account has been registered",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            var hasher = new PasswordHasher<User>();
            user = new User
            {
                SecurityStamp = _config["Tokens:SecretKey"],
                Email = registerInput.Email.ToLower(),
                Avatar = "basic",
                NormalizedEmail = registerInput.Email.ToLower(),
                FullName = registerInput.FullName,
                PhoneNumber = registerInput.PhoneNumber,
                UserName = registerInput.UserName.ToLower().Trim(),
                NormalizedUserName = registerInput.UserName.ToLower().Trim(),
                DateOfBirth = registerInput.DateOfBirth,
                PasswordHash = hasher.HashPassword(null, registerInput.Password)
            };
            try
            {
                _uow.BeginTransaction();
                _uow.GetRepository<User>().Add(user);
                await _uow.SaveChangesAsync();
                var userRole = new UserRole()
                {
                    HashCode = Guid.NewGuid().ToString(),
                    RoleId = 3,
                    UserId = user.Id,
                    RoleHashCode = RoleHashCode.User,
                    UserHashCode = user.HashCode
                };
                _uow.GetRepository<UserRole>().Add(userRole);
                await _uow.SaveChangesAsync();
                user = await _userManager.FindByEmailAsync(registerInput.Email);
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (string.IsNullOrEmpty(code))
                {
                    _uow.RollbackTransaction();
                    return new ApiResponse<string>()
                    {
                        Data = null,
                        Message = "There was an error, please try again!",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                string subject = "Xác nhận email đăng ký";
                if (!await _emailSender.SendEmailAsync(subject, user.Email, CreateBodyEmailConfirm(user.Email, code)))
                {
                    _uow.RollbackTransaction();
                    return new ApiResponse<string>()
                    {
                        Data = null,
                        Message = "There was an error, please try again!",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                _uow.CommitTransaction();
                return new ApiResponse<string>()
                {
                    Data = await GenerateTokenAsync(user),
                    Message = "Please confirm the registration email",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception)
            {
                _uow.RollbackTransaction();
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "There was an error, please try again!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<bool> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            string subject = "Đặt Lại Mật Khẩu Của Bạn";
            if (!await _emailSender.SendEmailAsync(subject, email, CreateBodyEmailForgetPassword(code)))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordInput resetPasswordInput)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordInput.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordInput.Code, resetPasswordInput.NewPassword);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Hash,user.HashCode),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string CreateBodyEmailConfirm(string email, string code)
        {
            code = HttpUtility.UrlEncode(code);
            var emailEncode = HttpUtility.UrlEncode(email);
            string link = $"{_config["Urls:Local"]}/api/1/Authentication/comfirm-email?email={emailEncode}&token={code}";
            string hrefLink = $"<a href=\"{link}\">Link</a>";
            var content = $"<h2> Vui lòng xác nhận email của bạn: {hrefLink}</h2>";
            return content;
        }
        private string CreateBodyEmailForgetPassword(string code)
        {
            var content = $"<h2> Mã xác nhận quên mật khẩu của bạn:</h2> {code}";
            return content;
        }

        public async Task<ApiResponse<string>> LoginWithPhoneNumber(LoginInput loginInput)
        {
            var user = _uow.GetRepository<User>().AsQueryable()
                  .Where(_ => _.PhoneNumber == loginInput.UserName)
                  .FirstOrDefault();
            if (user == null)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Account does not exist",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.PasswordHash, loginInput.Password) != PasswordVerificationResult.Success)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Wrong account or password",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            if (!user.EmailConfirmed)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "Please confirm email",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            return new ApiResponse<string>()
            {
                Data = await GenerateTokenAsync(user),
                Message = "Logged in successfully",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse<string>> ExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return new ApiResponse<string>()
                {
                    Message = "Loggin failed",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                var userExisted = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                return new ApiResponse<string>()
                {
                    Data = await GenerateTokenAsync(userExisted),
                    Message = "Logged in successfully",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            if (result.IsLockedOut)
            {
                return new ApiResponse<string>()
                {
                    Message = "Account Is Looked, Please Reset Password!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            string externalMail = null;
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                externalMail = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            var userWithExternalMail = (externalMail != null) ? (await _userManager.FindByEmailAsync(externalMail)) : null;
            if (userWithExternalMail != null)
            {
                if (!userWithExternalMail.EmailConfirmed)
                {
                    var codeActiveEmail = await _userManager.GenerateEmailConfirmationTokenAsync(userWithExternalMail);
                    await _userManager.ConfirmEmailAsync(userWithExternalMail, codeActiveEmail);
                }
                var resultAdd = await _userManager.AddLoginAsync(userWithExternalMail, info);
                if (resultAdd.Succeeded)
                {
                    await _uow.SaveChangesAsync();
                    return new ApiResponse<string>()
                    {
                        Data = await GenerateTokenAsync(userWithExternalMail),
                        Message = "Logged in successfully",
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
            }
            var hasher = new PasswordHasher<User>();
            string password = "Hcmfresher10";
            var user = new User()
            {
                SecurityStamp = _config["Tokens:SecretKey"],
                Email = externalMail,
                FullName = info.Principal.FindFirstValue(ClaimTypes.Name),
                PhoneNumber = "0123456xxx",
                UserName = externalMail,
                PasswordHash = hasher.HashPassword(null, password),
                DateOfBirth = DateTime.Parse("1/1/2000"),
                Avatar = "basic"
            };
            var rs = await _userManager.CreateAsync(user);
            if (rs.Succeeded)
            {
                var codeActiveEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, codeActiveEmail);
                var resultAdd = await _userManager.AddLoginAsync(user, info);
                await _uow.SaveChangesAsync();
                var userRole = new UserRole()
                {
                    HashCode = Guid.NewGuid().ToString(),
                    RoleId = 3,
                    UserId = user.Id,
                    RoleHashCode = RoleHashCode.User,
                    UserHashCode = user.HashCode
                };
                _uow.GetRepository<UserRole>().Add(userRole);
                await _uow.SaveChangesAsync();
                return new ApiResponse<string>()
                {
                    Data = await GenerateTokenAsync(user),
                    Message = $"First Login successfully! Your Password : {password}, Please change your profile!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            return new ApiResponse<string>()
            {
                Message = "Login Failed",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
