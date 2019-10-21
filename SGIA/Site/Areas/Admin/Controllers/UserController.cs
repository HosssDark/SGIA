using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class UserController : Controller
    {
        private IUserRepository _userRep = new UserRepository();
        private IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
        private ITituloRepository _titRep = new TituloRepository();
        private ITipoDocenteRepository _tipRep = new TipoDocenteRepository();
        private ITipoAcessoRepository _aceRep = new TipoAcessoRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from use in _userRep.GetAll()
                             join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId
                             join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId
                             join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId
                             join ace in _aceRep.GetAll() on use.TipoAcessoId equals ace.TipoAcessoId
                             join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                             select new UserViewModel
                             {
                                 User = use,
                                 AreaAtuacao = at.Descricao,
                                 Tipo = tp.Descricao,
                                 TipoAcesso = ace.Descricao,
                                 Titulo = tl.Descricao,
                                 Status = sta.Descricao,
                                 Classe = sta.Classe,
                                 Cor = sta.Cor,
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

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_userRep.GetById(Id));
            }
            catch (Exception erro)
            {
                ViewData["Error"] = erro.Message;
                return RedirectToAction("Index");
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