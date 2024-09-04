using BE.NET.As.LMS.DTOs.Output.Base;
using BE.NET.As.LMS.DTOs.Response;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class LessonDetailOutput : BaseOutput
    {
        public LessonOutput Lesson { get; set; }
        public int TotalLesson { get; set; }
        public int Completed { get; set; }
        public List<QuizOutput> Quiz { get; set; }
        public List<AssignmentOutput> Assignments { get; set; }
        public ApiResponse<NoteOutput> Notes { get; set; }
        public List<CommentOutput> Comments { get; set; }
    }
}
