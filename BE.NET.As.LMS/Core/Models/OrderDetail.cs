using BE.NET.As.LMS.Core.Models.Base;

namespace BE.NET.As.LMS.Core.Models
{
    public class OrderDetail : BaseModel
    {
        public decimal Price { get; set; }
        public string CourseName { get; set; }
        public long? CourseId { get; set; }
        public virtual Course Course { get; set; }
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
