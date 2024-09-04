using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class AssignmentController : BaseAPIController
    {
        private readonly IAssignmentServices _assignmentServices;
        public AssignmentController(IAssignmentServices assignmentServices)
        {
            _assignmentServices = assignmentServices;
        }
        [HttpGet("get-all-assignment")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            List<AssignmentOutput> assignments = await _assignmentServices.GetAll();
            if (assignments == null || assignments.Count == 0)
            {
                return NoContent();
            }
            return Ok(new ApiResponse<List<AssignmentOutput>>
            {
                Data = assignments,
                Message = "Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-assignment-by-lesson")]
        [Authorize]
        public async Task<IActionResult> GetAllByLesson(string hashCode)
        {
            List<AssignmentOutput> assignments = await _assignmentServices.GetAllByLesson(hashCode);
            if (assignments == null || assignments.Count == 0)
            {
                return NoContent();
            }
            return Ok(new ApiResponse<List<AssignmentOutput>>
            {
                Data = assignments,
                Message = "Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-assignment-by-user")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> GetAllByUser(string hashCode)
        {
            List<AssignmentUserOutput> assignments = await _assignmentServices.GetAllByUser(hashCode);
            if (assignments == null || assignments.Count == 0)
            {
                return NoContent();
            }
            return Ok(new ApiResponse<List<AssignmentUserOutput>>
            {
                Data = assignments,
                Message = "Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("get-all-assignment-by-current-user")]
        [Authorize]
        public async Task<IActionResult> GetAllByCurrentUser()
        {
            long currentUserId = Helper.GetCurrentUserId(this.User);
            List<AssignmentUserOutput> assignments = await _assignmentServices.GetAllCurrentByUser(currentUserId);
            if (assignments == null || assignments.Count == 0)
            {
                return NoContent();
            }
            return Ok(new ApiResponse<List<AssignmentUserOutput>>
            {
                Data = assignments,
                Message = "Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPost("add-assignment-user")]
        [Authorize]
        public async Task<IActionResult> AddAssignmentUser(AssignmentUserInput assignmentUserInput)
        {
            long currentUserId = Helper.GetCurrentUserId(this.User);
            var result = await _assignmentServices.AddAssignmentUser(currentUserId, assignmentUserInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add Assignment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add Assignment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("update-assignment-user")]
        [Authorize]
        public async Task<IActionResult> UpdateAssignmentUser(string hashCode, AssignmentUserInput assignmentUserInput)
        {
            long currentUserId = Helper.GetCurrentUserId(this.User);
            var result = await _assignmentServices.UpdateAssignmentUser(currentUserId, hashCode, assignmentUserInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update Assignment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update Assignment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-assignment")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> AddAssignment(AssignmentInput assignmentInput)
        {
            var result = await _assignmentServices.AddAssignment(assignmentInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add Assignment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add Assignment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPost("update-assignment")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> UpdateAssignment(string hashCode, AssignmentInput assignmentInput)
        {
            var result = await _assignmentServices.UpdateAssignment(hashCode, assignmentInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Updated Assignment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Updated Assignment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPost("delete-assignment")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> DeleteAssignment(string hashCode)
        {
            var result = await _assignmentServices.DeleteAssignment(hashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Deleted Assignment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Deleted Assignment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
