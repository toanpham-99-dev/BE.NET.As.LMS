using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class CommentController : BaseAPIController
    {
        private readonly ICommentServices _commentServices;
        private readonly ILessonServices _lessonServices;
        public CommentController(ICommentServices commentServices,
            ILessonServices lessonServices)
        {
            _commentServices = commentServices;
            _lessonServices = lessonServices;
        }
        [HttpGet("get-all-row-comments")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRow()
        {
            var result = await _commentServices.GetAllRow();
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No comment found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CommentOutput>>
            {
                Data = result,
                Message = "Get comments success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-comments-paging-sort-search")]
        public async Task<IActionResult> GetAllPagingSortSearch(string searchString, int pageSize,
                 int pageIndex, bool sortByLikeCount = true)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get comments failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _commentServices.GetAllPagingSortSearch(searchString, pageSize,
                pageIndex, sortByLikeCount);
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No comments found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CommentOutput>>
            {
                Data = result,
                Message = "Get comments success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-as-tree-format-comments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByLessonAsTreeFormat(string lessonHashCode)
        {
            if (lessonHashCode == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid Iutput",
                    StatusCode = (int)HttpStatusCode.OK
                });
            Lesson lesson = _lessonServices.GetByHashCode(lessonHashCode);
            if (lesson == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "No lesson found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            var result = await _commentServices.GetAllByLessonAsTreeFormat(lessonHashCode, null);
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No comments found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CommentOutput>>
            {
                Data = result,
                Message = "Get comments success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-parent-comment-lesson-hash-code")]
        [AllowAnonymous]
        public async Task<IActionResult> GetParentByLessonHashCode(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid parent comment",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _commentServices.GetParentByLessonHashCode(hashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get comments fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<List<CommentOutput>>
            {
                Data = result,
                Message = "Get comments success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-comments-by-parent-hashcode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByParentHashCode(string parentHashCode)
        {
            if (String.IsNullOrEmpty(parentHashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid parent Comment",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _commentServices.GetAllByParentHashCode(parentHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get comments fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<List<CommentOutput>>
            {
                Data = result,
                Message = "Get comments success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-comment-by-hash-code")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByHashCode(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest();
            var result = await _commentServices.GetByHashCode(hashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Comment not found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            result.Id = -1;
            return Ok(new ApiResponse<Comment>
            {
                Data = result,
                Message = "Get Comment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-comment")]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentInput commentInput)
        {
            long currentUserId = Helper.GetCurrentUserId(this.User);
            var result = await _commentServices.AddComment(currentUserId, commentInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add Comment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add Comment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("like-comment")]
        [Authorize]
        public async Task<IActionResult> LikeComment(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest();
            var result = await _commentServices.LikeComment(hashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Like fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Like comment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPut("delete-comment")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(string hashCode)
        {
            var result = await _commentServices.DeleteComment(hashCode);
            if (result < 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Delete Comment failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            else if (result == 0)
                return Ok(new ApiResponse<string>
                {
                    Message = "You cant delete this parent comment",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Delete comment success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("is-comment-have-parent")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCommentHaveParent(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid Comment",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _commentServices.IsCommentHaveParent(hashCode);
            if (!result)
                return Ok(new ApiResponse<string>
                {
                    Message = "Comment have no parent Comment",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Comment have a parent Comment",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("is-comment-have-child")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCommentHaveChild(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid Comment",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _commentServices.IsCommentHaveChild(hashCode);
            if (result == false)
                return Ok(new ApiResponse<string>
                {
                    Message = "Comment have no child Comment",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Comment have a child Comment",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
