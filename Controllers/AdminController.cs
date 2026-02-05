using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
