using BE.NET.As.LMS.Core.Models.Base;

namespace BE.NET.As.LMS.Core.Models
{
    public class SocialMedia : BaseModel
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
