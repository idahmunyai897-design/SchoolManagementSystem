using System.ComponentModel.DataAnnotations;
#nullable disable
 
namespace SchoolManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
