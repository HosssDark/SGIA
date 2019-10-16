using Microsoft.AspNetCore.Mvc;
using Site.Models;
using System.Linq;

namespace Site.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index(int UsuarioId)
        {
            using (Contexto bd = new Contexto())
            {
                var Model = bd.Usuarios.Where(a => a.UsuarioId == UsuarioId).FirstOrDefault();

                return View(Model);
            }
        }

        public IActionResult ConfiguracaoPerfil(int UsuarioId)
        {
            using(Contexto bd = new Contexto())
            {
                var Model = bd.Usuarios.Where(a => a.UsuarioId == UsuarioId).FirstOrDefault();

                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfiguracaoPerfil(Usuario Model)
        {
            using(Contexto bd = new Contexto())
            {
                #region


                #endregion

                if (ModelState.IsValid)
                {

                }

                return View(Model);
            }
        }
    }
}