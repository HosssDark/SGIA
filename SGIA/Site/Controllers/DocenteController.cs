using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class DocenteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}