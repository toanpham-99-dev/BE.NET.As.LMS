using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes").HasKey(_ => _.Id);
            builder.Property(_ => _.LessonId).IsRequired();
            builder.Property(_ => _.UserId).IsRequired();
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Notes)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.Lesson)
                .WithMany(_ => _.Notes)
                .HasForeignKey(_ => _.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
        }
    }
}
