using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Functions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class ProfileController : Controller
    {
        private IUserRepository _userRep = new UserRepository();
        private IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
        private ITituloRepository _titRep = new TituloRepository();
        private ITipoDocenteRepository _tipRep = new TipoDocenteRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private ITipoAcessoRepository _aceRep = new TipoAcessoRepository();
        private ILogRepository _LogRep = new LogRepository();
        private IAddressRepository _AddRep = new AddressRepository();

        private readonly IHostingEnvironment _appEnvironment;
        private LoginUser _LoginUser;

        public ProfileController(LoginUser loginUser, IHostingEnvironment appEnvironment)
        {
            _LoginUser = loginUser;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

                var User = _userRep.Get(a => a.UserId == _LoginUser.GetUser().UserId);

                var Model = (from use in User
                             join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId into r1
                             from at in r1.DefaultIfEmpty()
                             join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId into r2
                             from tl in r2.DefaultIfEmpty()
                             join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId into r3
                             from tp in r3.DefaultIfEmpty()
                             join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                             join ace in _aceRep.GetAll() on use.TipoAcessoId equals ace.TipoAcessoId
                             where use.UserId == _LoginUser.GetUser().UserId
                             select new UserViewModel
                             {
                                 User = use,
                                 AreaAtuacao = at != null ? at.Descricao : "",
                                 Tipo = tp != null ? tp.Descricao : "",
                                 TipoAcesso = ace.Descricao,
                                 Titulo = tl != null ? tl.Descricao : "",
                                 Status = sta.Descricao,
                                 Classe = sta.Classe,
                                 Cor = sta.Cor,
                                 Image = paramRep.GetImageUser(_LoginUser.GetUser().UserId, "images", "Usuarios", "Usuario", _appEnvironment.WebRootPath)
                             }).FirstOrDefault();

                if (Model.User != null)
                {
                    Model.ChangePassword.Email = Model.User.Email;
                    Model.ChangePassword.UserId = Model.User.UserId;

                    var Address = _AddRep.GetFirst(a => a.UserId == Model.User.UserId);

                    if (Address != null)
                    {
                        Model.Address = Address;
                    }
                    else
                        Model.Address.UserId = Model.User.UserId;
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Profile",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao Obter Registro";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(UserViewModel Model)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(Model.User.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (!string.IsNullOrEmpty(Model.User.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.User.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("Email", "Obrigatório");

                if (!string.IsNullOrEmpty(Model.User.EmailLattes))
                    if (!FunctionsValidate.ValidateEmail(Model.User.EmailLattes))
                        ModelState.AddModelError("Email", "Email Inválido!");

                #endregion

                if (ModelState.IsValid)
                {
                    _userRep.Attach(Model.User);

                    if (Model.File != null)
                    {
                        IParamDirectoryRepository imgRep = new ParamDirectoryRepository();

                        var Info = new FileInfo(Model.File.FileName);

                        using (var stream = new FileStream(Info.Name, FileMode.Create))
                        {
                            Model.File.CopyToAsync(stream);

                            imgRep.SalvarArquivo(stream, "images", "Usuarios", "Usuario", _LoginUser.GetUser().UserId, Info.Extension, _appEnvironment.WebRootPath);
                        }

                        Model.Image = imgRep.GetImageUser(_LoginUser.GetUser().UserId, "images", "Usuarios", "Usuario", _appEnvironment.WebRootPath);
                    }

                    TempData["Success"] = "Registro alterado com sucesso";
                }

                return RedirectToAction("Index");
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Profile",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return View("Index", Model);
            }
        }

        public IActionResult AttachPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachPassword(ChangePasswordViewModel Model)
        {
            try
            {
                #region + Validacao

                if (!string.IsNullOrEmpty(Model.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Password))
                    ModelState.AddModelError("Password", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Obrigatório");

                if (Model.Password != Model.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Senhas não conferem");

                #endregion

                if (ModelState.IsValid)
                {
                    IUserPasswordRepository passRep = new UserPasswordRepository();

                    passRep.ChangePassword(Model.UserId, Model.Password);

                    TempData["Success"] = "Registro gravado com sucesso";
                }

                return RedirectToAction("Index");
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Profile",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult AttachAddress()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachAddress(Address Model)
        {
            try
            {
                #region + Validacao

                if (!string.IsNullOrEmpty(Model.Cep))
                {
                    if (!FunctionsValidate.ValidateCep(Model.Cep))
                        ModelState.AddModelError("Cep", "CEP inválido!");
                }

                #endregion

                if (ModelState.IsValid)
                {
                    IAddressRepository add = new AddressRepository();

                    Model.StatusId = 1;

                    add.Attach(Model);

                    TempData["Success"] = "Registro gravado com sucesso";
                }

                return RedirectToAction("Index");
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Profile",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return RedirectToAction("Index");
            }
        }
    }
}