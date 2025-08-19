using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using Microsoft.EntityFrameworkCore;
using MyApp.Data.Models;

namespace MyApp.Data.Data
{

    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .ToTable("STUDENT")
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .Property(s => s.StudentId)
                .HasColumnName("ID");

            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .HasColumnName("FULLNAME");

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .HasColumnName("EMAIL");

            modelBuilder.Entity<Student>()
                .Property(s => s.Gender)
                .HasColumnName("GENDER");

            modelBuilder.Entity<Student>()
                .Property(s => s.DepartmentId)
                .HasColumnName("DEPARTMENTID");

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);

            modelBuilder.Entity<Department>()
                .ToTable("DEPARTMENT")
                .HasKey(d => d.DepartmentId);

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentId)
                .HasColumnName("ID");

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasColumnName("NAME");
        }
    }

}
