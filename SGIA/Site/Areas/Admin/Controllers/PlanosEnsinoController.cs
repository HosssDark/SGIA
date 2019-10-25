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
    public class PlanosEnsinoController : Controller
    {
        private IPlanoEnsinoRepository _ensRep = new PlanoEnsinoRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private IUserRepository _userRep = new UserRepository();
        private ITurmaRepository _turRep = new TurmaRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from pl in _ensRep.GetAll()
                             join dp in _dicRep.GetAll() on pl.DiciplinaId equals dp.DiciplinaId
                             join use in _userRep.GetAll() on pl.UserId equals use.UserId
                             join tm in _turRep.GetAll() on pl.TurmaId equals tm.TurmaId
                             join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                             select new PlanoEnsinoViewModel
                             {
                                 PlanoEnsinoId = pl.PlanoEnsinoId,
                                 BiografiaBasicaId = pl.BiografiaBasicaId,
                                 TurmaId = pl.TurmaId,
                                 UserId = pl.UserId,
                                 DiciplinaId = pl.DiciplinaId,
                                 StatusId = pl.StatusId,
                                 BiografiaComplementarId = pl.BiografiaComplementarId,
                                 EmentaId = pl.EmentaId,
                                 AtividadeTrabalhada = pl.AtividadeTrabalhada,
                                 DataCadastro = pl.DataCadastro,
                                 DataEmissao = pl.DataEmissao,
                                 Diciplina = dp.Nome,
                                 Docente = use.Nome,
                                 EspecificacaoConteudo = pl.EspecificacaoConteudo,
                                 MetodologiaAvaliacao = pl.MetodologiaAvaliacao,
                                 ObjetivoArea = pl.ObjetivoArea,
                                 ObjetivoGeral = pl.ObjetivoGeral,
                                 RecursoUtilizado = pl.RecursoUtilizado,
                                 Status = sta.Descricao,
                                 TecnicaPedagogica = pl.TecnicaPedagogica,
                                 Turma = tm.Nome
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

                TempData["Error"] = "Erro ao Obter Registro";
                return View();
            }
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(PlanoEnsino Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (Model.EmentaId == null && Model.EmentaId == 0)
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoArea))
                    ModelState.AddModelError("ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoGeral))
                    ModelState.AddModelError("ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EspecificacaoConteudo))
                    ModelState.AddModelError("EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.MetodologiaAvaliacao))
                    ModelState.AddModelError("MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.TecnicaPedagogica))
                    ModelState.AddModelError("TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.RecursoUtilizado))
                    ModelState.AddModelError("RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AtividadeTrabalhada))
                    ModelState.AddModelError("AtividadeTrabalhada", "Obrigatório");

                if (Model.DataEmissao == null && Model.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Add(Model);
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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_ensRep.GetById(Id));
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

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoEnsino Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (Model.EmentaId == null && Model.EmentaId == 0)
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoArea))
                    ModelState.AddModelError("ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoGeral))
                    ModelState.AddModelError("ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EspecificacaoConteudo))
                    ModelState.AddModelError("EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.MetodologiaAvaliacao))
                    ModelState.AddModelError("MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.TecnicaPedagogica))
                    ModelState.AddModelError("TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.RecursoUtilizado))
                    ModelState.AddModelError("RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AtividadeTrabalhada))
                    ModelState.AddModelError("AtividadeTrabalhada", "Obrigatório");

                if (Model.DataEmissao == null && Model.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Attach(Model);
                    ViewData["Success"] = "Registro alterado com sucesso";

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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_ensRep.GetById(Id));
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

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _ensRep.GetById(Id);

                if (Model != null)
                {
                    _ensRep.Remove(Model);

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
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