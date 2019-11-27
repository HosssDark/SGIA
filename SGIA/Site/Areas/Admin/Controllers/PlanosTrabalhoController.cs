using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class PlanosTrabalhoController : Controller
    {
        private IPlanoTrabalhoRepository _traRep = new PlanoTrabalhoRepository();
        private IUserRepository _userRep = new UserRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public PlanosTrabalhoController(LoginUser loginUser, IHostingEnvironment appEnvironment)
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
                var Model = _traRep.Grid(Buscar, StatusId, DataInicial, DataFinal, _appEnvironment.WebRootPath);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "PlanosTrabalho",
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
        public IActionResult Adicionar(PlanoTrabalhoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.PlanoTrabalho.UserId == null || Model.PlanoTrabalho.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _traRep.Add(Model.PlanoTrabalho);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "PlanosTrabalho", "PlanoTrabalho", Model.PlanoTrabalho.PlanoTrabalhoId, Info.Extension, _appEnvironment.WebRootPath);
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
                    Origin = "PlanosTrabalho",
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

                var PlanoTrabalho = _traRep.GetById(Id);

                PlanoTrabalhoViewModel Model = new PlanoTrabalhoViewModel()
                {
                    PlanoTrabalho = PlanoTrabalho,
                    Image = imgRep.GetImage(PlanoTrabalho.PlanoTrabalhoId, "images", "PlanosTrabalho", "PlanoTrabalho", _appEnvironment.WebRootPath)
                };

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "PlanosTrabalho",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoTrabalhoViewModel Model)
        {
            try
            {
                #region + Validacao

                if (Model.PlanoTrabalho.UserId == null || Model.PlanoTrabalho.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PlanoTrabalho.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _traRep.Attach(Model.PlanoTrabalho);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "PlanosTrabalho", "PlanoTrabalho", Model.PlanoTrabalho.PlanoTrabalhoId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImage(Model.PlanoTrabalho.PlanoTrabalhoId, "images", "PlanosTrabalho", "PlanoTrabalho", _appEnvironment.WebRootPath);
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
                    Origin = "PlanosTrabalho",
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
                return View(_traRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "PlanosTrabalho",
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
                    Origin = "PlanosTrabalho",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorio(PlanoTrabalhoReportViewModel Model)
        {
            try
            {
                var List = _traRep.Report();

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
                        ViewName = "RelatorioPlanosTrabalho",
                        Model = List.ToList(),
                        PageSize = Size.A4,
                        CustomSwitches = Footer,
                    };

                    return pdf;
                }
                else
                    return View("RelatorioPlanosTrabalho", List.ToList());

            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "PlanosTrabalho",
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
                var Model = _traRep.GetById(Id);

                if (Model != null)
                {
                    _traRep.Remove(Model);

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
                    Origin = "PlanosTrabalho",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}