using System;
using System.IO;
using Domain;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public PlanosEnsinoController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? TurmaId = null, int? DiciplinaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _ensRep.Grid(Buscar, TurmaId, DiciplinaId, StatusId, DataInicial, DataFinal, _appEnvironment.WebRootPath);

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
        public IActionResult Adicionar(PlanoEnsinoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.PlanoEnsino.DiciplinaId == null && Model.PlanoEnsino.DiciplinaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Diciplina", "Obrigatório");

                if (Model.PlanoEnsino.UserId == null && Model.PlanoEnsino.UserId == 0)
                    ModelState.AddModelError("PlanoEnsino_Docente", "Obrigatório");

                if (Model.PlanoEnsino.TurmaId == null && Model.PlanoEnsino.TurmaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Turma", "Obrigatório");

                if (Model.PlanoEnsino.EmentaId == null && Model.PlanoEnsino.EmentaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.ObjetivoArea))
                    ModelState.AddModelError("PlanoEnsino_ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.ObjetivoGeral))
                    ModelState.AddModelError("PlanoEnsino_ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.EspecificacaoConteudo))
                    ModelState.AddModelError("PlanoEnsino_EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.MetodologiaAvaliacao))
                    ModelState.AddModelError("PlanoEnsino_MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.TecnicaPedagogica))
                    ModelState.AddModelError("PlanoEnsino_TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.RecursoUtilizado))
                    ModelState.AddModelError("PlanoEnsino_RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.AtividadeTrabalhada))
                    ModelState.AddModelError("PlanoEnsino_AtividadeTrabalhada", "Obrigatório");

                if (Model.PlanoEnsino.DataEmissao == null && Model.PlanoEnsino.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("PlanoEnsino_DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Add(Model.PlanoEnsino);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "PlanosEnsino", "PlanoEnsino", Model.PlanoEnsino.PlanoEnsinoId, Info.Extension, _appEnvironment.WebRootPath);
                        }
                    }

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
                IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                var PlanoEnsino = _ensRep.GetById(Id);

                PlanoEnsinoViewModel Model = new PlanoEnsinoViewModel()
                {
                    PlanoEnsino = PlanoEnsino,
                    Image = imgRep.GetImage(PlanoEnsino.PlanoEnsinoId, "images", "PlanosEnsino", "PlanoEnsino", _appEnvironment.WebRootPath)
                };

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

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoEnsinoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.PlanoEnsino.DiciplinaId == null && Model.PlanoEnsino.DiciplinaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Diciplina", "Obrigatório");

                if (Model.PlanoEnsino.UserId == null && Model.PlanoEnsino.UserId == 0)
                    ModelState.AddModelError("PlanoEnsino_Docente", "Obrigatório");

                if (Model.PlanoEnsino.TurmaId == null && Model.PlanoEnsino.TurmaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Turma", "Obrigatório");

                if (Model.PlanoEnsino.EmentaId == null && Model.PlanoEnsino.EmentaId == 0)
                    ModelState.AddModelError("PlanoEnsino_Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.ObjetivoArea))
                    ModelState.AddModelError("PlanoEnsino_ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.ObjetivoGeral))
                    ModelState.AddModelError("PlanoEnsino_ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.EspecificacaoConteudo))
                    ModelState.AddModelError("PlanoEnsino_EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.MetodologiaAvaliacao))
                    ModelState.AddModelError("PlanoEnsino_MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.TecnicaPedagogica))
                    ModelState.AddModelError("PlanoEnsino_TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.RecursoUtilizado))
                    ModelState.AddModelError("PlanoEnsino_RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoEnsino.AtividadeTrabalhada))
                    ModelState.AddModelError("PlanoEnsino_AtividadeTrabalhada", "Obrigatório");

                if (Model.PlanoEnsino.DataEmissao == null && Model.PlanoEnsino.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("PlanoEnsino_DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Attach(Model.PlanoEnsino);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "PlanosEnsino", "PlanoEnsino", Model.PlanoEnsino.PlanoEnsinoId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImage(Model.PlanoEnsino.PlanoEnsinoId, "images", "PlanosEnsino", "PlanoEnsino", _appEnvironment.WebRootPath);
                    }

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
        public IActionResult Relatorio(DiciplinaReportViewModel Model)
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