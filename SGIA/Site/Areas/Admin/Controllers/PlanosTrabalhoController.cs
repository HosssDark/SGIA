using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
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

        public IActionResult Index()
        {
            try
            {
                var Model = (from pl in _traRep.GetAll()
                             join use in _userRep.GetAll() on pl.UserId equals use.UserId
                             join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                             select new PlanoTrabalhoViewModel
                             {
                                 PlanoTrabalhoId = pl.PlanoTrabalhoId,
                                 UserId = pl.UserId,
                                 StatusId = pl.StatusId,
                                 DataCadastro = pl.DataCadastro,
                                 DescricaoAtividade = pl.DescricaoAtividade,
                                 DiaSemana = pl.DiaSemana,
                                 Docente = use.Nome,
                                 HoraEncerramento = pl.HoraEncerramento,
                                 HoraInicio = pl.HoraInicio,
                                 Status = sta.Descricao
                             }).ToList();

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
        public IActionResult Adicionar(PlanoTrabalho Model)
        {
            try
            {
                #region + Validacao

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _traRep.Add(Model);
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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Alterar(int Id)
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
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoTrabalho Model)
        {
            try
            {
                #region + Validacao

                if (Model.UserId == null && Model.UserId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _traRep.Attach(Model);
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
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
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