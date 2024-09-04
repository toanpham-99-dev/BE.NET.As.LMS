using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Duration).IsRequired();
            builder.Property(_ => _.Name).IsRequired().HasMaxLength(150);
            builder.Property(_ => _.Description).IsRequired();
            builder.Property(_ => _.LinkVideo).IsRequired();
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.Section).WithMany(_ => _.Lessons)
                .HasForeignKey(_ => _.SectionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
