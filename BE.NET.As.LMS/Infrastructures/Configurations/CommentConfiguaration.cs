using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class CommentConfiguaration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(250);
            builder.Property(_ => _.Content).IsRequired();
            builder.Property(_ => _.Content).IsRequired(false);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.Lesson)
                .WithMany(_ => _.Comments)
                .HasForeignKey(_ => _.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Comments)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.ParentComment)
                .WithMany(_ => _.SubComments)
                .HasForeignKey(_ => _.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
