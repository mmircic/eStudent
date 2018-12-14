using eStudent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStudent.EntityConfig
{
    public class SubjectCourseConfig : IEntityTypeConfiguration<SubjectCourse>
    {
        public void Configure(EntityTypeBuilder<SubjectCourse> builder)
        {
            builder.HasKey(sc => new { sc.SubjectId, sc.CourseId });

            builder.HasOne(sc => sc.Subject)
                .WithMany(sc => sc.SubjectCourses)
                .HasForeignKey(sc => sc.SubjectId);

            builder.HasOne(sc => sc.Course)
                .WithMany(sc => sc.SubjectCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
