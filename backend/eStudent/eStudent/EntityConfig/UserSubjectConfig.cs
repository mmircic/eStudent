using eStudent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStudent.EntityConfig
{
    public class UserSubjectConfig : IEntityTypeConfiguration<UserSubject>
    {
        public void Configure(EntityTypeBuilder<UserSubject> builder)
        {
            builder.HasKey(us => new { us.UserId, us.SubjectId });

            builder.HasOne(us => us.User)
                .WithMany(us => us.UserSubjects)
                .HasForeignKey(us => us.UserId);

            builder.HasOne(us => us.Subject)
                .WithMany(us => us.UserSubjects)
                .HasForeignKey(us => us.SubjectId);
        }
    }
}
