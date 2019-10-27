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
    public class PlanosEnsinoController : Controller
    {
        private IPlanoEnsinoRepository _ensRep = new PlanoEnsinoRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? TurmaId = null, int? DiciplinaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _ensRep.Grid(Buscar, TurmaId, DiciplinaId, StatusId, DataInicial, DataFinal);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
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
                var List = _ensRep.Report();

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
                    Origin = "PlanosEnsino",
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
                    Origin = "PlanosEnsino",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}