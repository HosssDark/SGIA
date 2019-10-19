using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Controllers
{
    public class AtribuicoesController : Controller
    {
        private IAtribuicaoRepository _atrRep = new AtribuicaoRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private IProjetoRepository _proRep = new ProjetoRepository();
        private IDocenteRepository _docRep = new DocenteRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from at in _atrRep.GetAll()
                             join sta in _staRep.GetAll() on at.StatusId equals sta.StatusId
                             join dp in _dicRep.GetAll() on at.DiciplinaId equals dp.DiciplinaId
                             join pj in _proRep.GetAll() on at.ProjetoId equals pj.ProjetoId
                             join dc in _docRep.GetAll() on at.DocenteId equals dc.DocenteId
                             select new AtribuicaoViewModel
                             {
                                 AtribuicaoId = at.AtribuicaoId,
                                 DiciplinaId = at.DiciplinaId,
                                 ProjetoId = at.ProjetoId,
                                 DocenteId = at.DocenteId,
                                 DataCadastro = at.DataCadastro,
                                 Diciplina = dp.Nome,
                                 Projeto = pj.Nome,
                                 StatusId = at.StatusId,
                                 Status = sta.Descricao,
                                 Docente = dc.Nome
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
        public IActionResult Adicionar(Atribuicao Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.ProjetoId == null && Model.ProjetoId == 0)
                    ModelState.AddModelError("Projeto", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    _atrRep.Add(Model);

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
                return View(_atrRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Atribuicao Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.ProjetoId == null && Model.ProjetoId == 0)
                    ModelState.AddModelError("Projeto", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _atrRep.Attach(Model);
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
                var Model = _atrRep.GetById(Id);

                if (Model != null)
                {
                    _atrRep.Remove(Model);

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