using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class EditorasController : Controller
    {
        private IEditoraRepository _ediRep = new EditoraRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from ed in _ediRep.GetAll()
                             join sta in _staRep.GetAll() on ed.StatusId equals sta.StatusId
                             select new EditoraViewModel
                             {
                                 EditoraId = ed.EditoraId,
                                 Nome = ed.Nome,
                                 StatusId = ed.StatusId,
                                 Status = sta.Descricao,
                                 DataCadastro = ed.DataCadastro
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
        public IActionResult Adicionar(Editora Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    _ediRep.Add(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_ediRep.GetById(Id));
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Editora Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ediRep.Attach(Model);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _ediRep.GetById(Id);

                if (Model != null)
                {
                    _ediRep.Remove(Model);

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