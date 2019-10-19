using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class UsuariosController : Controller
    {
        private IUserRepository _userRep = new UserRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from usu in _userRep.GetAll()
                             join sta in _staRep.GetAll() on usu.StatusId equals sta.StatusId
                             select new UserViewModel
                             {
                                 UserId = usu.UserId,
                                 StatusId = usu.StatusId,
                                 TipoAcessoId = usu.TipoAcesso,
                                 Nome = usu.Nome,
                                 Email = usu.Email,
                                 DataCadastro = usu.DataCadastro,
                                 Status = sta.Descricao,
                             }).ToList();

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return View();
            }
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(User Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Email))
                {

                }
                else
                    ModelState.AddModelError("Email", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _userRep.Add(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Erro");
                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_userRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(User Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Email))
                {

                }
                else
                    ModelState.AddModelError("Email", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _userRep.Attach(Model);
                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _userRep.GetById(Id);

                if (Model != null)
                {
                    _userRep.Remove(Model);

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
            catch (Exception erro)
            {
                return Json(new { Result = "Erro", Message = erro.Message });
            }
        }
    }
}