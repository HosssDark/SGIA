using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class AtribuicoesController : Controller
    {
        private IAtribuicaoRepository _atrRep = new AtribuicaoRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private IProjetoRepository _proRep = new ProjetoRepository();
        private IUserRepository _userRep = new UserRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from at in _atrRep.GetAll()
                             join sta in _staRep.GetAll() on at.StatusId equals sta.StatusId
                             join dp in _dicRep.GetAll() on at.DiciplinaId equals dp.DiciplinaId
                             join pj in _proRep.GetAll() on at.ProjetoId equals pj.ProjetoId
                             join dc in _userRep.GetAll() on at.UserId equals dc.UserId
                             select new AtribuicaoViewModel
                             {
                                 AtribuicaoId = at.AtribuicaoId,
                                 DiciplinaId = at.DiciplinaId,
                                 ProjetoId = at.ProjetoId,
                                 DocenteId = at.UserId,
                                 DataCadastro = at.DataCadastro,
                                 Diciplina = dp.Nome,
                                 Projeto = pj.Nome,
                                 StatusId = at.StatusId,
                                 Status = sta.Descricao,
                                 Docente = dc.Nome
                             }).ToList();

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                ViewData["Error"] = "Erro ao Obter Registro";
                return View();
            }
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Atribuicao Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Dicip", "Obrigatório");

                if (Model.ProjetoId == null && Model.ProjetoId == 0)
                    ModelState.AddModelError("Projet", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    _atrRep.Add(Model);
                    TempData["Success"] = "Registro gravado com sucesso";

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Gravar Registro!";
                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_atrRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Atribuicao Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Dicip", "Obrigatório");

                if (Model.ProjetoId == null && Model.ProjetoId == 0)
                    ModelState.AddModelError("Projet", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.StatusId == null && Model.StatusId == 0)
                    ModelState.AddModelError("Status", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _atrRep.Attach(Model);
                    TempData["Success"] = "Registro alterado com sucesso";

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _atrRep.GetById(Id);

                if (Model != null)
                {
                    _atrRep.Remove(Model);

                    ViewData["Error"] = "Registro excluido com sucesso";
                    return Json(new { Result = "OK" });
                }

                TempData["Error"] = "Registro não encontrado!";
                return Json(new { Result = "Erro" });
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}