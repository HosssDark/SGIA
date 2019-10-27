using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class DiciplinasController : Controller
    {
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _dicRep.Grid(Buscar, StatusId, DataInicial, DataFinal);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Diciplinas",
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
                    Origin = "Diciplinas",
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
                    Origin = "Diciplinas",
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
                    Origin = "Diciplinas",
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
                    Origin = "Diciplinas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Relatorio()
        {
            try
            {
                return View();
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Diciplinas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorio(DiciplinaRelatorioViewModel Model)
        {
            try
            {
                var List = _dicRep.Report();

                #region + Filters

                if (Model.StatusId != 0)
                    List = List.Where(a => a.StatusId == Model.StatusId);

                if(Model.TurmaId != 0)
                    List = List.Where(a => a.TurmaId == Model.TurmaId);

                if (Model.DataInicial != null)
                    List = List.Where(a => a.DataCadastro >= Model.DataInicial);

                if (Model.DataFinal != null)
                    List = List.Where(a => a.DataCadastro <= Model.DataFinal);

                #endregion

                string Footer = "--outline --margin-bottom 15  --footer-right \"Página [page]/[toPage]\" --footer-font-size \"9\" --footer-spacing 4 ";

                if (Model.Formato == "pdf")
                {
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "",
                        Model = List,
                        PageSize = Size.A4,
                        CustomSwitches = Footer,
                    };

                    return pdf;
                }
                else
                    return View("", List);

            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Diciplinas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
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
                    Origin = "Diciplinas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}