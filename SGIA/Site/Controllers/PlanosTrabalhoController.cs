using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class PlanosTrabalhoController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from pl in bd.PlanosTrabalho
                             join dc in bd.Docentes on pl.DocenteId equals dc.DocenteId
                             join sta in bd.Status on pl.StatusId equals sta.StatusId
                             select new PlanoTrabalhoViewModel
                             {
                                 PlanoTrabalhoId = pl.PlanoTrabalhoId,
                                 DocenteId = pl.DocenteId,
                                 StatusId = pl.StatusId,
                                 DataCadastro = pl.DataCadastro,
                                 DescricaoAtividade = pl.DescricaoAtividade,
                                 DiaSemana = pl.DiaSemana,
                                 Docente = dc.Nome,
                                 HoraEncerramento = pl.HoraEncerramento,
                                 HoraInicio = pl.HoraInicio,
                                 Status = sta.Descricao
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
        public IActionResult Adicionar(PlanoTrabalho Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.PlanosTrabalho.Add(Model);
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
        public IActionResult Alterar(PlanoTrabalho Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (Model.DocenteId == null && Model.DocenteId == 0)
                    ModelState.AddModelError("Docente", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraInicio))
                    ModelState.AddModelError("HoraInicio", "Obrigatório");

                if (string.IsNullOrEmpty(Model.HoraEncerramento))
                    ModelState.AddModelError("HoraEncerramento", "Obrigatório");

                if (string.IsNullOrEmpty(Model.DescricaoAtividade))
                    ModelState.AddModelError("DescricaoAtividade", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.PlanosTrabalho.Attach(Model);
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
                var Model = bd.PlanosTrabalho.Where(a => a.PlanoTrabalhoId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.PlanosTrabalho.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}