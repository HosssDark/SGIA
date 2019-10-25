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
    public class HorarioAulasController : Controller
    {
        private IHorarioAulaRepository _horRep = new HorarioAulaRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private ITurmaRepository _turRep = new TurmaRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            try
            {
                var Diciplinas = _dicRep.GetAll();

                var Model = (from hr in _horRep.GetAll()
                             join dp1 in Diciplinas on hr.DiciplinaPrimeiroId equals dp1.DiciplinaId into r1
                             from dp1 in r1.DefaultIfEmpty()
                             join dp2 in Diciplinas on hr.DiciplinaSegundoId equals dp2.DiciplinaId into r2
                             from dp2 in r2.DefaultIfEmpty()
                             join dp3 in Diciplinas on hr.DiciplinaTerceiroId equals dp3.DiciplinaId into r3
                             from dp3 in r3.DefaultIfEmpty()
                             join dp4 in Diciplinas on hr.DiciplinaQuartoId equals dp4.DiciplinaId into r4
                             from dp4 in r4.DefaultIfEmpty()
                             join tm in _turRep.GetAll() on hr.HorarioAulaId equals tm.TurmaId into r5
                             from tm in r5.DefaultIfEmpty()
                             join sta in _staRep.GetAll() on hr.StatusId equals sta.StatusId
                             select new HorarioAulaViewModel
                             {
                                 HorarioAulaId = hr.HorarioAulaId,
                                 DiciplinaPrimeiroId = hr.DiciplinaPrimeiroId,
                                 DiciplinaPrimeiro = dp1.Nome,
                                 DiciplinaSegundoId = hr.DiciplinaSegundoId,
                                 DiciplinaSegundo = dp2.Nome,
                                 DiciplinaTerceiroId = hr.DiciplinaTerceiroId,
                                 DiciplinaTerceiro = dp3.Nome,
                                 DiciplinaQuartoId = hr.DiciplinaQuartoId,
                                 DiciplinaQuarto = dp4.Nome,
                                 DataCadastro = hr.DataCadastro,
                                 DiaSemana = hr.DiaSemana,
                                 Periodo = hr.Periodo,
                                 StatusId = hr.StatusId,
                                 Status = sta.Descricao,
                                 TurmaId = hr.TurmaId,
                                 Turma = tm != null ? tm.Nome : ""
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
        public IActionResult Adicionar(HorarioAula Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (Model.DiciplinaPrimeiroId == null && Model.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("DiciplinaPrimeiro", "Obrigatório");

                if (Model.DiciplinaSegundoId == null && Model.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("DiciplinaSegundo", "Obrigatório");

                if (Model.DiciplinaTerceiroId == null && Model.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("DiciplinaTerceiro", "Obrigatório");

                if (Model.DiciplinaQuartoId == null && Model.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("DiciplinaQuarto", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Periodo))
                    ModelState.AddModelError("Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _horRep.Add(Model);
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
                return View(_horRep.GetById(Id));
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
        public IActionResult Alterar(HorarioAula Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (Model.DiciplinaPrimeiroId == null && Model.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("DiciplinaPrimeiro", "Obrigatório");

                if (Model.DiciplinaSegundoId == null && Model.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("DiciplinaSegundo", "Obrigatório");

                if (Model.DiciplinaTerceiroId == null && Model.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("DiciplinaTerceiro", "Obrigatório");

                if (Model.DiciplinaQuartoId == null && Model.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("DiciplinaQuarto", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Periodo))
                    ModelState.AddModelError("Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _horRep.Attach(Model);
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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_horRep.GetById(Id));
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
                var Model = _horRep.GetById(Id);

                if (Model != null)
                {
                    _horRep.Remove(Model);

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