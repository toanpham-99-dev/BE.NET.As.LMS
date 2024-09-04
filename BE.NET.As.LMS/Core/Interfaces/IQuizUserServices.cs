using BE.NET.As.LMS.DTOs.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IQuizUserServices
    {
        Task<int> AddQuizUser(QuizUserInput quizUserInput, long currentId);
    }
}
