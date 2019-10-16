using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.ViewModels;
using System.Linq;

namespace Site.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Validacao(LoginViewModel Model)
        {
            using (Contexto bd = new Contexto())
            {
                var User = bd.Usuarios.Where(s => s.Email == Model.Email);

                if (User.Any())
                {
                    if (User.Where(s => s.Senha == Model.Password).Any())
                    {
                        var Usuario = bd.Usuarios.Where(s => s.Email == Model.Email).FirstOrDefault();

                        return Json(new { Result = "OK", nome = Usuario.Nome, email = Usuario.Email, tipo = Usuario.Tipo, usuarioid = Usuario.UsuarioId });
                    }
                    else
                        return Json(new { Result = "Error", Message = "Senha Inválida!" });
                }
                else
                    return Json(new { Result = "Error", Message = "Email Inválido!" });
            }
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }
    }
}