using BE.NET.As.LMS.DTOs.Output.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class QuizOutput : BaseOutput
    {
        public string LessonName { get; set; }
        public string QuizContent { get; set; }
        public int Score { get; set; }
        public virtual List<AnswerOutput> Answers { get; set; }
    }
}
