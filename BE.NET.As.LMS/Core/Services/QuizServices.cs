using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class QuizServices : IQuizServices
    {
        private readonly IUnitOfWork _uow;
        public QuizServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Quiz GetByHashCode(string hashcode)
        {
            return _uow.GetRepository<Quiz>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == hashcode);
        }
        public async Task<List<QuizOutput>> GetAllByLesson(string hashCode)
        {
            return await _uow.GetRepository<Quiz>().AsQueryable()
                .Include(_ => _.Answers)
                .Where(_ => _.Lesson.HashCode == hashCode && _.isDeleted == false)
                .Select(_ => new QuizOutput
                {
                    HashCode = _.HashCode,
                    LessonName = _.Lesson.Name,
                    QuizContent = _.QuizContent,
                    Score = _.Score,
                    Answers = _.Answers.Select(_ => new AnswerOutput
                    {
                        AnswerContent = _.AnswerContent,
                        IsCorrect = _.IsCorrect,
                    }).ToList(),
                }).ToListAsync();
        }
        public async Task<int> CreateQuiz(QuizInput quizInput)
        {
            var lesson = _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == quizInput.HashCodeLesson && x.isDeleted == false);
            if (lesson == null)
            {
                return 0;
            }
            Quiz quiz = new Quiz()
            {
                LessonId = lesson.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                QuizContent = quizInput.QuizContent,
                Score = quizInput.Score,
                Answers = quizInput.Answer.Select(_ => new Answer
                {
                    AnswerContent = _.AnswerContent,
                    IsCorrect = _.IsCorrect
                }).ToList(),
                isDeleted = false,
            };
            _uow.GetRepository<Quiz>().Add(quiz);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> DeleteQuiz(string hashCodeQuiz)
        {
            try
            {
                var quiz = await _uow.GetRepository<Quiz>().AsQueryable()
                    .FirstOrDefaultAsync(_ => _.HashCode == hashCodeQuiz && _.isDeleted == false);
                if (quiz == null)
                {
                    return -1;
                }
                quiz.isDeleted = true;
                _uow.GetRepository<Quiz>().Update(quiz);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> UpdateQuiz(QuizInput quizInput, string hashCodeQuiz)
        {
            var quiz = await _uow.GetRepository<Quiz>().AsQueryable()
                 .FirstOrDefaultAsync(_ => _.HashCode == hashCodeQuiz);
            Lesson lesson = _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == quizInput.HashCodeLesson && x.isDeleted == false);
            if (quiz == null || lesson == null)
            {
                return 0;
            }
            quiz.LessonId = lesson.Id;
            quiz.UpdatedAt = DateTime.Now;
            quiz.QuizContent = quizInput.QuizContent;
            quiz.Score = quizInput.Score;
            quiz.Answers = quizInput.Answer
                .Select(_ => new Answer
                {
                    AnswerContent = _.AnswerContent,
                    IsCorrect = _.IsCorrect
                }).ToList();
            _uow.GetRepository<Quiz>().Update(quiz);
            return await _uow.SaveChangesAsync();
        }
    }
}
