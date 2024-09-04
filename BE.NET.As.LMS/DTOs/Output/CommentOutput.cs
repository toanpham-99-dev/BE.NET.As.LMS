using BE.NET.As.LMS.DTOs.Output.Base;
using System;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class CommentOutput : BaseOutput
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
        public string ParentHashCode { get; set; }
        public string LessonHashCode { get; set; }
        public string LessonName { get; set; }
        public string CreateBy { get; set; }
        public List<CommentOutput> SubComments { get; set; }
    }
}
