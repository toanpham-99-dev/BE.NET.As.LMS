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
    public class LessonController : BaseAPIController
    {
        private readonly ILessonServices _lessonServices;
        public LessonController(ILessonServices lessonServices)
        {
            _lessonServices = lessonServices;
        }
        [HttpPost("create-lesson")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> CreateLesson([FromBody] LessonInput lessonInput)
        {
            var result = await _lessonServices.CreateLesson(lessonInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<LessonInput>()
                {
                    Message = "Create Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<LessonInput>()
            {
                Data = lessonInput,
                Message = "Create Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("update-lesson")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonInput updateLessonInput, string lessonHashCode)
        {

            if (!ModelState.IsValid || lessonHashCode == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Input Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _lessonServices.UpdateLesson(lessonHashCode, updateLessonInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<UpdateLessonInput>()
                {
                    Message = "Update Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<LessonInput>()
            {
                Message = "Update Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("delete-lesson")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> DeleteLesson(string lessonHashCode)
        {
            var result = await _lessonServices.DeleteLesson(lessonHashCode);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Delete Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Delete Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-lessons-orderby-priority")]
        public async Task<IActionResult> GetLessonsOrderByPriority(string sectionHashCode)
        {
            List<LessonOutput> lessons = await _lessonServices.GetLessonsOrderByPriority(sectionHashCode);
            if (lessons.Count <= 0 || lessons == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<List<LessonOutput>>
            {
                Data = lessons,
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
