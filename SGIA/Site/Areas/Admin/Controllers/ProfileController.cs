using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Functions;
using Microsoft.AspNetCore.Http;
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
        private IUserImageRepository _imgRep = new UserImageRepository();
        private ITipoAcessoRepository _aceRep = new TipoAcessoRepository();
        private ILogRepository _LogRep = new LogRepository();

        private LoginUser _LoginUser;

        public ProfileController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            try
            {
                var User = _userRep.Get(a => a.UserId == _LoginUser.GetUser().UserId);
                var Image = _imgRep.Get(a => a.UserId == _LoginUser.GetUser().UserId);

                var Model = (from use in User
                             join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId into r1
                             from at in r1.DefaultIfEmpty()
                             join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId into r2
                             from tl in r2.DefaultIfEmpty()
                             join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId into r3
                             from tp in r3.DefaultIfEmpty()
                             join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                             join ace in _aceRep.GetAll() on use.TipoAcessoId equals ace.TipoAcessoId
                             join img in Image on use.UserId equals img.UserId into r4
                             from img in r4.DefaultIfEmpty()
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
                                 Image = img != null ? img.Dados : null
                             }).FirstOrDefault();

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao Obter Registro";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(UserViewModel Model, IFormFile Arquivo)
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
                    if (Arquivo != null)
                    {
                        IUserImageRepository imgRep = new UserImageRepository();

                        IFormFile Imagem = Arquivo;

                        if (Imagem != null || Imagem.ContentType.ToLower().StartsWith("image/"))
                        {
                            MemoryStream ms = new MemoryStream();
                            Imagem.OpenReadStream().CopyTo(ms);

                            UserImage Image = new UserImage()
                            {
                                UserId = 1,
                                Name = Imagem.Name,
                                Dados = ms.ToArray(),
                                ContentType = Imagem.ContentType,
                                TipoAcesso = ""
                            };

                            imgRep.Add(Image);
                        }
                    }

                    _userRep.Attach(Model.User);

                    TempData["Success"] = "Registro alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachPassword(UserViewModel Model)
        {
            try
            {
                #region + Validacao

                //if (string.IsNullOrEmpty(Model.Password.Email))
                //    ModelState.AddModelError("", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Password.Password))
                    ModelState.AddModelError("Password_Password", "Obrigatório");

                if (string.IsNullOrEmpty(Model.PasswordConfirm))
                    ModelState.AddModelError("PasswordConfirm", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    IUserPasswordRepository passRep = new UserPasswordRepository();

                    passRep.Attach(Model.Password);

                    TempData["Success"] = "Registro gravado com sucesso";
                    return View();
                }
                                
                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                #endregion

                TempData["Error"] = "Erro ao tentar Alterar o Registro!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public FileStreamResult ToSeeImagem(int Id)
        {
            UserImage imagem = _imgRep.GetById(Id);

            MemoryStream Ms = new MemoryStream(imagem.Dados);

            return new FileStreamResult(Ms, imagem.ContentType);
        }
    }
}