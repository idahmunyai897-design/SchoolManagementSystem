#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }

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

        // Navigation
        public ICollection<TutorAssignment> TutorAssignments { get; set; }
    }
}
