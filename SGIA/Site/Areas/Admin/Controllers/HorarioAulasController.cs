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
    public class HorarioAulasController : Controller
    {
        private IHorarioAulaRepository _horRep = new HorarioAulaRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private ITurmaRepository _turRep = new TurmaRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();
        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public HorarioAulasController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Periodo = null, int? TurmaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _horRep.Grid(Periodo, TurmaId, StatusId, DataInicial, DataFinal, _appEnvironment.WebRootPath);
            
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
        public IActionResult Adicionar(HorarioAulaViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.HorarioAula.DiaSemana))
                    ModelState.AddModelError("HorarioAula_DiaSemana", "Obrigatório");

                if (Model.HorarioAula.DiciplinaPrimeiroId == null && Model.HorarioAula.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaPrimeiro", "Obrigatório");

                if (Model.HorarioAula.DiciplinaSegundoId == null && Model.HorarioAula.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaSegundo", "Obrigatório");

                if (Model.HorarioAula.DiciplinaTerceiroId == null && Model.HorarioAula.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaTerceiro", "Obrigatório");

                if (Model.HorarioAula.DiciplinaQuartoId == null && Model.HorarioAula.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaQuarto", "Obrigatório");

                if (Model.HorarioAula.TurmaId == null && Model.HorarioAula.TurmaId == 0)
                    ModelState.AddModelError("HorarioAula_Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HorarioAula.Periodo))
                    ModelState.AddModelError("HorarioAula_Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _horRep.Add(Model.HorarioAula);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "HorarioAulas", Model.File.FileName, _LoginUser.GetUser().UserId, Info.Extension, _appEnvironment.WebRootPath);
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
                HorarioAulaViewModel Model = new HorarioAulaViewModel()
                {
                    HorarioAula = _horRep.GetById(Id)
                };

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

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(HorarioAulaViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.HorarioAula.DiaSemana))
                    ModelState.AddModelError("HorarioAula_DiaSemana", "Obrigatório");

                if (Model.HorarioAula.DiciplinaPrimeiroId == null && Model.HorarioAula.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaPrimeiro", "Obrigatório");

                if (Model.HorarioAula.DiciplinaSegundoId == null && Model.HorarioAula.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaSegundo", "Obrigatório");

                if (Model.HorarioAula.DiciplinaTerceiroId == null && Model.HorarioAula.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaTerceiro", "Obrigatório");

                if (Model.HorarioAula.DiciplinaQuartoId == null && Model.HorarioAula.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("HorarioAula_DiciplinaQuarto", "Obrigatório");

                if (Model.HorarioAula.TurmaId == null && Model.HorarioAula.TurmaId == 0)
                    ModelState.AddModelError("HorarioAula_Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HorarioAula.Periodo))
                    ModelState.AddModelError("HorarioAula_Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _horRep.Attach(Model.HorarioAula);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "HorarioAulas", Model.File.FileName, _LoginUser.GetUser().UserId, Info.Extension, _appEnvironment.WebRootPath);
                        }
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
        public IActionResult Relatorio(DiciplinaReportViewModel Model)
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