using Domain;
using Functions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class DicentesController : Controller
    {
        private IDicenteRepository _dicRep = new DicenteRepository();
        private ILogRepository _LogRep = new LogRepository();
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public DicentesController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, 
                                  DateTime? DataFinal = null)
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
                    Origin = "Dicentes",
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
        public IActionResult Adicionar(DicenteViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.Dicente.Matricula == null || Model.Dicente.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Dicente.Nome))
                    ModelState.AddModelError("Dicente_Nome", "Obrigatório");

                if (!string.IsNullOrEmpty(Model.Dicente.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Dicente.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Add(Model.Dicente);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Dicentes", "Dicente", Model.Dicente.DicenteId, Info.Extension, _appEnvironment.WebRootPath);
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
                    Origin = "Dicentes",
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
                IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                var Dicente = _dicRep.GetById(Id);

                DicenteViewModel Model = new DicenteViewModel()
                {
                    Dicente = Dicente,
                    Image = imgRep.GetImage(Dicente.DicenteId, "images", "Dicentes", "Dicente", _appEnvironment.WebRootPath)
                };

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Dicentes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(DicenteViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.Dicente.Matricula == null || Model.Dicente.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Dicente.Nome))
                    ModelState.AddModelError("Dicente_Nome", "Obrigatório");

                if (!string.IsNullOrEmpty(Model.Dicente.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Dicente.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }

                #endregion

                if (ModelState.IsValid)
                {
                    _dicRep.Attach(Model.Dicente);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Dicentes", "Dicente", Model.Dicente.DicenteId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImageUser(Model.Dicente.DicenteId, "images", "Dicentes", "Dicente", _appEnvironment.WebRootPath);
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
                    Origin = "Dicentes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return View(Model);
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
                    Origin = "Dicentes",
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
                    Origin = "Dicentes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorio(DicenteReportViewModel Model)
        {
            try
            {
                var List = _dicRep.Report();

                #region + Filters

                if (Model.StatusId != 0)
                    List = List.Where(a => a.StatusId == Model.StatusId);

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
                        ViewName = "RelatorioDicentes",
                        Model = List.ToList(),
                        PageSize = Size.A4,
                        CustomSwitches = Footer,
                    };

                    return pdf;
                }
                else
                    return View("RelatorioDicentes", List.ToList());

            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Dicentes",
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
                    Origin = "Dicentes",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}