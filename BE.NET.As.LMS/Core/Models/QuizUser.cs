using BE.NET.As.LMS.Core.Models.Base;
using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class QuizUser
    {
        public long UserId { get; set; }
        public long QuizId { get; set; }
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public int Score { get; set; }
        public virtual User User { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
