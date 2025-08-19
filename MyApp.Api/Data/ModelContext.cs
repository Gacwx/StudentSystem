using Microsoft.EntityFrameworkCore;
using MyApp.Api.Models;

namespace MyApp.Api.Data
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("DEPARTMENT");
            modelBuilder.Entity<Department>().HasKey(d => d.Id);
            modelBuilder.Entity<Department>().Property(d => d.Id).HasColumnName("ID");
            modelBuilder.Entity<Department>().Property(d => d.Name).HasColumnName("NAME");

            modelBuilder.Entity<Student>().ToTable("STUDENT");
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().Property(s => s.Id).HasColumnName("ID");
            modelBuilder.Entity<Student>().Property(s => s.Name).HasColumnName("NAME");
            modelBuilder.Entity<Student>().Property(s => s.Email).HasColumnName("EMAIL");
            modelBuilder.Entity<Student>().Property(s => s.Gender).HasColumnName("GENDER");
            modelBuilder.Entity<Student>().Property(s => s.DepartmentId).HasColumnName("DEPARTMENTID");

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);
        }
    }
}
