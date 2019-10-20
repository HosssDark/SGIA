using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private IUserRepository _userRep = new UserRepository();
        private IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
        private ITituloRepository _titRep = new TituloRepository();
        private ITipoDocenteRepository _tipRep = new TipoDocenteRepository();
        private IStatusRepository _staRep = new StatusRepository();
        private IUserImageRepository imgRep = new UserImageRepository();

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
                var Image = imgRep.Get(a => a.UserId == _LoginUser.GetUser().UserId);

                var Model = (from use in User
                             join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId into r1
                             from at in r1.DefaultIfEmpty()
                             join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId into r2
                             from tl in r2.DefaultIfEmpty()
                             join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId into r3
                             from tp in r3.DefaultIfEmpty()
                             join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                             join img in Image on use.UserId equals img.UserId into r4
                             from img in r4.DefaultIfEmpty()
                             where use.UserId == _LoginUser.GetUser().UserId
                             select new UserViewModel
                             {
                                 User = use,
                                 AreaAtuacao = at != null ? at.Descricao : "",
                                 Tipo = tp != null ? tp.Descricao : "",
                                 Titulo = tl != null ? tl.Descricao : "",
                                 Status = sta.Descricao,
                                 Classe = sta.Classe,
                                 Cor = sta.Cor,
                                 Image = img != null ? img.Dados : null
                             }).FirstOrDefault();

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return View();
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_userRep.GetById(Id));
            }
            catch (Exception erro)
            {
                ViewData["Error"] = erro.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(DocenteViewModel Model, IList<IFormFile> Arquivo)
        {
            try
            {
                #region + Validacao

                if (Model.User.AreaAtuacaoId == null && Model.User.AreaAtuacaoId == 0)
                    ModelState.AddModelError("AreaAtuacao", "Obrigatório");

                if (Model.User.TituloId == null && Model.User.TituloId == 0)
                    ModelState.AddModelError("Titulo", "Obrigatório");

                if (string.IsNullOrEmpty(Model.User.Nome))
                    ModelState.AddModelError("Nome", "Obrigatório");

                if (string.IsNullOrEmpty(Model.User.Email))
                    ModelState.AddModelError("Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.User.EmailLattes))
                    ModelState.AddModelError("EmailLattes", "Obrigatório");

                if (Model.User.DataNascimento == null && Model.User.DataNascimento == DateTime.MinValue)
                    ModelState.AddModelError("DataNascimento", "Obrigatório");

                if (Model.User.CargaHoraria == null && Model.User.CargaHoraria >= 0)
                    ModelState.AddModelError("CargaHoraria", "Obrigatório");

                if (Model.User.DataPosse == null && Model.User.DataPosse == DateTime.MinValue)
                    ModelState.AddModelError("DataPosse", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    if (Arquivo.Count > 0)
                        if (Arquivo.Count > 1)
                        {
                            IUserImageRepository imgRep = new UserImageRepository();

                            IFormFile Imagem = Arquivo.FirstOrDefault();

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
                        else
                        {
                            ViewData["Error"] = "Só pode ser adicionado uma imagem!";
                            return View(Model);
                        }

                    _userRep.Attach(Model.User);

                    ViewData["Success"] = "Registro gravado com sucesso";
                    return View();
                }

                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachPassword(DocenteViewModel Model)
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

                    ViewData["Success"] = "Registro gravado com sucesso";
                    return View();
                }

                ViewData["Error"] = "";
                return View(Model);
            }
            catch (Exception error)
            {
                ViewData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public FileStreamResult ToSeeImagem(int Id)
        {
            UserImage imagem = imgRep.GetById(Id);

            MemoryStream Ms = new MemoryStream(imagem.Dados);

            return new FileStreamResult(Ms, imagem.ContentType);
        }
    }
}