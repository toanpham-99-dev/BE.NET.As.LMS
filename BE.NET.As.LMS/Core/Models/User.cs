using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class User : IdentityUser<long>
    {
        public string HashCode { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string BI { get; set; }
        public string Avatar { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool isDeleted { get; set; } = false;
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<NotificationUser> NotificationUsers { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<SocialMedia> SocialMedias { get; set; }
        public virtual List<QuizUser> QuizUsers { get; set; }
        public virtual List<AssignmentUser> AssignmentUsers { get; set; }
        public virtual ICollection<IdentityUserClaim<long>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<long>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<long>> Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
