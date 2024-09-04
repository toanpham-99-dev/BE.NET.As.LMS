using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IQuizServices
    {
        Quiz GetByHashCode(string hashcode);
        Task<List<QuizOutput>> GetAllByLesson(string hashCode);
        Task<int> CreateQuiz(QuizInput quizInput);
        Task<int> UpdateQuiz(QuizInput quizInput, string hashCodeQuiz);
        Task<int> DeleteQuiz(string hashCodeQuiz);
    }
}
