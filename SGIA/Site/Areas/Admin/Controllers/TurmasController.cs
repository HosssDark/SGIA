using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Site.Areas.Admin.Controllers.ViewModels;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class TurmasController : Controller
    {
        private ITurmaRepository _turmRep = new TurmaRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _turmRep.Grid(Buscar, StatusId, DataInicial, DataFinal);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Turmas",
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
        public IActionResult Adicionar(Turma Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.DataCadastro == null && Model.DataCadastro == DateTime.MinValue)
                    ModelState.AddModelError("DataCadastro", "Obrigatório");

                if (Model.QtdeSemestres == null && Model.QtdeSemestres == 0)
                    ModelState.AddModelError("QtdeSemestres", "Obrigatório");

                if (Model.QtdeSemestres <= 0)
                    ModelState.AddModelError("QtdeSemestres", "Semestres não pode ter valor negativo!");

                if (Model.Duracao == null && Model.Duracao == 0)
                    ModelState.AddModelError("Duracao", "Obrigatório");

                if (Model.Duracao <= 0)
                    ModelState.AddModelError("Duracao", "Duração não pode ter valor negativo!");

                if (Model.CoordenadorId == null && Model.CoordenadorId == 0)
                    ModelState.AddModelError("Coordenador", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _turmRep.Add(Model);
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
                    Origin = "Turmas",
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
                return View(_turmRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Turma Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.DataCadastro == null && Model.DataCadastro == DateTime.MinValue)
                    ModelState.AddModelError("DataCadastro", "Obrigatório");

                if (Model.QtdeSemestres == null && Model.QtdeSemestres == 0)
                    ModelState.AddModelError("QtdeSemestres", "Obrigatório");

                if (Model.QtdeSemestres <= 0)
                    ModelState.AddModelError("QtdeSemestres", "Semestres não pode ter valor negativo!");

                if (Model.Duracao == null && Model.Duracao == 0)
                    ModelState.AddModelError("Duracao", "Obrigatório");

                if (Model.Duracao <= 0)
                    ModelState.AddModelError("Duracao", "Duração não pode ter valor negativo!");

                if (Model.CoordenadorId == null && Model.CoordenadorId == 0)
                    ModelState.AddModelError("Coordenador", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _turmRep.Attach(Model);
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
                    Origin = "Turmas",
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
                    Origin = "Turmas",
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
                var List = _turmRep.Report();

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
                    Origin = "Turmas",
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
                var Model = _turmRep.GetById(Id);

                if (Model != null)
                {
                    _turmRep.Remove(Model);

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
                    Origin = "Turmas",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}