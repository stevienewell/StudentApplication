using Microsoft.EntityFrameworkCore;
using SA.Data.Entities;
using SA.Data.EntityMaps;

namespace SA.Data.DataContexts
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new StudentMap(modelBuilder.Entity<Student>());
        }
    }
}
