using BE.NET.As.LMS.Core.Models.Base;

namespace BE.NET.As.LMS.Core.Models
{
    public class DescriptionDetail : BaseModel
    {
        public string Description { get; set; }
        public long? CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
