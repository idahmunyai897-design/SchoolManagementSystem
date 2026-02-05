using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class TutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
