using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;

namespace Site.Controllers
{
    public class PlanosEnsinoController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from pl in bd.PlanosEnsino
                             join dp in bd.Diciplinas on pl.DiciplinaId equals dp.DiciplinaId
                             join dc in bd.Docentes on pl.DocenteId equals dc.DocenteId
                             join tm in bd.Turmas on pl.TurmaId equals tm.TurmaId
                             join sta in bd.Status on pl.StatusId equals sta.StatusId
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
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(PlanoEnsino Model)
        {
            using (Contexto bd = new Contexto())
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
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.PlanosEnsino.Add(Model);
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                return View(bd.Livros.Where(a => a.LivroId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PlanoEnsino Model)
        {
            using (Contexto bd = new Contexto())
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
                    bd.PlanosEnsino.Attach(Model);
                    bd.Entry(Model).State = EntityState.Modified;
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
        }

        public IActionResult Detalhes(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                return View(bd.Livros.Where(a => a.LivroId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                var Model = bd.PlanosEnsino.Where(a => a.PlanoEnsinoId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.PlanosEnsino.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}