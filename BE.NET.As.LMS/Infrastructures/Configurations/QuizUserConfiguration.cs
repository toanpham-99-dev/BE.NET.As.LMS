using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class QuizUserConfiguration : IEntityTypeConfiguration<QuizUser>
    {
        public void Configure(EntityTypeBuilder<QuizUser> builder)
        {
            builder.HasKey(_ => new { _.UserId, _.QuizId });
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.Property(_ => _.Score).IsRequired().HasMaxLength(3);
            builder.Property(_ => _.UserId).IsRequired();
            builder.Property(_ => _.QuizId).IsRequired();
            builder.HasOne<User>(_ => _.User)
                    .WithMany(_ => _.QuizUsers)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Quiz>(_ => _.Quiz)
                    .WithMany(_ => _.QuizUsers)
                    .HasForeignKey(_ => _.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
