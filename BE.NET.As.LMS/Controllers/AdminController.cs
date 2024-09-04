using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseAPIController
    {
        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ApiResponse<List<UserDetailOutput>>
            {
                Data = await _adminServices.GetListUser()
            });
        }
        [HttpGet("get-user-detail")]
        public async Task<IActionResult> GetUserDetail(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest();
            }
            var result = await _adminServices.GetUserDetail(hashCode);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("get-user-paging")]
        public async Task<IActionResult> GetUserPaging([FromQuery] PagingInput pagingInput, string roleHashCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(new ApiResponse<PageResponse<UserDetailOutput>>
            {
                Data = await _adminServices.GetUserPaging(roleHashCode, pagingInput)
            });
        }
        [HttpPut("update-detail-user")]
        public async Task<IActionResult> UpdateDetailUser([FromForm] UserUpdateInput userInput, string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Edit users failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var rs = await _adminServices.UpdateDetailUser(userInput, hashCode);
            if (!rs)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Edit users failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Edit successful users",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("update-in-line-user")]
        public async Task<IActionResult> UpdateInLineUser([FromForm] UserUpdateInput userInput, string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Edit users failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _adminServices.UpdateUserInLine(userInput, hashCode);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Edit users failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Edit successful users",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromForm] RegisterInput registerInput, string rolehashCode)
        {
            if (string.IsNullOrEmpty(rolehashCode))
            {
                return BadRequest();
            }
            var result = await _adminServices.AddUser(registerInput, rolehashCode);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add user failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<StatisticOutput>
            {
                Message = "Add user success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Detele user failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _adminServices.DeleteUser(hashCode);
            if (result <= 0)
            {
                return BadRequest(result);
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Detele user success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("recover-user")]
        public async Task<IActionResult> RecoverUser(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Recover user failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _adminServices.RecoverUser(hashCode);
            if (result <= 0)
            {
                return BadRequest(result);
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Recover user success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _adminServices.GetStatistics();
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "Data currently empty",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<StatisticOutput>
            {
                Data = result,
                Message = "Get statistics success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("filter-instructor")]
        public async Task<IActionResult> SearchByInstructor(string searchString, int pageSize, int pageIndex)
        {
            var users = await _adminServices.SearchByInstructor(searchString, pageSize, pageIndex);
            if (pageSize <= 0 || pageIndex <= 0)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            if (users == null)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            return Ok(new ApiResponse<List<SearchInstructorOutput>>
            {
                Data = users
            });
        }
    }
}
