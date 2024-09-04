using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.Infrastructures.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BE.NET.As.LMS.Infrastructures
{
    public class LMSDataContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public LMSDataContext()
        {

        }
        public LMSDataContext(DbContextOptions<LMSDataContext> options) : base(options)
        {

        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentUser> AssignmentUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DescriptionDetail> DescriptionDetails { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<QuizUser> QuizUsers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentUserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguaration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationUserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new QuizConfiguration());
            modelBuilder.ApplyConfiguration(new QuizUserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCourseConfiguration());
            modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims").HasKey(_ => _.Id);
            modelBuilder.Entity<IdentityUserRole<long>>().ToTable("UserRoles").HasKey(_ => new { _.UserId, _.RoleId });
            modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins").HasKey(_ => _.UserId);
            modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims").HasKey(_ => _.Id);
            modelBuilder.Entity<IdentityUserToken<long>>().ToTable("UserToken").HasKey(_ => _.UserId);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
        }
    }
}
