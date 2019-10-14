using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;

namespace Site.Controllers
{
    public class AtribuicoesController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from at in bd.Atribuicoes
                             join sta in bd.Status on at.StatusId equals sta.StatusId
                             join dp in bd.Diciplinas on at.DiciplinaId equals dp.DiciplinaId
                             join pj in bd.Projetos on at.ProjetoId equals pj.ProjetoId
                             join dc in bd.Docentes on at.DocenteId equals dc.DocenteId
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
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Atribuicao Model)
        {
            using (Contexto bd = new Contexto())
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

                    bd.Atribuicoes.Add(Model);
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
                return View(bd.Atribuicoes.Where(a => a.AtribuicaoId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Atribuicao Model)
        {
            using (Contexto bd = new Contexto())
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
                    bd.Atribuicoes.Attach(Model);
                    bd.Entry(Model).State = EntityState.Modified;
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                var Model = bd.Atribuicoes.Where(a => a.AtribuicaoId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Atribuicoes.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}