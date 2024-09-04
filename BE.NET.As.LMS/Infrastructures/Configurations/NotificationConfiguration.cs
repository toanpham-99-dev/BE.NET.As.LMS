using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications").HasKey(_ => _.Id);
            builder.Property(_ => _.Link).IsRequired();
            builder.Property(_ => _.IsRead).IsRequired().HasDefaultValue(false);
            builder.Property(_ => _.Content).IsRequired().HasMaxLength(250);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
        }
    }
}
