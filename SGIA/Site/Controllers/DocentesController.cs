using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class DocentesController : Controller
    {
        private IDocenteRepository _docRep = new DocenteRepository();
        private IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ITituloRepository _titRep = new TituloRepository();
        private ITipoDocenteRepository _tipRep = new TipoDocenteRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from dc in _docRep.GetAll()
                             join at in _areRep.GetAll() on dc.AreaAtuacaoId equals at.AreaAtuacaoId
                             join tl in _titRep.GetAll() on dc.TituloId equals tl.TituloId
                             join tp in _tipRep.GetAll() on dc.TipoId equals tp.TipoDocenteId
                             join sta in _staRep.GetAll() on dc.StatusId equals sta.StatusId
                             select new DocenteViewModel
                             {
                                 DocenteId = dc.DocenteId,
                                 AreaAtuacaoId = dc.AreaAtuacaoId,
                                 AreaAtuacao = at.Descricao,
                                 TipoId = dc.TipoId,
                                 Tipo = tp.Descricao,
                                 TituloId = dc.TituloId,
                                 Titulo = tl.Descricao,
                                 CargaHoraria = dc.CargaHoraria,
                                 Celular = dc.Celular,
                                 DataNascimento = dc.DataNascimento,
                                 DataPosse = dc.DataPosse,
                                 Email = dc.Email,
                                 EmailLattes = dc.EmailLattes,
                                 Nome = dc.Nome,
                                 Telefone = dc.Telefone,
                                 DataCadastro = dc.DataCadastro,
                                 StatusId = dc.StatusId,
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
        public IActionResult Adicionar(Docente Model)
        {
            try
            {
                #region + Validacao

                if (Model.AreaAtuacaoId == null && Model.AreaAtuacaoId == 0)
                    ModelState.AddModelError("AreaAtuacao", "Obrigatório");

                if (Model.TituloId == null && Model.TituloId == 0)
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Email))
                    ModelState.AddModelError("Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EmailLattes))
                    ModelState.AddModelError("EmailLattes", "Obrigatório");

                if (Model.DataNascimento == null && Model.DataNascimento == DateTime.MinValue)
                    ModelState.AddModelError("DataNascimento", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.DataPosse == null && Model.DataPosse == DateTime.MinValue)
                    ModelState.AddModelError("DataPosse", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _docRep.Add(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_docRep.GetById(Id));
            }
            catch (Exception erro)
            {
                ViewData["Error"] = erro.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Docente Model)
        {
            try
            {
                #region + Validacao

                if (Model.AreaAtuacaoId == null && Model.AreaAtuacaoId == 0)
                    ModelState.AddModelError("AreaAtuacao", "Obrigatório");

                if (Model.TituloId == null && Model.TituloId == 0)
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Email))
                    ModelState.AddModelError("Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EmailLattes))
                    ModelState.AddModelError("EmailLattes", "Obrigatório");

                if (Model.DataNascimento == null && Model.DataNascimento == DateTime.MinValue)
                    ModelState.AddModelError("DataNascimento", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.DataPosse == null && Model.DataPosse == DateTime.MinValue)
                    ModelState.AddModelError("DataPosse", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _docRep.Attach(Model);

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_docRep.GetById(Id));
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
                var Model = _docRep.GetById(Id);

                if (Model != null)
                {
                    _docRep.Remove(Model);

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
            catch (Exception error)
            {
                return Json(new { Result = "Erro", Message = error.Message });
            }
        }
    }
}