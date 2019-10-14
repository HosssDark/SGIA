using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class DicentesController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from dc in bd.Dicentes
                             join sta in bd.Status on dc.StatusId equals sta.StatusId
                             select new DicenteViewModel
                             {
                                 DicenteId = dc.DicenteId,
                                 Matricula = dc.Matricula,
                                 Nome = dc.Nome,
                                 Email = dc.Email,
                                 Telefone = dc.Telefone,
                                 Celular = dc.Celular,
                                 DataCadastro = dc.DataCadastro,
                                 StatusId = dc.StatusId,
                                 Status = sta.Descricao,
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
        public IActionResult Adicionar(Dicente Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (Model.Matricula == null && Model.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.Dicentes.Add(Model);
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
                return View(bd.Dicentes.Where(a => a.DicenteId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Dicente Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (Model.Matricula == null && Model.Matricula == 0)
                    ModelState.AddModelError("Matricula", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.Dicentes.Attach(Model);
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
                var Model = bd.Dicentes.Where(a => a.DicenteId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Dicentes.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}