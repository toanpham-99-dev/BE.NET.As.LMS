using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments").HasKey(_ => _.Id);
            builder.Property(_ => _.HashCode).IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.Property(_ => _.AssignmentName).IsRequired();
            builder.HasOne(_ => _.Lesson)
                .WithMany(_ => _.Assignments)
                .HasForeignKey(_ => _.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
