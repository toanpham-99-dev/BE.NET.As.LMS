using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class QuizController : BaseAPIController
    {
        private readonly IQuizServices _quizServives;
        public QuizController(IQuizServices quizServives)
        {
            _quizServives = quizServives;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string hashCode)
        {
            List<QuizOutput> quiz = await _quizServives.GetAllByLesson(hashCode);
            if (quiz == null || quiz.Count == 0)
            {
                return NoContent();
            }
            return Ok(new ApiResponse<List<QuizOutput>>
            {
                Data = quiz,
                Message = "Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPost("create-quiz")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizInput quizInput)
        {
            if (!ModelState.IsValid || quizInput == null)
            {
                return BadRequest(new ApiResponse<QuizInput>
                {
                    Data = quizInput,
                    Message = "Not Match Validate",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            int result = await _quizServives.CreateQuiz(quizInput);
            if (result < 0)
            {
                return BadRequest(new ApiResponse<QuizInput>
                {
                    Data = quizInput,
                    Message = "Create quiz fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<QuizInput>
            {
                Data = quizInput,
                Message = "Create quiz success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPut("update-quiz")]
        public async Task<IActionResult> UpdateQuiz(string hashCodeQuiz, [FromBody] QuizInput quizInput)
        {
            if (!ModelState.IsValid || quizInput == null || hashCodeQuiz == null)
            {
                return BadRequest(new ApiResponse<QuizInput>
                {
                    Data = quizInput,
                    Message = "Not Match Validate",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            int result = await _quizServives.UpdateQuiz(quizInput, hashCodeQuiz);
            if (result < 0)
            {
                return BadRequest(new ApiResponse<QuizInput>
                {
                    Data = quizInput,
                    Message = "Update fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<QuizInput>
            {
                Data = quizInput,
                Message = "Update success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpDelete("delete-quiz")]
        public async Task<IActionResult> DeleteQuiz(string hashCodeQuiz)
        {
            if (hashCodeQuiz == null)
            {
                return NotFound();
            }
            var result = await _quizServives.DeleteQuiz(hashCodeQuiz);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Delete Fail!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<string>
            {
                Message = "Delete success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
