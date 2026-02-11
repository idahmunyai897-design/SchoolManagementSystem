using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SchoolManagementSystem.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
        public DbSet<TutorAssignment> TutorAssignments { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
               new Admin
               {
                   AdminId = 1,
                   FullNames = "Super Admin",
                   Email = "admin@school.com",
                   Password = "admin123",
                   Role = "Admin"
               }
           );

            // StudentSubjectPerformance: Student ↔ Subject
            modelBuilder.Entity<StudentSubjectPerformance>()
                .HasOne(ssp => ssp.Student)
                .WithMany(s => s.StudentSubjectPerformances)
                .HasForeignKey(ssp => ssp.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSubjectPerformance>()
                .HasOne(ssp => ssp.Subject)
                .WithMany(sub => sub.StudentSubjectPerformances)
                .HasForeignKey(ssp => ssp.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);


            // Optional Peer Helper (self-reference)
            modelBuilder.Entity<StudentSubjectPerformance>()
                .HasOne(ssp => ssp.PeerHelper)
                .WithMany(s => s.PeerHelperAssignments)
                .HasForeignKey(ssp => ssp.PeerHelperId)
                .OnDelete(DeleteBehavior.SetNull);

            // Student ↔ Major
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Major)
                .WithMany(m => m.Students)
                .HasForeignKey(s => s.MajorId)
                .OnDelete(DeleteBehavior.SetNull);

            // TutorAssignment: Tutor ↔ Student ↔ Subject
            modelBuilder.Entity<TutorAssignment>()
                .HasOne(ta => ta.Tutor)
                .WithMany(t => t.TutorAssignments)
                .HasForeignKey(ta => ta.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TutorAssignment>()
                .HasOne(ta => ta.Student)
                .WithMany(s => s.TutorAssignments)
                .HasForeignKey(ta => ta.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TutorAssignment>()
                .HasOne(ta => ta.Subject)
                .WithMany(sub => sub.TutorAssignments)
                .HasForeignKey(ta => ta.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Sample Data Seeding ---
            modelBuilder.Entity<Major>().HasData(
                new Major { MajorId = 1, MajorName = "Civil Engineering" },
                new Major { MajorId = 2, MajorName = "Mechanical Engineering" },
                new Major { MajorId = 3, MajorName = "Electrical Engineering" },
                new Major { MajorId = 4, MajorName = "Technical Math & Science" },
                new Major { MajorId = 5, MajorName = "Pure Math & Science" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, SubjectName = "Mathematics" },
                new Subject { SubjectId = 2, SubjectName = "Physics" },
                new Subject { SubjectId = 3, SubjectName = "Chemistry" },
                new Subject { SubjectId = 4, SubjectName = "English" },
                new Subject { SubjectId = 5, SubjectName = "Computer Science" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FullNames = "Alice Johnson",
                    Email = "alice@student.com",
                    Password = "password123",
                    Age = 16,
                    DateOfBirth = new DateTime(2009, 2, 15),
                    Address = "123 Main St",
                    DateEnrolled = new DateTime(2024, 1, 15),
                    GuardianName = "Mary Johnson",
                    GuardianContact = "0123456789",
                    GradeLevel = "10",
                    MajorId = 1,
                    Role = "Student"
                },
                new Student
                {
                    StudentId = 2,
                    FullNames = "Bob Smith",
                    Email = "bob@student.com",
                    Password = "password123",
                    Age = 16,
                    DateOfBirth = new DateTime(2009, 6, 10),
                    Address = "456 Oak St",
                    DateEnrolled = new DateTime(2024, 1, 15),
                    GuardianName = "John Smith",
                    GuardianContact = "0987654321",
                    GradeLevel = "10",
                    MajorId = 2,
                    Role = "Student"
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    TeacherId = 1,
                    FullNames = "Mr. James Lee",
                    Email = "james@teacher.com",
                    Password = "teach123",
                    Grade = "10",
                    Role = "Teacher"
                }
            );

            modelBuilder.Entity<Tutor>().HasData(
                new Tutor
                {
                    TutorId = 1,
                    FullNames = "Mr. Kevin Brown",
                    Email = "kevin@tutor.com",
                    Password = "tutor123",
                    Role = "Tutor"
                }
            );
        }
    }
}

