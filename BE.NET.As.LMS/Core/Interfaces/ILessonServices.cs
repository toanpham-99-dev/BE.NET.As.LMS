using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface ILessonServices
    {
        Lesson GetByHashCode(string hashCode);
        Task<int> CreateLesson(LessonInput lessonInput);
        Task<int> UpdateLesson(string sectionHashCode, UpdateLessonInput updateLessonInput);
        Task<int> DeleteLesson(string lessonHashCode);
        Task<List<LessonOutput>> GetLessonsOrderByPriority(string sectionHashCode);
    }
}
