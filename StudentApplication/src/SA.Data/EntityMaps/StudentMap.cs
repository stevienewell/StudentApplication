using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SA.Data.Entities;

namespace SA.Data.EntityMaps
{
    public class StudentMap
    {
        public StudentMap(EntityTypeBuilder<Student> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.FirstName).IsRequired();
            entityTypeBuilder.Property(t => t.LastName).IsRequired();
            entityTypeBuilder.Property(t => t.Email).IsRequired();
            entityTypeBuilder.Property(t => t.EnrollmentNo).IsRequired();
        }
    }
}
