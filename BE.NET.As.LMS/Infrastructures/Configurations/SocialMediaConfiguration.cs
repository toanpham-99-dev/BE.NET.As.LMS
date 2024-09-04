using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.ToTable("SocialMedias")
                .HasKey(_ => _.Id);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.Property(_ => _.Link)
                .HasMaxLength(4000);
            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(_ => _.UserId)
                .IsRequired();
            builder.HasOne(s => s.User)
                .WithMany(u => u.SocialMedias)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
