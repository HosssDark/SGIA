using Microsoft.AspNetCore.Mvc;
using Repository;
using Functions;
using System;
using System.Net.Mail;
using System.Net;
using Domain;
using Site.Controllers.ViewModels;
using System.Linq;

namespace Site.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _RepositoryUser;
        private IUserPasswordRepository _RepositoryUserPass;
        private LoginUser _LoginUser;

        public LoginController(IUserRepository IUserRepository, IUserPasswordRepository userPasswordRepository, LoginUser loginUser)
        {
            _RepositoryUser = IUserRepository;
            _RepositoryUserPass = userPasswordRepository;
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel Model)
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

                #endregion

                if (ModelState.IsValid)
                {
                    var model = _RepositoryUser.VerificationEmail(Model.Email);

                    if (model != null)
                    {
                        IUserPasswordRepository passRep = new UserPasswordRepository();

                        var Password = passRep.GetById(model.UserId);

                        if (Password.Guid == null)//Cadastro Confirmado
                        {
                            var password = _RepositoryUserPass.VerificationPassword(Model.Password);

                            if (password != null)
                            {
                                _LoginUser.SetUser(model);

                                return RedirectToAction("DashBoard", "Home", new { area = "Admin" });
                            }
                            else
                            {
                                ModelState.AddModelError("Password", "Senha Inválido");
                                return View("Index", Model);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email Inválido!");
                        return View("Index", Model);
                    }
                }

                return View("Index", Model);
            }
            catch (Exception erro)
            {
                ViewData["Error"] = "Erro Inesperado!";
                return View("Index");
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel Model)
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

                if (Model.Password == Model.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Senhas não conferem");

                #endregion

                if (ModelState.IsValid)
                {
                    IUserPasswordRepository passRep = new UserPasswordRepository();

                    #region + Set User

                    User User = new User()
                    {
                        Email = Model.Email
                    };

                    _RepositoryUser.Add(User);

                    #endregion

                    #region + Set Password

                    UserPassword Password = new UserPassword()
                    {
                        Password = MD5.MD5Hash(Model.Password),
                        Guid = Guid.NewGuid().ToString(),
                        UserId = User.UserId
                    };
                    
                    passRep.Add(Password);

                    #endregion

                    //Email email = new Email
                    //{
                    //    ToEmail = Model.Email,
                    //    Subject = "Sistema de Gerenciamento de Informação Acadêmica",
                    //    Body = ""
                    //};

                    //string retorno = this.SubmitEmai(email);

                    //ViewData["Msg"] = retorno;
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception)
            {
                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }

        public IActionResult ToRecover()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToRecover(ToRecoverViewModel Model)
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

                #endregion

                if (ModelState.IsValid)
                {
                    var model = _RepositoryUser.VerificationEmail(Model.Email);

                    if (model != null)
                    {
                        Email email = new Email
                        {
                            ToEmail = Model.Email,
                            Subject = "Sistema de Gerenciamento de Informação Acadêmica",
                            Body = ""
                        };

                        string retorno = this.SubmitEmai(email);

                        ViewData["Msg"] = retorno;
                        return View();
                    }
                    else
                    {
                        ViewData["Error"] = "Email Inválido!";
                        return View();
                    }
                }

                return View(Model);
            }
            catch (Exception erro)
            {
                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }

        public IActionResult RegisterConfirm(string hash)
        {
            try
            {
                #region + Validacao

                if (string.IsNullOrEmpty(hash))
                    ViewData["Error"] = "Hash não foi encontrada!";

                #endregion

                if (ModelState.IsValid)
                {
                    IUserPasswordRepository passRep = new UserPasswordRepository();

                    var Password = passRep.Get(a => a.Guid == hash).FirstOrDefault();
                    Password.Guid = "";

                    passRep.Attach(Password);
                    ViewData["Success"] = "Cadastro confirmado";

                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception)
            {
                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }

        public string SubmitEmai(Email Email)
        {
            try
            {
                EmailSettings EmailSetting = new EmailSettings();

                MailMessage message = new MailMessage();
                message.From = new MailAddress(EmailSetting.UsernameEmail);
                message.To.Add(Email.ToEmail);
                message.Subject = Email.Subject;
                message.Body = Email.Body;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(EmailSetting.PrimaryDomain, EmailSetting.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(EmailSetting.UsernameEmail, EmailSetting.UsernamePassword);
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(message);
                }

                return "Email enviado com sucesso";
            }
            catch (Exception)
            {
                return "Erro Inesperado!";
            }
        }
    }
}