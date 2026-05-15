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

            modelBuilder.Entity<Subject>().HasData(
                // Grade 8 - 12 Compa...
                new Subject { SubjectId = 1, SubjectName = "Tshivenda", GradeFrom = 8, GradeTo = 12, RequiresTrack = false },
                new Subject { SubjectId = 2, SubjectName = "English", GradeFrom = 8, GradeTo = 12, RequiresTrack = false },
                new Subject { SubjectId = 3, SubjectName = "Life Orientation", GradeFrom = 8, GradeTo = 12, RequiresTrack = false },

                // Grade 8 - 9
                new Subject { SubjectId = 4, SubjectName = "History", GradeFrom = 8, GradeTo = 9, RequiresTrack = false },
                new Subject { SubjectId = 5, SubjectName = "Creative Arts", GradeFrom = 8, GradeTo = 9, RequiresTrack = false },
                new Subject { SubjectId = 6, SubjectName = "Geography", GradeFrom = 8, GradeTo = 9, RequiresTrack = false },
                new Subject { SubjectId = 7, SubjectName = "Natural Science", GradeFrom = 8, GradeTo = 9, RequiresTrack = false },
                new Subject { SubjectId = 8, SubjectName = "Mathematics", GradeFrom = 8, GradeTo = 9, RequiresTrack = true },

                // Grade 10 - 12
                new Subject { SubjectId = 9, SubjectName = "Pure Mathematics", GradeFrom = 10, GradeTo = 12, RequiresTrack = true },
                new Subject { SubjectId = 10, SubjectName = "Technical Mathematics", GradeFrom = 10, GradeTo = 12, RequiresTrack = true },
                new Subject { SubjectId = 11, SubjectName = "Pysical Science", GradeFrom = 10, GradeTo = 12, RequiresTrack = true },
                new Subject { SubjectId = 12, SubjectName = "EGD", GradeFrom = 10, GradeTo = 12, RequiresTrack = false },


                // --- Engineering Majors ---
                new Subject { SubjectId = 13, SubjectName = "Civil Technology", GradeFrom = 10, GradeTo = 12, RequiresTrack = true },
                new Subject { SubjectId = 14, SubjectName = "Mechanical Technology", GradeFrom = 10, GradeTo = 12, RequiresTrack = true },
                new Subject { SubjectId = 15, SubjectName = "Electrical Technology", GradeFrom = 10, GradeTo = 12, RequiresTrack = true }
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
        }
    }
}

