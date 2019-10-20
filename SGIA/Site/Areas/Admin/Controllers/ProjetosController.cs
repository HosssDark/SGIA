using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjetosController : Controller
    {
        private IProjetoRepository _proRep = new ProjetoRepository();
        private IUserRepository _userRep = new UserRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from pj in _proRep.GetAll()
                             join use in _userRep.GetAll() on pj.UserId equals use.UserId
                             join sta in _staRep.GetAll() on pj.StatusId equals sta.StatusId
                             select new ProjetoViewModel
                             {
                                 ProjetoId = pj.ProjetoId,
                                 UserId = pj.UserId,
                                 StatusId = pj.StatusId,
                                 Docente = use.Nome,
                                 Nome = pj.Nome,
                                 Status = sta.Descricao,
                                 CargaHoraria = pj.CargaHoraria,
                                 DataCadastro = pj.DataCadastro,
                                 DataInicio = pj.DataInicio,
                                 DataTermino = pj.DataTermino
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
        public IActionResult Adicionar(Projeto Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.DataInicio == null && Model.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.DataTermino == null && Model.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _proRep.Add(Model);

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
                return View(_proRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Projeto Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.DataInicio == null && Model.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.DataTermino == null && Model.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _proRep.Attach(Model);

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
                var Model = _proRep.GetById(Id);

                if (Model != null)
                {
                    _proRep.Remove(Model);

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