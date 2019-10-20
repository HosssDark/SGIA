using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DicentesController : Controller
    {
        private IDicenteRepository _dicRep = new DicenteRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from dc in _dicRep.GetAll()
                             join sta in _staRep.GetAll() on dc.StatusId equals sta.StatusId
                             select new DicenteViewModel
                             {
                                 DicenteId = dc.DicenteId,
                                 Matricula = dc.Matricula,
                                 Nome = dc.Nome,
                                 Email = dc.Email,
                                 Telefone = dc.Telefone,
                                 Celular = dc.Celular,
                                 DataCadastro = dc.DataCadastro,
                                 StatusId = dc.StatusId,
                                 Status = sta.Descricao,
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
        public IActionResult Adicionar(Dicente Model)
        {
            try
            {
                #region + Validacao

                if (Model.Matricula == null && Model.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Add(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_dicRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Dicente Model)
        {
            try
            {
                #region + Validacao

                if (Model.Matricula == null && Model.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    //Model.UsuarioAlterouId = 
                    _dicRep.Attach(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _dicRep.GetById(Id);

                if (Model != null)
                {
                    _dicRep.Remove(Model);

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