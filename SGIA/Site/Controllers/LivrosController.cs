using Microsoft.AspNetCore.Mvc;
using Site.Models;
using System;

namespace Site.Controllers
{
    public class LivrosController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                Livro Model = new Livro
                {
                    AreaConhecimento = "Ciencia da Computacao",
                    Autor = "Elias Teste",
                    DataCadastro = DateTime.Now,
                    EditoraId = 1,
                    StatusId = 1,
                    DataPublicacao = DateTime.Now,
                    Titulo = "Teste"
                };

                bd.Livros.Add(Model);
                bd.SaveChanges();

                return View();
            }
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Livro Model)
        {
            using (Contexto bd = new Contexto())
            {

                return View();
            }
        }

        public IActionResult Alterar(int Id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Livro Model)
        {
            return View();
        }

        public JsonResult Excluir(int Id)
        {
            return Json(new { });
        }
    }
}