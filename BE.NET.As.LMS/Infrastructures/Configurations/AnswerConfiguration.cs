using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers").HasKey(_ => _.Id);
            builder.Property(_ => _.AnswerContent).IsRequired();
            builder.Property(_ => _.IsCorrect).IsRequired();
            builder.Property(_ => _.HashCode).IsRequired()
                    .HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.Quiz)
                .WithMany(_ => _.Answers)
                .HasForeignKey(_ => _.QuizId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}