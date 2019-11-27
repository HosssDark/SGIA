using System;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

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
                return View();
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Atribuicoes",
                    UserChangeId = 1
                });

                #endregion

                ViewData["Error"] = "Erro ao Obter Registro";
                return View();
            }
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, int? DiciplinaId = null, int? ProjetoId = null, 
                                  int? DocenteId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _atrRep.Grid(Buscar, StatusId, DiciplinaId, ProjetoId, DocenteId, DataInicial, DataFinal);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Atribuicoes",
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

                if (Model.DiciplinaId == null || Model.DiciplinaId == 0)
                    ModelState.AddModelError("Dicip", "Obrigatório");

                if (Model.ProjetoId == null || Model.ProjetoId == 0)
                    ModelState.AddModelError("Projet", "Obrigatório");

                if (Model.UserId == null || Model.UserId == 0)
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
                    Origin = "Atribuicoes",
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
                    Origin = "Atribuicoes",
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

                if (Model.DiciplinaId == null || Model.DiciplinaId == 0)
                    ModelState.AddModelError("Dicip", "Obrigatório");

                if (Model.ProjetoId == null || Model.ProjetoId == 0)
                    ModelState.AddModelError("Projet", "Obrigatório");

                if (Model.UserId == null || Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

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
                    Origin = "Atribuicoes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return View(Model);
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
                    Origin = "Atribuicoes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorio(AtribuicaoReportViewModel Model)
        {
            try
            {
                var List = _atrRep.Report();

                #region + Filters

                if (Model.StatusId != 0)
                    List = List.Where(a => a.StatusId == Model.StatusId);

                //if (Model.TurmaId != 0)
                //    List = List.Where(a => a.TurmaId == Model.TurmaId);

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
                        ViewName = "RelatorioAtribuicoes",
                        Model = List.ToList(),
                        PageSize = Size.A4,
                        CustomSwitches = Footer,
                    };

                    return pdf;
                }
                else
                    return View("RelatorioAtribuicoes", List.ToList());

            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Atribuicoes",
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
                    Origin = "Atribuicoes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}