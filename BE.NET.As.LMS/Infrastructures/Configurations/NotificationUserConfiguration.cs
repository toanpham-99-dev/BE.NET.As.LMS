using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class NotificationUserConfiguration : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable("NotificationUsers").HasKey(_ => new { _.NotificationId, _.UserId });
            builder.Property(_ => _.UserId).IsRequired();
            builder.Property(_ => _.NotificationId).IsRequired();
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(x => x.Notification)
                .WithMany(x => x.NotificationUsers)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User)
                .WithMany(x => x.NotificationUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
