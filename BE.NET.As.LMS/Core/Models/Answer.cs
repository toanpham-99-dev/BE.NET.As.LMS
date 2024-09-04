using BE.NET.As.LMS.Core.Models.Base;

namespace BE.NET.As.LMS.Core.Models
{
    public class Answer : BaseModel
    {
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
        public long QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}