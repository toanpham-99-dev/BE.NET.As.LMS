using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BE.NET.As.LMS.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> UserProfile(string hashCode)
        {
            var user = await _userServices.GetDetailByHashCode(hashCode);
            if (user != null)
            {
                return Ok(new ApiResponse<UserOutput>
                {
                    Data = user,
                    Message = "Successful!",
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            return NotFound();
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserInput user, string hashCode)
        {
            if (hashCode != user.HashCode)
            {
                return NotFound();
            }

            if (await _userServices.UpdateUser(user, hashCode) is true)
            {
                return Ok(new ApiResponse<UserOutput>
                {
                    Message = "Update Successful!",
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            return BadRequest();
        }

        [HttpPut("notification")]
        public async Task<IActionResult> UserNotification(string hashCode)
        {
            return Ok(new ApiResponse<List<UserNotificationOutput>>
            {
                Data = await _userServices.NotificationUser(hashCode),
            });
        }
        [Authorize]
        [HttpPost("user-enroll")]
        public async Task<IActionResult> UserEnroll(string courseHashCode)
        {
            if (string.IsNullOrEmpty(courseHashCode))
            {
                return BadRequest();
            }
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var rs = await _userServices.UserEnroll(userHashCode, courseHashCode);
            if (!rs)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("get-sections-by-usercourse")]
        [Authorize]
        public async Task<IActionResult> GetSectionsByUserCourse(string courseHashCode)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            List<SectionOutput> sections = await _userServices.GetSectionsByUserCourse(courseHashCode, userHashCode);
            if (sections == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<List<SectionOutput>>
            {
                Data = sections,
                Message = "Success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
