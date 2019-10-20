using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LivrosController : Controller
    {
        private ILivroRepository _livRep = new LivroRepository();
        private IEditoraRepository _ediRep = new EditoraRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from lv in _livRep.GetAll()
                             join ed in _ediRep.GetAll() on lv.EditoraId equals ed.EditoraId
                             join sta in _staRep.GetAll() on lv.StatusId equals sta.StatusId
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
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
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
            try
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
                    _livRep.Add(Model);
                    
                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_livRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Livro Model)
        {
            try
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
                    _livRep.Attach(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_livRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _livRep.GetById(Id);

                if (Model != null)
                {
                    _livRep.Remove(Model);

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
            catch (Exception error)
            {
                return Json(new { Result = "Erro", Message = error.Message });
            }
        }
    }
}