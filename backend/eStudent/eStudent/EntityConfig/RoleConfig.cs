using eStudent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStudent.EntityConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.RoleId);
        }
    }
}
