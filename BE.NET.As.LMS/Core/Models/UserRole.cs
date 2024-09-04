using Microsoft.AspNetCore.Identity;
using System;

namespace BE.NET.As.LMS.Core.Models
{
    public class UserRole : IdentityUserRole<long>
    {
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public string UserHashCode { get; set; }
        public string RoleHashCode { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
