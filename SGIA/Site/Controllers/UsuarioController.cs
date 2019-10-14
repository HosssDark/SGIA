using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}