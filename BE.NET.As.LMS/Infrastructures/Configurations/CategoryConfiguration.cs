using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories").HasKey(_ => _.Id);
            builder.Property(_ => _.HashCode).IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(250);
            builder.Property(_ => _.Description).IsRequired();
            builder.Property(_ => _.Alias).IsRequired();
            builder.Property(_ => _.ImageURL).IsRequired();
            builder.HasOne(_ => _.ParentCategory)
                .WithMany(_ => _.SubCategories)
                .HasForeignKey(_ => _.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
