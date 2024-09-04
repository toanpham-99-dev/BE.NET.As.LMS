using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(_ => _.Id);
            builder.Property(_ => _.TotalPrice).IsRequired().HasPrecision(18, 3);
            builder.Property(_ => _.Quantity).HasDefaultValue(0);
            builder.Property(_ => _.UserId).IsRequired();
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Orders)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
        }
    }
}
