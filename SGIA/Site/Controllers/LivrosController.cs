using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class LivrosController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from lv in bd.Livros
                             join sta in bd.Status on lv.StatusId equals sta.StatusId
                             join ed in bd.Editoras on lv.EditoraId equals ed.EditoraId
                             select new LivroViewModel
                             {
                                 LivroId = lv.LivroId,
                                 AreaConhecimento = lv.AreaConhecimento,
                                 Autor = lv.Autor,
                                 DataCadastro = lv.DataCadastro,
                                 DataPublicacao = lv.DataPublicacao,
                                 EditoraId = lv.EditoraId,
                                 Editora = ed.Nome,
                                 StatusId = lv.StatusId,
                                 Status = sta.Descricao,
                                 Titulo = lv.Titulo
                             }).ToList();

                return View(Model);
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
                #region + Validacao

                if(string.IsNullOrEmpty(Model.Titulo))
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Autor))
                    ModelState.AddModelError("Autor", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AreaConhecimento))
                    ModelState.AddModelError("AreaConhecimento", "Obrigatório");

                if (Model.DataPublicacao == null && Model.DataPublicacao == DateTime.MinValue)
                    ModelState.AddModelError("DataPublicacao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.Livros.Add(Model);
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                return View(bd.Livros.Where(a => a.LivroId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Livro Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Titulo))
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Autor))
                    ModelState.AddModelError("Autor", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AreaConhecimento))
                    ModelState.AddModelError("AreaConhecimento", "Obrigatório");

                if (Model.DataPublicacao == null && Model.DataPublicacao == DateTime.MinValue)
                    ModelState.AddModelError("DataPublicacao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.Livros.Attach(Model);
                    bd.Entry(Model).State = EntityState.Modified;
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
        }

        public IActionResult Detalhes(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                return View(bd.Livros.Where(a => a.LivroId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                var Model = bd.Livros.Where(a => a.LivroId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Livros.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!"});
            }
        }
    }
}