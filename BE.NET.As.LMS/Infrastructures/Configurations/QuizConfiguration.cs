using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.HashCode).IsRequired()
                     .HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne<Lesson>(_ => _.Lesson)
                    .WithOne(_ => _.Quiz)
                    .HasForeignKey<Quiz>(_ => _.LessonId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
