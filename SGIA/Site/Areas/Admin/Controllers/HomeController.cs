using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Logged]
    public class HomeController : Controller
    {
        private LoginUser _LoginUser;

        private IUserRepository _userRep = new UserRepository();

        public HomeController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }

        public IActionResult UserImage()
        {
            try
            {
                IUserImageRepository imgRep = new UserImageRepository();

                ViewBag.HaveImg = imgRep.Get(a => a.UserId == _LoginUser.GetUser().UserId).Any(a => a.Dados != null);

                return View(_LoginUser.GetUser());
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult DashBoard()
        {
            ILogRepository _LogRep = new LogRepository();

            _LogRep.Add(new Log
            {
                Description = string.Format("{0}", _LoginUser.GetUser()),
                Origin = "DashBord",
                UserChangeId = 1
            });

            return View();
        }

        public IActionResult DashCards()
        {
            IProjetoRepository _proRep = new ProjetoRepository();
            ITurmaRepository _turRep = new TurmaRepository();
            IDicenteRepository _dicRep = new DicenteRepository();

            DashCardsViewModel Model = new DashCardsViewModel
            {
                ProjetoTotal = _proRep.Get(a => a.StatusId == 1).Count(),
                TurmasTotal = _turRep.Get().Count(),
                DocentesTotal = _userRep.Get(a => a.StatusId == 1).Count(),
                DicentesTotal = _dicRep.Get(a => a.StatusId == 1).Count()
            };

            return View(Model);
        }

        public IActionResult DashProjetos()
        {
            try
            {
                IProjetoRepository _proRep = new ProjetoRepository();
                IStatusRepository _staRep = new StatusRepository();

                var Model = (from pj in _proRep.GetAll()
                             join use in _userRep.GetAll() on pj.UserId equals use.UserId
                             join sta in _staRep.GetAll() on pj.StatusId equals sta.StatusId
                             select new DashProjetosViewModel
                             {
                                 ProjetoId = pj.ProjetoId,
                                 StatusId = pj.StatusId,
                                 Descricao = pj.Nome,
                                 DocenteId = pj.UserId,
                                 Docente = use.Nome,
                                 CargaHoraria = pj.CargaHoraria,
                                 DataInicio = pj.DataInicio,
                                 DataTermino = pj.DataTermino,
                                 Progresso = 10,
                                 Status = sta.Descricao,
                                 Classe = sta.Classe,
                                 Cor = sta.Cor
                             }).ToList();

                return View(Model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public IActionResult DashTurmas()
        //{
        //    try
        //    {
        //        var Model = (from tm in bd.Turmas
        //                     select new DashTurmaViewModel
        //                     {
        //                         TurmaId = tm.TurmaId,
        //                         Descricao = tm.Nome
        //                     }).ToList();

        //        return View(Model);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public IActionResult DashDicentes()
        //{
        //    try
        //    {
        //        var Model = (from dc in bd.Dicentes
        //                     join sta in bd.Status on dc.StatusId equals sta.StatusId
        //                     select new DicenteViewModel
        //                     {
        //                         DicenteId = dc.DicenteId,
        //                         Matricula = dc.Matricula,
        //                         Nome = dc.Nome,
        //                         Email = dc.Email,
        //                         Telefone = dc.Telefone,
        //                         Celular = dc.Celular,
        //                         DataCadastro = dc.DataCadastro,
        //                         StatusId = dc.StatusId,
        //                         Status = sta.Descricao,
        //                     }).ToList();

        //        return View(Model);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public IActionResult DashDocentes()
        //{
        //    try
        //    {
        //        var Model = (from dc in bd.Docentes
        //                     join sta in bd.Status on dc.StatusId equals sta.StatusId
        //                     select new DocenteViewModel
        //                     {
        //                         DocenteId = dc.DocenteId,
        //                         AreaAtuacaoId = dc.AreaAtuacaoId,
        //                         TipoId = dc.TipoId,
        //                         TituloId = dc.TituloId,
        //                         CargaHoraria = dc.CargaHoraria,
        //                         Celular = dc.Celular,
        //                         DataNascimento = dc.DataNascimento,
        //                         DataPosse = dc.DataPosse,
        //                         Email = dc.Email,
        //                         EmailLattes = dc.EmailLattes,
        //                         Nome = dc.Nome,
        //                         Telefone = dc.Telefone,
        //                         DataCadastro = dc.DataCadastro,
        //                         StatusId = dc.StatusId,
        //                         Status = sta.Descricao,
        //                     }).ToList();

        //        return View(Model);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public IActionResult LeftBar()
        {
            try
            {
                IMenuRepository _menRep = new MenuRepository();

                return View(_menRep.GetAll().ToList());
            }
            catch (Exception)
            {

                throw;
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