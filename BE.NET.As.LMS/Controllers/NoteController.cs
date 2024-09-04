using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class NoteController : BaseAPIController
    {
        private readonly INoteServices _noteServices;
        public NoteController(INoteServices noteServices)
        {
            _noteServices = noteServices;
        }
        [Authorize]
        [HttpPut("add-note")]
        public async Task<IActionResult> AddNote([FromForm] NoteInput noteInput, string lessionHashCode)
        {
            if (string.IsNullOrEmpty(noteInput.Content))
            {
                return BadRequest();
            }
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _noteServices.AddNote(userHashCode, lessionHashCode, noteInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Created Note Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Created Note Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize]
        [HttpGet("get-list-note")]
        public async Task<IActionResult> GetListNote(string lessonHashCode)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            if (string.IsNullOrEmpty(lessonHashCode) || string.IsNullOrEmpty(userHashCode))
            {
                return BadRequest();
            }
            return Ok(await _noteServices.GetListNote(lessonHashCode, userHashCode));
        }
        [Authorize]
        [HttpPut("update-note")]
        public async Task<IActionResult> UpdateNote(string hashCode, string lessonHashCode, NoteInput noteInput)
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            if (!ModelState.IsValid || string.IsNullOrEmpty(hashCode) || string.IsNullOrEmpty(userHashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Updated Note Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _noteServices.UpdateNote(hashCode, userHashCode, lessonHashCode, noteInput);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Updated Note Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Updated Note Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize]
        [HttpDelete("delete-note")]
        public async Task<IActionResult> DeleteNote(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Updated Note Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            var result = await _noteServices.DeleteNote(hashCode);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Deleted Note Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Deleted Note Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });

        }
    }
}

