using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Areas.Admin.Controllers.ViewModels;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class TurmasController : Controller
    {
        private ILogRepository _LogRep = new LogRepository();
        private ITurmaRepository turmRep = new TurmaRepository();

        public IActionResult Index()
        {
            try
            {
                IUserRepository useRep = new UserRepository();

                var Turmas = turmRep.GetAll();

                var Model = (from tur in Turmas
                             join use in useRep.GetAll() on tur.CoordenadorId equals use.UserId
                             select new TurmaViewModel
                             {
                                 TormaId = tur.TurmaId,
                                 CoordenadorId = tur.CoordenadorId,
                                 Coordenador = use,
                                 Descricao = tur.Descricao,
                                 Duracao = tur.Duracao,
                                 Name = tur.Nome,
                             }).ToList();

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                //_LogRep.Add(new Log
                //{
                //    Description = Error.Message,
                //    Origin = "Login",
                //    UserChangeId = 1
                //});

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
                    turmRep.Add(Model);
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
                    Origin = "Login",
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
                return View(turmRep.GetById(Id));
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
                    turmRep.Attach(Model);
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
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = turmRep.GetById(Id);

                if (Model != null)
                {
                    turmRep.Remove(Model);

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
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}