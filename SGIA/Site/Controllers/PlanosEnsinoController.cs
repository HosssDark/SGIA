using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Controllers
{
    public class PlanosEnsinoController : Controller
    {
        private IPlanoEnsinoRepository _ensRep = new PlanoEnsinoRepository();
        private IDiciplinaRepository _dicRep = new DiciplinaRepository();
        private IDocenteRepository _docRep = new DocenteRepository();
        private ITurmaRepository _turRep = new TurmaRepository();
        private IStatusRepository _staRep = new StatusRepository();

        public IActionResult Index()
        {
            try
            {
                var Model = (from pl in _ensRep.GetAll()
                             join dp in _dicRep.GetAll() on pl.DiciplinaId equals dp.DiciplinaId
                             join dc in _docRep.GetAll() on pl.DocenteId equals dc.DocenteId
                             join tm in _turRep.GetAll() on pl.TurmaId equals tm.TurmaId
                             join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                             select new PlanoEnsinoViewModel
                             {
                                 PlanoEnsinoId = pl.PlanoEnsinoId,
                                 BiografiaBasicaId = pl.BiografiaBasicaId,
                                 TurmaId = pl.TurmaId,
                                 DocenteId = pl.DocenteId,
                                 DiciplinaId = pl.DiciplinaId,
                                 StatusId = pl.StatusId,
                                 BiografiaComplementarId = pl.BiografiaComplementarId,
                                 EmentaId = pl.EmentaId,
                                 AtividadeTrabalhada = pl.AtividadeTrabalhada,
                                 DataCadastro = pl.DataCadastro,
                                 DataEmissao = pl.DataEmissao,
                                 Diciplina = dp.Nome,
                                 Docente = dc.Nome,
                                 EspecificacaoConteudo = pl.EspecificacaoConteudo,
                                 MetodologiaAvaliacao = pl.MetodologiaAvaliacao,
                                 ObjetivoArea = pl.ObjetivoArea,
                                 ObjetivoGeral = pl.ObjetivoGeral,
                                 RecursoUtilizado = pl.RecursoUtilizado,
                                 Status = sta.Descricao,
                                 TecnicaPedagogica = pl.TecnicaPedagogica,
                                 Turma = tm.Nome
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
        public IActionResult Adicionar(PlanoEnsino Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (Model.EmentaId == null && Model.EmentaId == 0)
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoArea))
                    ModelState.AddModelError("ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoGeral))
                    ModelState.AddModelError("ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EspecificacaoConteudo))
                    ModelState.AddModelError("EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.MetodologiaAvaliacao))
                    ModelState.AddModelError("MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.TecnicaPedagogica))
                    ModelState.AddModelError("TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.RecursoUtilizado))
                    ModelState.AddModelError("RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AtividadeTrabalhada))
                    ModelState.AddModelError("AtividadeTrabalhada", "Obrigatório");

                if (Model.DataEmissao == null && Model.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Add(Model);

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
                return View(_ensRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoEnsino Model)
        {
            try
            {
                #region + Validacao

                if (Model.DiciplinaId == null && Model.DiciplinaId == 0)
                    ModelState.AddModelError("Diciplina", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (Model.EmentaId == null && Model.EmentaId == 0)
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoArea))
                    ModelState.AddModelError("ObjetivoArea", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ObjetivoGeral))
                    ModelState.AddModelError("ObjetivoGeral", "Obrigatório");

                if (string.IsNullOrEmpty(Model.EspecificacaoConteudo))
                    ModelState.AddModelError("EspecificacaoConteudo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.MetodologiaAvaliacao))
                    ModelState.AddModelError("MetodologiaAvaliacao", "Obrigatório");

                if (string.IsNullOrEmpty(Model.TecnicaPedagogica))
                    ModelState.AddModelError("TecnicaPedagogica", "Obrigatório");

                if (string.IsNullOrEmpty(Model.RecursoUtilizado))
                    ModelState.AddModelError("RecursoUtilizado", "Obrigatório");

                if (string.IsNullOrEmpty(Model.AtividadeTrabalhada))
                    ModelState.AddModelError("AtividadeTrabalhada", "Obrigatório");

                if (Model.DataEmissao == null && Model.DataEmissao == DateTime.MinValue)
                    ModelState.AddModelError("DataEmissao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _ensRep.Attach(Model);

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
                return View(_ensRep.GetById(Id));
            }
            catch (Exception)
            {
                ViewData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _ensRep.GetById(Id);

                if (Model != null)
                {
                    _ensRep.Remove(Model);

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