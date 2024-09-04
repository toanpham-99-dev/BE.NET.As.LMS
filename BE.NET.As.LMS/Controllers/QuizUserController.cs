using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class QuizUserController : BaseAPIController
    {
        private readonly IQuizUserServices _quizUserServices;
        public QuizUserController(IQuizUserServices quizUserServices)
        {
            _quizUserServices = quizUserServices;
        }
        [HttpPost("create-quiz-user")]
        [Authorize]
        public async Task<IActionResult> AddQuizUser(QuizUserInput quizUserInput)
        {
            long currentId = Helper.GetCurrentUserId(this.User);
            var result = await _quizUserServices.AddQuizUser(quizUserInput, currentId);
            if (result <= 0)
            {
                return BadRequest(new ApiResponse<QuizUserInput>()
                {
                    Message = "Create Error!",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            }
            return Ok(new ApiResponse<QuizUserInput>()
            {
                Data = quizUserInput,
                Message = "Create Successful!",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
