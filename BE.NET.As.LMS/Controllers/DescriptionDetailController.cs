using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class DescriptionDetailController : BaseAPIController
    {
        private readonly IDescriptionDetailServices _descriptionDetailServices;
        public DescriptionDetailController(IDescriptionDetailServices descriptionDetailServices)
        {
            _descriptionDetailServices = descriptionDetailServices;
        }
        [HttpGet("get-by-course-hash-code")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCourse(string courseHashCode)
        {
            if (string.IsNullOrEmpty(courseHashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid input",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            var result = await _descriptionDetailServices.GetByCourse(courseHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Course not found or this course have no description detail",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            return Ok(new ApiResponse<DescriptionDetailOutput>
            {
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get description detail success"
            });
        }
        [Authorize(Roles = "admin,instructor")]
        [HttpPost("add-description-detail")]
        public async Task<IActionResult> AddDescriptionDetail(string courseHashCode, string description)
        {
            if (string.IsNullOrEmpty(courseHashCode) || string.IsNullOrEmpty(description))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid input",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _descriptionDetailServices.AddDescriptionDetail(courseHashCode, description,
                userHashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add description detail fail",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            return Ok(new ApiResponse<string>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Add description detail success"
            });
        }
        [Authorize(Roles = "admin,instructor")]
        [HttpPut("update-description-detail")]
        public async Task<IActionResult> UpdateDescriptionDetail(string hashCode, string description)
        {
            if (string.IsNullOrEmpty(hashCode) || string.IsNullOrEmpty(description))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid input",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            var result = await _descriptionDetailServices.UpdateDescriptionDetail(hashCode, description);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update description detail fail",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            return Ok(new ApiResponse<string>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update description detail success"
            });
        }
    }
}
