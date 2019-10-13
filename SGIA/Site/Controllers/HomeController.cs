using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult MenuLeft()
        {
            using(Contexto bd = new Contexto())
            {
                return View(bd.Menus.Where(a => a.Ativo == true).ToList());
            }
        }

        public IActionResult VersionHistory()
        {
            List<Historico> ListaVersao = new List<Historico>();

            var Versao = new Historico();

            Versao.Versao = "1.0.0";
            Versao.Data = "13/10/2019";

            Versao.Descricao.Add("1 - Foi criado projeto.");
            Versao.Descricao.Add("1 - Foi criado Cruds.");

            ListaVersao.Add(Versao);

            return View(ListaVersao);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}