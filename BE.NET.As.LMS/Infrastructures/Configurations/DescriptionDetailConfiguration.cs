using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class DescriptionDetailConfiguration : IEntityTypeConfiguration<DescriptionDetail>
    {
        public void Configure(EntityTypeBuilder<DescriptionDetail> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Description);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
        }
    }
}
