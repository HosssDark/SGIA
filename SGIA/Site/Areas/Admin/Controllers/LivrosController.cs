using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class LivrosController : Controller
    {
        private ILivroRepository _livRep = new LivroRepository();
        private IEditoraRepository _ediRep = new EditoraRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ILogRepository _LogRep = new LogRepository();

        public IActionResult Index()
        {
                return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null, int? EditoraId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _livRep.Grid(Buscar, StatusId, EditoraId, DataInicial, DataFinal);

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Livros",
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
        public IActionResult Adicionar(Livro Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Titulo))
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Autor))
                    ModelState.AddModelError("Autor", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AreaConhecimento))
                    ModelState.AddModelError("AreaConhecimento", "Obrigatório");

                if (Model.DataPublicacao == null && Model.DataPublicacao == DateTime.MinValue)
                    ModelState.AddModelError("DataPublicacao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _livRep.Add(Model);
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
                    Origin = "Livros",
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
                return View(_livRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Livros",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Livro Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Titulo))
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Autor))
                    ModelState.AddModelError("Autor", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AreaConhecimento))
                    ModelState.AddModelError("AreaConhecimento", "Obrigatório");

                if (Model.DataPublicacao == null && Model.DataPublicacao == DateTime.MinValue)
                    ModelState.AddModelError("DataPublicacao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _livRep.Attach(Model);
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
                    Origin = "Livros",
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
                return View(_livRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Livros",
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
                var Model = _livRep.GetById(Id);

                if (Model != null)
                {
                    _livRep.Remove(Model);

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
                    Origin = "Livros",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
                return Json(new { Result = "Erro" });
            }
        }
    }
}