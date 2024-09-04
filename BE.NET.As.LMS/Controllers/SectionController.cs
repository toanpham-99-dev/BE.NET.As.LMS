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
    public class SectionController : BaseAPIController
    {
        private readonly ISectionServices _sectionServices;
        public SectionController(ISectionServices sectionServices)
        {
            _sectionServices = sectionServices;
        }
        [HttpPost("createSection")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> CreateSection([FromBody] SectionInput sectionInput)
        {
            var result = await _sectionServices.CreateSection(sectionInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<SectionInput>()
                {
                    Message = "Create Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<SectionInput>()
            {
                Data = sectionInput,
                Message = "Create Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("update-section")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> UpdateSection([FromBody] UpdateSectionInput updateSectionInput, string sectionHashCode)
        {

            if (!ModelState.IsValid || sectionHashCode == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Input Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _sectionServices.UpdateSection(sectionHashCode, updateSectionInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<UpdateSectionInput>()
                {
                    Message = "Update Failed!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<CourseInput>()
            {
                Message = "Update Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("delete-section")]
        [Authorize(Roles = "instructor,admin")]
        public async Task<IActionResult> DeleteSection(string sectionHashCode)
        {
            var result = await _sectionServices.DeleteSection(sectionHashCode);
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
        [HttpGet("get-sections-orderby-priority")]
        public async Task<IActionResult> GetSectionsOrderByPriority(string courseHashCode)
        {
            List<SectionOutput> sections = await _sectionServices.GetSectionsOrderByPriority(courseHashCode);
            if (sections.Count <= 0 || sections == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Message = "Not found"
                });
            }
            return Ok(new ApiResponse<List<SectionOutput>>
            {
                Data = sections,
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
