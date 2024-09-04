using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BE.NET.As.LMS.Infrastructures.Configurations
{
    public class AssignmentUserConfiguration : IEntityTypeConfiguration<AssignmentUser>
    {
        public void Configure(EntityTypeBuilder<AssignmentUser> builder)
        {
            builder.ToTable("AssignmentUsers").HasKey(_ => new { _.UserId, _.AssignmentId });
            builder.Property(_ => _.Link).IsRequired();
            builder.Property(_ => _.HashCode).IsRequired().HasMaxLength(250);
            builder.HasIndex(_ => _.HashCode).IsUnique();
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.AssignmentUsers)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(_ => _.Assignment)
                .WithMany(_ => _.AssignmentUsers)
                .HasForeignKey(_ => _.AssignmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
