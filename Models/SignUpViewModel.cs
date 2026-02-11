using System.ComponentModel.DataAnnotations;
#nullable disable
namespace SchoolManagementSystem.Model
{
    public class SignUpViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Role { get; set; } // Student, Teacher, Tutor
    }
}
