using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles")
                .HasKey(_ => _.Id);
            builder.Property(_ => _.Name)
                .IsRequired().HasMaxLength(25);
            builder.Property(_ => _.Description)
                .HasMaxLength(500);
            builder.HasMany(_ => _.UserRoles)
                .WithOne(_ => _.Role)
                .HasForeignKey(_ => _.RoleId)
                .IsRequired();
        }
    }
}
