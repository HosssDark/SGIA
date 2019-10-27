using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

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
            return View();
        }

        public IActionResult Grid(string Periodo = null, int? TurmaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _horRep.Grid(Periodo, TurmaId, StatusId, DataInicial, DataFinal);
            
                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
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
                var List = _horRep.Report();

                #region + Filters

                //if (Model.StatusId != 0)
                //    List = List.Where(a => a.StatusId == Model.StatusId);

                //if (Model.TurmaId != 0)
                //    List = List.Where(a => a.TurmaId == Model.TurmaId);

                //if (Model.DataInicial != null)
                //    List = List.Where(a => a.DataCadastro >= Model.DataInicial);

                //if (Model.DataFinal != null)
                //    List = List.Where(a => a.DataCadastro <= Model.DataFinal);

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
                    Origin = "HorarioAulas",
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
                    Origin = "HorarioAulas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}