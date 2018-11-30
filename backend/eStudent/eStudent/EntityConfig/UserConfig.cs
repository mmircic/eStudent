using eStudent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStudent.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.OIB).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.BirthDate).HasColumnType("date");

        }
    }
}
