using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Comment : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public long? ParentId { get; set; }
        public long LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual List<Comment> SubComments { get; set; }
    }
}
