using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.IO;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class DiciplinasController : Controller
    {
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private ILogRepository _LogRep = new LogRepository();
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public DiciplinasController(LoginUser loginUser, IHostingEnvironment appEnvironment)
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
                var Model = _dicRep.Grid(Buscar, StatusId, DataInicial, DataFinal, _appEnvironment.WebRootPath);

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
        public IActionResult Adicionar(DiciplinaViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Diciplina.Nome))
                    ModelState.AddModelError("Diciplina_Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Diciplina.Ementa))
                    ModelState.AddModelError("Diciplina_Ementa", "Obrigatório");

                if (Model.Diciplina.CargaHoraria == null && Model.Diciplina.CargaHoraria >= 0)
                    ModelState.AddModelError("Diciplina_CargaHoraria", "Obrigatório");

                if (Model.Diciplina.HoraSemanal == null && Model.Diciplina.HoraSemanal >= 0)
                    ModelState.AddModelError("Diciplina_HoraSemanal", "Obrigatório");

                if (Model.Diciplina.CreditoEnsino == null && Model.Diciplina.CreditoEnsino >= 0)
                    ModelState.AddModelError("Diciplina_CreditoEnsino", "Obrigatório");

                if (Model.Diciplina.CreditoAtividadePratica == null && Model.Diciplina.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("Diciplina_CreditoAtividadePratica", "Obrigatório");

                if (Model.Diciplina.CreditoAtividadeCampo == null && Model.Diciplina.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("Diciplina_CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Add(Model.Diciplina);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Diciplinas", "Diciplina", Model.Diciplina.DiciplinaId, Info.Extension, _appEnvironment.WebRootPath);
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
                IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                var Diciplina = _dicRep.GetById(Id);

                DiciplinaViewModel Model = new DiciplinaViewModel()
                {
                    Diciplina = Diciplina,
                    Image = imgRep.GetImage(Diciplina.DiciplinaId, "images", "Diciplinas", "Diciplina", _appEnvironment.WebRootPath)
                };

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

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(DiciplinaViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Diciplina.Nome))
                    ModelState.AddModelError("Diciplina_Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Diciplina.Ementa))
                    ModelState.AddModelError("Diciplina_Ementa", "Obrigatório");

                if (Model.Diciplina.CargaHoraria == null && Model.Diciplina.CargaHoraria >= 0)
                    ModelState.AddModelError("Diciplina_CargaHoraria", "Obrigatório");

                if (Model.Diciplina.HoraSemanal == null && Model.Diciplina.HoraSemanal >= 0)
                    ModelState.AddModelError("Diciplina_HoraSemanal", "Obrigatório");

                if (Model.Diciplina.CreditoEnsino == null && Model.Diciplina.CreditoEnsino >= 0)
                    ModelState.AddModelError("Diciplina_CreditoEnsino", "Obrigatório");

                if (Model.Diciplina.CreditoAtividadePratica == null && Model.Diciplina.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("Diciplina_CreditoAtividadePratica", "Obrigatório");

                if (Model.Diciplina.CreditoAtividadeCampo == null && Model.Diciplina.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("Diciplina_CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Attach(Model.Diciplina);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Diciplinas", "Diciplina", Model.Diciplina.DiciplinaId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImage(Model.Diciplina.DiciplinaId, "images", "Diciplinas", "Diciplina", _appEnvironment.WebRootPath);
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
        public IActionResult Relatorio(DiciplinaReportViewModel Model)
        {
            try
            {
                var List = _dicRep.Report();

                #region + Filters

                if (Model.StatusId != 0)
                    List = List.Where(a => a.StatusId == Model.StatusId);

                if (Model.TurmaId != 0)
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