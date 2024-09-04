using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.CourseName).IsRequired()
                .HasMaxLength(250);
            builder.Property(_ => _.Price).IsRequired()
                .HasPrecision(18,3);
            builder.Property(_ => _.HashCode).IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne<Order>(_ => _.Order)
                    .WithMany(_ => _.OrderDetails)
                    .HasForeignKey(_ => _.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(_ => _.Course)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(_ => _.CourseId);
        }
    }
}
