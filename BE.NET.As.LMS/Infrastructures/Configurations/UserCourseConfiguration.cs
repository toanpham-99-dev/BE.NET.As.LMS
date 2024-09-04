using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.ToTable("UserCourses")
                .HasKey(_ => new { _.CourseId, _.UserId });
            builder.Property(_ => _.Completed)
                .IsRequired();
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.Course)
                .WithMany(_ => _.UserCourses)
                .HasForeignKey(_ => _.CourseId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.UserCourses)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
