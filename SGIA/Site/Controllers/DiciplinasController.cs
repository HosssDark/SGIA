using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class DiciplinasController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from dp in bd.Diciplinas
                             join sta in bd.Status on dp.StatusId equals sta.StatusId
                             select new DiciplinaViewModel
                             {
                                 DiciplinaId = dp.DiciplinaId,
                                 TurmaId = dp.TurmaId,
                                 StatusId = dp.StatusId,
                                 CargaHoraria = dp.CargaHoraria,
                                 CreditoAtividadeCampo = dp.CreditoAtividadeCampo,
                                 CreditoAtividadePratica = dp.CreditoAtividadePratica,
                                 CreditoEnsino = dp.CreditoEnsino,
                                 Ementa = dp.Ementa,
                                 HoraSemanal = dp.HoraSemanal,
                                 Nome = dp.Nome,
                                 DataCadastro = dp.DataCadastro,
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
        public IActionResult Adicionar(Diciplina Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Ementa))
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.HoraSemanal == null && Model.HoraSemanal >= 0)
                    ModelState.AddModelError("HoraSemanal", "Obrigatório");

                if (Model.CreditoEnsino == null && Model.CreditoEnsino >= 0)
                    ModelState.AddModelError("CreditoEnsino", "Obrigatório");

                if (Model.CreditoAtividadePratica == null && Model.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("CreditoAtividadePratica", "Obrigatório");

                if (Model.CreditoAtividadeCampo == null && Model.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.Diciplinas.Add(Model);
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
                return View(bd.Diciplinas.Where(a => a.DiciplinaId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Diciplina Model)
        {
            using (Contexto bd = new Contexto())
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Ementa))
                    ModelState.AddModelError("Ementa", "Obrigatório");

                if (Model.CargaHoraria == null && Model.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.HoraSemanal == null && Model.HoraSemanal >= 0)
                    ModelState.AddModelError("HoraSemanal", "Obrigatório");

                if (Model.CreditoEnsino == null && Model.CreditoEnsino >= 0)
                    ModelState.AddModelError("CreditoEnsino", "Obrigatório");

                if (Model.CreditoAtividadePratica == null && Model.CreditoAtividadePratica >= 0)
                    ModelState.AddModelError("CreditoAtividadePratica", "Obrigatório");

                if (Model.CreditoAtividadeCampo == null && Model.CreditoAtividadeCampo >= 0)
                    ModelState.AddModelError("CreditoAtividadeCampo", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    bd.Diciplinas.Attach(Model);
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
                return View(bd.Diciplinas.Where(a => a.DiciplinaId == Id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            using (Contexto bd = new Contexto())
            {
                var Model = bd.Diciplinas.Where(a => a.DiciplinaId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Diciplinas.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}