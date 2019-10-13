using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;

namespace Site.Controllers
{
    public class HorarioAulasController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from hr in bd.HorarioAulas
                             join dp1 in bd.Diciplinas on hr.DiciplinaPrimeiroId equals dp1.DiciplinaId into r1
                             from dp1 in r1.DefaultIfEmpty()
                             join dp2 in bd.Diciplinas on hr.DiciplinaSegundoId equals dp2.DiciplinaId into r2
                             from dp2 in r2.DefaultIfEmpty()
                             join dp3 in bd.Diciplinas on hr.DiciplinaTerceiroId equals dp3.DiciplinaId into r3
                             from dp3 in r3.DefaultIfEmpty()
                             join dp4 in bd.Diciplinas on hr.DiciplinaQuartoId equals dp4.DiciplinaId into r4
                             from dp4 in r4.DefaultIfEmpty()
                             join tm in bd.Turmas on hr.HorarioAulaId equals tm.TurmaId
                             join sta in bd.Status on hr.StatusId equals sta.StatusId
                             select new HorarioAulaViewModel
                             {
                                 HorarioAulaId = hr.HorarioAulaId,
                                 DiciplinaPrimeiroId = hr.DiciplinaPrimeiroId,
                                 DiciplinaPrimeiro = dp1.Nome,
                                 DiciplinaSegundoId = hr.DiciplinaSegundoId,
                                 DiciplinaSegundo = dp2.Nome,
                                 DiciplinaTerceiroId = hr.DiciplinaTerceiroId,
                                 DiciplinaTerceiro = dp3.Nome,
                                 DiciplinaQuartoId = hr.DiciplinaQuartoId,
                                 DiciplinaQuarto = dp4.Nome,
                                 DataCadastro = hr.DataCadastro,
                                 DiaSemana = hr.DiaSemana,
                                 Periodo = hr.Periodo,
                                 StatusId = hr.StatusId,
                                 Status = sta.Descricao,
                                 TurmaId = hr.TurmaId,
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
        public IActionResult Adicionar(HorarioAula Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (Model.DiciplinaPrimeiroId == null && Model.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("DiciplinaPrimeiro", "Obrigatório");

                if (Model.DiciplinaSegundoId == null && Model.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("DiciplinaSegundo", "Obrigatório");

                if (Model.DiciplinaTerceiroId == null && Model.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("DiciplinaTerceiro", "Obrigatório");

                if (Model.DiciplinaQuartoId == null && Model.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("DiciplinaQuarto", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Periodo))
                    ModelState.AddModelError("Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.HorarioAulas.Add(Model);
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
        public IActionResult Alterar(HorarioAula Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.DiaSemana))
                    ModelState.AddModelError("DiaSemana", "Obrigatório");

                if (Model.DiciplinaPrimeiroId == null && Model.DiciplinaPrimeiroId == 0)
                    ModelState.AddModelError("DiciplinaPrimeiro", "Obrigatório");

                if (Model.DiciplinaSegundoId == null && Model.DiciplinaSegundoId == 0)
                    ModelState.AddModelError("DiciplinaSegundo", "Obrigatório");

                if (Model.DiciplinaTerceiroId == null && Model.DiciplinaTerceiroId == 0)
                    ModelState.AddModelError("DiciplinaTerceiro", "Obrigatório");

                if (Model.DiciplinaQuartoId == null && Model.DiciplinaQuartoId == 0)
                    ModelState.AddModelError("DiciplinaQuarto", "Obrigatório");

                if (Model.TurmaId == null && Model.TurmaId == 0)
                    ModelState.AddModelError("Turma", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Periodo))
                    ModelState.AddModelError("Periodo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.HorarioAulas.Attach(Model);
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
                var Model = bd.HorarioAulas.Where(a => a.HorarioAulaId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.HorarioAulas.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}