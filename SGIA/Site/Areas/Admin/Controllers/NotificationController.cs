using Microsoft.AspNetCore.Mvc;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetNotification()
        {
            return View();
        }
    }
}