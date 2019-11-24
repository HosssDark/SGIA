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

                            imgRep.SalvarArquivo(stream, "images", "Usuarios", Model.File.FileName, _LoginUser.GetUser().UserId, Info.Extension, _appEnvironment.WebRootPath);
                        }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachPassword(UserViewModel Model)
        {
            try
            {
                #region + Validacao

                if (!string.IsNullOrEmpty(Model.User.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.ChangePassword.Email))
                        ModelState.AddModelError("ChangePassword_Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("ChangePassword_Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ChangePassword.Password))
                    ModelState.AddModelError("ChangePassword_Password", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ChangePassword.ConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Obrigatório");

                if (Model.ChangePassword.Password != Model.ChangePassword.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Senhas não conferem");

                #endregion

                if (ModelState.IsValid)
                {
                    IUserPasswordRepository passRep = new UserPasswordRepository();

                    passRep.Attach(Model.Password);

                    TempData["Success"] = "Registro gravado com sucesso";
                }

                return View("Index", Model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachAddress(UserViewModel Model)
        {
            try
            {
                #region + Validacao

                if (!string.IsNullOrEmpty(Model.Address.Cep))
                {
                    if (!FunctionsValidate.ValidateCep(Model.Address.Cep))
                        ModelState.AddModelError("Address_Cep", "CEP inválido!");
                }

                #endregion

                if (ModelState.IsValid)
                {
                    IAddressRepository add = new AddressRepository();

                    add.Attach(Model.Address);

                    TempData["Success"] = "Registro gravado com sucesso";
                }

                return View("Index", Model);
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
    }
}