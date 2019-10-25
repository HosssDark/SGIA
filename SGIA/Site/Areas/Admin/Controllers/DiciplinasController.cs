using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class DiciplinasController : Controller
    {
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from dp in _dicRep.GetAll()
                             join sta in _staRep.GetAll() on dp.StatusId equals sta.StatusId
                             select new DiciplinaViewModel
                             {
                                 DiciplinaId = dp.DiciplinaId,
                                 TurmaId = dp.TurmaId,
                                 StatusId = dp.StatusId,
                                 CargaHoraria = dp.CargaHoraria,
                                 CreditoAtividadeCampo = dp.CreditoAtividadeCampo,
                                 CreditoAtividadePratica = dp.CreditoAtividadePratica,
                                 CreditoEnsino = dp.CreditoEnsino,
                                 Ementa = dp.Ementa,
                                 HoraSemanal = dp.HoraSemanal,
                                 Nome = dp.Nome,
                                 DataCadastro = dp.DataCadastro,
                                 Status = sta.Descricao,
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
        public IActionResult Adicionar(Diciplina Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Ementa))
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.HoraSemanal == null && Model.HoraSemanal >= 0)
                    ModelState.AddModelError("HoraSemanal", "Obrigatório");

                if (Model.CreditoEnsino == null && Model.CreditoEnsino >= 0)
                    ModelState.AddModelError("CreditoEnsino", "Obrigatório");

                if (Model.CreditoAtividadePratica == null && Model.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("CreditoAtividadePratica", "Obrigatório");

                if (Model.CreditoAtividadeCampo == null && Model.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    _dicRep.Add(Model);
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
                return View(_dicRep.GetById(Id));
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
        public IActionResult Alterar(Diciplina Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Ementa))
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.HoraSemanal == null && Model.HoraSemanal >= 0)
                    ModelState.AddModelError("HoraSemanal", "Obrigatório");

                if (Model.CreditoEnsino == null && Model.CreditoEnsino >= 0)
                    ModelState.AddModelError("CreditoEnsino", "Obrigatório");

                if (Model.CreditoAtividadePratica == null && Model.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("CreditoAtividadePratica", "Obrigatório");

                if (Model.CreditoAtividadeCampo == null && Model.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Attach(Model);
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
                return View(_dicRep.GetById(Id));
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
                var Model = _dicRep.GetById(Id);

                if (Model != null)
                {
                    _dicRep.Remove(Model);

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