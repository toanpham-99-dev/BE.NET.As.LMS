using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Quiz : BaseModel
    {
        public long LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual List<QuizUser> QuizUsers { get; set; }
        public string QuizContent { get; set; }
        public int Score { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}