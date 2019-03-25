using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Entities;

namespace WebApplication.AppContext
{
    public class AppDbContext: DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            builder.Entity<Group>()
                .HasOne(g => g.Faculty)
                .WithMany(f => f.Groups)
                .HasForeignKey(g => g.FacultyId);

            builder.Entity<StudentMark>()
                .HasKey(t => new { t.StudentId, t.MarkId });

            builder.Entity<StudentMark>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.StudentMarks)
                .HasForeignKey(pt => pt.StudentId);

            builder.Entity<StudentMark>()
                .HasOne(pt => pt.Mark)
                .WithMany(t => t.StudentMarks)
                .HasForeignKey(pt => pt.MarkId);

            base.OnModelCreating(builder);

            

            builder.Entity<Faculty>().HasData(
                new Faculty
                {    
                    Id=1,
                    Name = "Programming",
                },
                new Faculty
                {    
                    Id=2,
                    Name = "System administration and security",
                },
                new Faculty
                {    
                    Id=3,
                    Name = "Disign",

                },
                new Faculty
                {  
                    Id=5,
                    Name = "Base",
                });

            builder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "PP-12-1", FacultyId = 1 },
                new Group { Id = 2, Name = "PP-12-2", FacultyId = 1 },
                new Group { Id = 3, Name = "PP-12-3", FacultyId = 1 },
                new Group { Id = 4, Name = "PP-12-4", FacultyId = 1 });

            List<Mark> marks = new List<Mark>();
            for (int i = 1; i <= 12; i++)
            {
                marks.Add(new Mark { Id = i, Value = i });
            }
            builder.Entity<Mark>().HasData(marks);
        }

        public DbSet<WebApplication.Entities.Mark> Marks { get; set; }

        public DbSet<WebApplication.Entities.Subject> Subjects { get; set; }

        public DbSet<WebApplication.Entities.Teacher> Teacher { get; set; }

        public DbSet<WebApplication.Entities.StudentMark> StudentMark { get; set; }
    }
}
