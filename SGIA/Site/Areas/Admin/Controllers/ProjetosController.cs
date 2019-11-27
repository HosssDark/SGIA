using System;
using System.IO;
using System.Linq;
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
    public class ProjetosController : Controller
    {
        private IProjetoRepository _proRep = new ProjetoRepository();
        private ILogRepository _LogRep = new LogRepository();
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public ProjetosController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _proRep.Grid(Buscar, StatusId, DataInicial, DataFinal, _appEnvironment.WebRootPath);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Projetos",
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
        public IActionResult Adicionar(ProjetoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Projeto.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.Projeto.UserId == null || Model.Projeto.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.Projeto.CargaHoraria == null || Model.Projeto.CargaHoraria == 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.Projeto.CargaHoraria <= 0)
                    ModelState.AddModelError("CargaHoraria", "Carga Horária não pode ser negativa!");

                if (Model.Projeto.DataInicio == null || Model.Projeto.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.Projeto.DataTermino == null || Model.Projeto.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _proRep.Add(Model.Projeto);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Projetos", "Projeto", Model.Projeto.ProjetoId, Info.Extension, _appEnvironment.WebRootPath);
                        }
                    }

                    ViewData["Success"] = "Registro gravado com sucesso";

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
                    Origin = "Projetos",
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

                var Projeto = _proRep.GetById(Id);

                ProjetoViewModel Model = new ProjetoViewModel()
                {
                    Projeto = Projeto,
                    Image = imgRep.GetImage(Projeto.ProjetoId, "images", "Projetos", "Projeto", _appEnvironment.WebRootPath)
                };

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Projetos",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(ProjetoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Projeto.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.Projeto.UserId == null || Model.Projeto.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.Projeto.CargaHoraria == null || Model.Projeto.CargaHoraria == 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.Projeto.CargaHoraria <= 0)
                    ModelState.AddModelError("CargaHoraria", "Carga Horária não pode ser negativa!");

                if (Model.Projeto.DataInicio == null || Model.Projeto.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.Projeto.DataTermino == null || Model.Projeto.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _proRep.Attach(Model.Projeto);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Projetos", "Projeto", Model.Projeto.ProjetoId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImage(Model.Projeto.ProjetoId, "images", "Projetos", "Projeto", _appEnvironment.WebRootPath);
                    }

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
                    Origin = "Projetos",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
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
                    Origin = "Projetos",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorio(ProjetoReportViewModel Model)
        {
            try
            {
                var List = _proRep.Report();
                //string Type = Model.DocenteId != 0 ? "RelatorioResponsavel" : "RelatorioProjetos";

                #region + Filters

                if (Model.StatusId != 0)
                    List = List.Where(a => a.StatusId == Model.StatusId);

                if (Model.DocenteId != 0)
                    List = List.Where(a => a.UserId == Model.DocenteId);

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
                        ViewName = "RelatorioProjetos",
                        Model = List.ToList(),
                        PageSize = Size.A4,
                        CustomSwitches = Footer,
                    };

                    return pdf;
                }
                else
                    return View("RelatorioProjetos", List.ToList());

            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Projetos",
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
                var Model = _proRep.GetById(Id);

                if (Model != null)
                {
                    _proRep.Remove(Model);

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
                    Origin = "Projetos",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}