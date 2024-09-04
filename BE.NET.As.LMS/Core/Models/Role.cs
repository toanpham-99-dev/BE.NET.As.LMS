using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Role : IdentityRole<long>
    {
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
