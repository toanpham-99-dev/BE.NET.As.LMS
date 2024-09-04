using BE.NET.As.LMS.DTOs.Output.Base;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class AnswerOutput : BaseOutput
    {
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
    }
}
