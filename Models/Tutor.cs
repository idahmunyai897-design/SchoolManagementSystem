#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }
        public string FullNames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public DateTime DateJoined { get; set; }
        public string Address { get; set; }

        //Navigation Property
        public ICollection<TutorAssignment> TutorAssignments { get; set; }

    }
}
