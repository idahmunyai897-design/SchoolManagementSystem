#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }  // store hashed password

        [Required, StringLength(100)]
        public string FullNames { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, Range(12, 20)]
        public int Age { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateEnrolled { get; set; }

        [Required, StringLength(100)]
        public string GuardianName { get; set; }

        [Required, Phone]
        public string GuardianContact { get; set; }

        [Required, StringLength(10)]
        public string GradeLevel { get; set; }
        [StringLength(50)]
        public string Role { get; set; } = "Student";
        public bool IsDeleted { get; set; } = false; // false = active, true = deleted
                                                     // Track selections (Grade 10–12)
        [StringLength(50)]
        public string MathTrack { get; set; }   // Pure Math / Technical Math

        [StringLength(50)]
        public string ScienceTrack { get; set; } // Pure Science / Technical Science



        // Grade 10 Major / Track
        public int? MajorId { get; set; }  // Nullable for students not in Grade 10 yet
        public Major Major { get; set; }

        // Navigation
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
        public ICollection<TutorAssignment> TutorAssignments { get; set; }

        // Optional: PeerHelperAssignments
        public ICollection<StudentSubjectPerformance> PeerHelperAssignments { get; set; }
    }
}
