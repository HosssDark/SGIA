using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository;
using static Site.Notification;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class HomeController : Controller
    {
        private LoginUser _LoginUser;

        private IUserRepository _userRep = new UserRepository();
        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }

        public IActionResult UserImage()
        {
            try
            {
                IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                ViewBag.Image = imgRep.GetImageUser(_LoginUser.GetUser().UserId, "images", "Usuarios", "Usuario", _appEnvironment.WebRootPath);

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
                DocentesTotal = _userRep.Get(a => a.StatusId == 1 && a.TipoAcessoId == 2).Count(),
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

        public IActionResult Notifications()
        {
            List<NotificationList> List = new List<NotificationList>();

            if (TempData.ContainsKey("Success"))
            {
                TempData.TryGetValue("Success", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = "Success",
                        Message = item.ToString()
                    });
                }
            }

            if (TempData.ContainsKey("Error"))
            {
                TempData.TryGetValue("Error", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = "Error",
                        Message = item.ToString()
                    });
                }
            }

            if (TempData.ContainsKey("Info"))
            {
                TempData.TryGetValue("Info", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = "Info",
                        Message = item.ToString()
                    });
                }
            }

            return Json(new { List = List });
        }
    }
}