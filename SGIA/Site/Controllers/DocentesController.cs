using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;
using System;
using System.Linq;

namespace Site.Controllers
{
    public class DocentesController : Controller
    {
        public IActionResult Index()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from dc in bd.Docentes
                             join sta in bd.Status on dc.StatusId equals sta.StatusId
                             select new DocenteViewModel
                             {
                                 DocenteId = dc.DocenteId,
                                 AreaAtuacaoId = dc.AreaAtuacaoId,
                                 TipoId = dc.TipoId,
                                 TituloId = dc.TituloId,
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
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Docente Model)
        {
            using (Contexto bd = new Contexto())
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
                    Model.DataCadastro = DateTime.Now;
                    Model.StatusId = 1;

                    bd.Docentes.Add(Model);
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
        public IActionResult Alterar(Docente Model)
        {
            using (Contexto bd = new Contexto())
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
                    bd.Docentes.Attach(Model);
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
                var Model = bd.Docentes.Where(a => a.DocenteId == Id).FirstOrDefault();

                if (Model != null)
                {
                    bd.Docentes.Remove(Model);
                    bd.SaveChanges();

                    return Json(new { Result = "OK", Message = "Registro excluido com sucesso!" });
                }

                return Json(new { Result = "Erro", Message = "Registro não encontrado!" });
            }
        }
    }
}