using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;

namespace Site.Controllers
{
    public class ProjetosController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from pj in bd.Projetos
                             join dc in bd.Docentes on pj.DocenteId equals dc.DocenteId
                             join sta in bd.Status on pj.StatusId equals sta.StatusId
                             select new ProjetoViewModel
                             {
                                 ProjetoId = pj.ProjetoId,
                                 DocenteId = pj.DocenteId,
                                 StatusId = pj.StatusId,
                                 Docente = dc.Nome,
                                 Nome = pj.Nome,
                                 Status = sta.Descricao,
                                 CargaHoraria = pj.CargaHoraria,
                                 DataCadastro = pj.DataCadastro,
                                 DataInicio = pj.DataInicio,
                                 DataTermino = pj.DataTermino
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
        public IActionResult Adicionar(Projeto Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.DataInicio == null && Model.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.DataTermino == null && Model.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.Projetos.Add(Model);
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
        public IActionResult Alterar(Projeto Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("DocenteId", "Obrigatório");

                if (Model.DataInicio == null && Model.DataInicio == DateTime.MinValue)
                    ModelState.AddModelError("DataInicio", "Obrigatório");

                if (Model.DataTermino == null && Model.DataTermino == DateTime.MinValue)
                    ModelState.AddModelError("DataTermino", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.Projetos.Attach(Model);
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
                var Model = bd.Projetos.Where(a => a.ProjetoId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Projetos.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}