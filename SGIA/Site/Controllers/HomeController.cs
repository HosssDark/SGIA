using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.ViewModels;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult DashCards()
        {
            using (Contexto bd = new Contexto())
            {
                DashCardsViewModel Model = new DashCardsViewModel
                {
                    ProjetoTotal = bd.Projetos.Where(a => a.StatusId == 1).ToList().Count,
                    TurmasTotal = bd.Turmas.ToList().Count,
                    DocentesTotal = bd.Projetos.Where(a => a.StatusId == 1).ToList().Count,
                    DicentesTotal = bd.Projetos.Where(a => a.StatusId == 1).ToList().Count
                };

                return View(Model);
            }
        }

        public IActionResult DashProjetos()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from pj in bd.Projetos
                             join dc in bd.Docentes on pj.DocenteId equals dc.DocenteId
                             join sta in bd.Status on pj.StatusId equals sta.StatusId
                             select new DashProjetosViewModel
                             {
                                 ProjetoId = pj.ProjetoId,
                                 StatusId = pj.StatusId,
                                 Descricao = pj.Nome,
                                 DocenteId = pj.DocenteId,
                                 Docente = dc.Nome,
                                 CargaHoraria = pj.CargaHoraria,
                                 DataInicio = pj.DataInicio,
                                 DataTermino = pj.DataTermino,
                                 Progresso = 10,
                                 Status = sta.Descricao
                             }).ToList();

                return View(Model);
            }
        }

        public IActionResult DashTurmas()
        {
            using (Contexto bd = new Contexto())
            {
                var Model = (from tm in bd.Turmas
                             select new DashTurmaViewModel
                             {
                                 TurmaId = tm.TurmaId,
                                 Descricao = tm.Nome
                             }).ToList();

                return View(Model);
            }
        }

        public IActionResult DashDicentes()
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

        public IActionResult DashDocentes()
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

        public IActionResult MenuLeft(int TipoAcesso)
        {
            using (Contexto bd = new Contexto())
            {
                if (TipoAcesso == 2)
                    return View(bd.Menus.Where(a => a.Ativo == true && a.TipoAcesso == TipoAcesso).ToList());
                else
                    return View(bd.Menus.Where(a => a.Ativo == true).ToList());
            }
        }

        public IActionResult VersionHistory()
        {
            List<Historico> ListaVersao = new List<Historico>();

            var Versao = new Historico("1.0.0", "13/10/2019");
            Versao.Descricao.Add("1 - Foi criado projeto.");
            Versao.Descricao.Add("2 - Foi criado Cruds.");

            ListaVersao.Add(Versao);

            return View(ListaVersao);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}