using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class QuizUserServices : IQuizUserServices
    {
        private readonly IUnitOfWork _uow;
        public QuizUserServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<int> AddQuizUser(QuizUserInput quizUserInput, long currentId)
        {
            var quiz = await _uow.GetRepository<Quiz>().AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == quizUserInput.QuizHashCode);
            if (quiz == null)
            {
                return -1;
            }
            var quizUser = new QuizUser
            {
                UserId = currentId,
                QuizId = quiz.Id,
                Score = quizUserInput.Score
            };
            _uow.GetRepository<QuizUser>().Add(quizUser);
            return await _uow.SaveChangesAsync();
        }
    }
}
