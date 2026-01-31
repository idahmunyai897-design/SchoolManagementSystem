#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }  // store hashed password

        [Required, StringLength(100)]
        public string FullNames { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Qualification { get; set; }

        [StringLength(100)]
        public string Specialization { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateJoined { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required, StringLength(10)]
        public string Grade { get; set; }
        [StringLength(50)]
        public string Role { get; set; } = "Teacher";

        // Navigation
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
    }
}
