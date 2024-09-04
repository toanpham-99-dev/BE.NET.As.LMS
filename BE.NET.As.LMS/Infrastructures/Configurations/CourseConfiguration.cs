using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Summary).IsRequired();
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(250);
            builder.Property(_ => _.Content).IsRequired();
            builder.Property(_ => _.ImageURL).IsRequired();
            builder.Property(_ => _.Rating);
            builder.Property(_ => _.Duration);
            builder.Property(_ => _.Level).IsRequired();
            builder.Property(_ => _.Description).IsRequired();
            builder.Property(_ => _.Syllabus).IsRequired();
            builder.Property(_ => _.Price).IsRequired().HasPrecision(18,3);
            builder.Property(_ => _.Status).IsRequired();
            builder.Property(_ => _.ViewCount).IsRequired();
            builder.Property(_ => _.CreatedBy).IsRequired();
            builder.Property(_ => _.UpdatedBy);
            builder.Property(_ => _.PublishedBy);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.Category)
                .WithMany(_ => _.Courses)
                .HasForeignKey(_ => _.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.DescriptionDetail)
                .WithOne(_ => _.Course)
                .HasForeignKey<DescriptionDetail>(_ => _.CourseId);
        }
    }
}
