using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class HomeController : BaseAPIController
    {
        private readonly IHomeServices _homeServices;
        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices;
        }
        [AllowAnonymous]
        [HttpGet("get-course-outstanding")]
        public async Task<IActionResult> GetCourseOutstanding(int take)
        {
            return Ok(new ApiResponse<List<HomeCourseOutput>>
            {
                Message = "Danh sách khóa học phổ biến",
                Data = await _homeServices.GetCourseOutstanding(take)
            });
        }
        [AllowAnonymous]
        [HttpGet("get-course-new")]
        public async Task<IActionResult> GetCourseNew(int take)
        {
            return Ok(new ApiResponse<List<HomeCourseOutput>>
            {
                Message = "Danh sách khóa học mới",
                Data = await _homeServices.GetCourseNew(take)
            });
        }
    }
}
