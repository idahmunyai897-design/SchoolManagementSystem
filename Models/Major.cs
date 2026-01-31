#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Major
    {
        [Key]
        public int MajorId { get; set; }

        [Required, StringLength(100)]
        public string MajorName { get; set; }  // e.g., Civil, Mechanical, Electrical, Technical Math, Pure Math

        // Navigation
        public ICollection<Student> Students { get; set; }  // Grade 10 students who chose this major
    }
}
