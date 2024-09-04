using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using static BE.NET.As.LMS.Utilities.Constaint;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BE.NET.As.LMS.Controllers
{
    public class CourseController : BaseAPIController
    {
        private readonly ICourseServices _courseServices;
        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        [HttpPost("create-course")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> CreateCourse([FromForm] CourseInput courseInput)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _courseServices.CreateCourse(courseInput, userHashCode);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<CourseInput>()
                {
                    Message = "Create Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<CourseInput>()
            {
                Data = courseInput,
                Message = "Create Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("update-course")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> UpdateCourse([FromForm] UpdateCourseInput updateCourseInput, string courseHashCode)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            if (!ModelState.IsValid || courseHashCode == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Input Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _courseServices.UpdateCourse(courseHashCode, updateCourseInput, userHashCode);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<UpdateCourseInput>()
                {
                    Message = "Update Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<UpdateCourseInput>()
            {
                Message = "Update Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("delete-course")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> DeleteCourse(string courseHashCode)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _courseServices.DeleteCourse(courseHashCode, userHashCode);
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
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string searchName)
        {
            List<SearchCourseOutput> courses = await _courseServices.SearchByName(searchName.ToLower());
            if (courses.Count < 1 || courses == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<List<SearchCourseOutput>>
            {
                Data = courses
            });
        }

        [HttpGet("get-guest-course-detail")]
        public async Task<IActionResult> GetGuestDetail(string hashCode)
        {
            GuestCourseDetailOutput course = await _courseServices.GuestCourseDetail(hashCode);
            if (course == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<GuestCourseDetailOutput>
            {
                Data = course,
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("get-course-syllabus")]
        public async Task<IActionResult> GetSyllabus(string hashCode)
        {
            SyllabusOfCourseOutput course = await _courseServices.GetSyllabusOfCourse(hashCode);
            if (course == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<SyllabusOfCourseOutput>
            {
                Data = course,
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("get-course-rating")]
        public async Task<IActionResult> GetRating(string hashCode)
        {
            RatingOfCourseOutput course = await _courseServices.GetRatingOfCourse(hashCode);
            if (course == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<RatingOfCourseOutput>
            {
                Data = course,
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("filter-price")]
        public async Task<IActionResult> SearchByPrice(decimal searchPrice, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            var prices = await _courseServices.SearchByPrice(searchPrice, pageSize, pageIndex);
            if (prices == null)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            return Ok(new ApiResponse<List<SearchCourseOutput>>
            {
                Data = prices
            });
        }

        [HttpGet("filter-level")]
        public async Task<IActionResult> SearchByLevel(EnumCourseLevel searchLevel, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            var levels = await _courseServices.SearchByLevel(searchLevel, pageSize, pageIndex);
            if (levels == null)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            return Ok(new ApiResponse<List<SearchCourseOutput>>
            {
                Data = levels
            });
        }

        [HttpGet("get-courses-by-category")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCoursesByCategory(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid input",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            var result = await _courseServices.GetCoursesByCategory(hashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Category not found",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            return Ok(new ApiResponse<List<CourseByCategoryOutput>>
            {
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get courses success"
            });
        }
        [Authorize(Roles = "admin,instructor")]
        [HttpPut("public-or-block-course")]
        public async Task<IActionResult> PublicOrHideCourse(string courseHashCode, CourseStatus courseStatus)
        {
            if (string.IsNullOrEmpty(courseHashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid input",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            var result = await _courseServices.PublicOrHideCourse(courseHashCode, courseStatus);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update course fail",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                });
            return Ok(new ApiResponse<string>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update courses success"
            });
        }
        [HttpGet("filter-instructor")]
        public async Task<IActionResult> SearchByInstructor(string searchString, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            var users = await _courseServices.SearchByInstructor(searchString, pageSize, pageIndex);
            if (users == null)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            return Ok(new ApiResponse<List<SearchCourseOutput>>
            {
                Data = users
            });
        }
        [HttpGet("filter-catagories")]
        public async Task<IActionResult> SearchByCategory(string searchString, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            var categories = await _courseServices.SearchByCategory(searchString, pageSize, pageIndex);
            if (categories == null)
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Not found"
                });
            return Ok(new ApiResponse<List<SearchCategoryOutput>>
            {
                Data = categories
            });
        }
        [HttpGet("get-all-paging-filter-search-course")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllPagingFilterSearch(string searchString, EnumCourseLevel level, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get courses failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var courses = await _courseServices.GetAllPagingFilterSearchCourse(searchString, level, pageSize, pageIndex);
            if (courses == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CourseAdminOutput>>
            {
                Data = courses,
                Message = "Get courses success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("get-lesson-detail")]
        [Authorize]
        public async Task<IActionResult> GetLessonDetail(string courseHashCode, string lessonHashCode)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var courses = await _courseServices.LessonDetail(courseHashCode, userHashCode, lessonHashCode);
            if (courses == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                });
            return Ok(new ApiResponse<LessonDetailOutput>
            {
                Data = courses,
                Message = "Get courses success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpGet("get-course-detail")]
        public async Task<IActionResult> GetCourseDetail(string courseHashCode)
        {
            var courses = await _courseServices.UserCourseDetail(courseHashCode);
            if (courses == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                });
            return Ok(new ApiResponse<UserCourseDetailOutput>
            {
                Data = courses,
                Message = "Get courses success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-course-user-enroll")]
        [Authorize]
        public async Task<IActionResult> GetCourseUserEnroll()
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _courseServices.GetCourseUserEnroll(userHashCode);
            if (result == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<List<UserCourseOutput>>
            {
                Data = result,
                Message = "Success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-course-by-instructor")]
        [Authorize(Roles = "admin,instructor")]
        public async Task<IActionResult> GetCourseCreateByInstructor()
        {
            string instructorHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _courseServices.GetCourseCreateByInstructor(instructorHashCode);
            if (result == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Not found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<List<HomeCourseOutput>>
            {
                Data = result,
                Message = "Success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
