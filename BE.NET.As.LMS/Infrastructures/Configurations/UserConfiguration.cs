using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(_ => _.Id);
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.Property(_ => _.Email)
                .IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(_ => _.Email)
                .IsUnique();
            builder.Property(_ => _.UserName)
                .IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(_ => _.UserName)
                .IsUnique();
            builder.Property(_ => _.PhoneNumber)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(_ => _.FullName)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(_ => _.PasswordHash)
                .IsRequired();
            builder.HasMany(_ => _.Claims)
                    .WithOne()
                    .HasForeignKey(_ => _.UserId)
                    .IsRequired();
            builder.HasMany(_ => _.Logins)
                .WithOne()
                .HasForeignKey(_ => _.UserId)
                .IsRequired();
            builder.HasMany(_ => _.Tokens)
                .WithOne()
                .HasForeignKey(_ => _.UserId)
                .IsRequired();
            builder.HasMany(_ => _.UserRoles)
                .WithOne(_ => _.User)
                .HasForeignKey(_ => _.UserId)
                .IsRequired();
        }
    }
}
