using Microsoft.AspNetCore.Mvc;
using Repository;
using Functions;
using System;
using Domain;
using System.Linq;
using System.Collections.Generic;
using static Site.Notification;
using Site.libraries.SubmitEmail;

namespace Site.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _RepositoryUser;
        private IUserPasswordRepository _RepositoryUserPass;
        private LoginUser _LoginUser;
        private ILogRepository _LogRep = new LogRepository();
        private List<Log> Logs = new List<Log>();

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

                        //Cadastro Confirmado
                        if (Password.Guid == null)
                        {
                            var password = _RepositoryUserPass.VerificationPassword(Model.Email, Model.Password);

                            if (password != null)
                            {
                                _LoginUser.SetUser(model);

                                return RedirectToAction("DashBoard", "Home", new { area = "Admin" });
                            }
                            else
                            {
                                #region + Log

                                Logs.Add(new Log
                                {
                                    Description = string.Format("Erro: Email {0}, Senha Inválido {1}", Model.Email, Model.Password),
                                    Origin = "Login",
                                    UserChangeId = 1
                                });

                                #endregion

                                ModelState.AddModelError("Password", "Senha Inválido");
                                return View("Index", Model);
                            }
                        }
                        else
                        {
                            #region + Log

                            Logs.Add(new Log
                            {
                                Description = string.Format("Erro: Cadastro não foi confirmado Email {0}, Senha {1}, Guid {2}", Model.Email, Model.Password, Password.Guid),
                                Origin = "Login",
                                UserChangeId = 1
                            });

                            #endregion

                            ViewData["Error"] = "Cadastro não foi confirmado";
                            return View("Index", Model);
                        }
                    }
                    else
                    {
                        #region + Log

                        Logs.Add(new Log
                        {
                            Description = string.Format("Erro: Email Inválido {0}, Senha {1}", Model.Email, Model.Password),
                            Origin = "Login",
                            UserChangeId = 1
                        });

                        #endregion

                        ModelState.AddModelError("Email", "Email Inválido!");
                        return View("Index", Model);
                    }
                }

                #region + Log

                Logs.Add(new Log
                {
                    Description = string.Format("Erro: Email Inválido {0}, Senha {1}", Model.Email, Model.Password),
                    Origin = "Login",
                    UserChangeId = 1
                });

                _LogRep.AddAll(Logs);

                #endregion

                return View("Index", Model);
            }
            catch (Exception Error)
            {
                #region + Log

                Logs.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                _LogRep.AddAll(Logs);

                #endregion

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
                    IUserRepository User = new UserRepository();

                    bool Exist = User.Get(a => a.Email == Model.Email).Any();

                    if (Exist)
                        ModelState.AddModelError("Email", "Email já foi cadastrado!");

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

                    var Guid = passRep.UserRegister(Model.Email, Model.Password);

                    if (!string.IsNullOrEmpty(Guid))
                    {
                        string host = HttpContext.Request.Host.Host;
                        
                        if (host.ToString() == "localhost" || host.StartsWith("205"))
                            host = HttpContext.Request.Host.Host + ":" + HttpContext.Request.Host.Port;

                        string Link = string.Format("{0}://{1}/Login/RegisterConfirm?hash={2}", HttpContext.Request.Scheme, host, Guid);

                        Email.EmailSettingsOutlook EmailOutlook = new Email.EmailSettingsOutlook();

                        Email.SubmitEmailSettings EmailSettings = new Email.SubmitEmailSettings()
                        {
                            ClientEmail = Model.Email,
                            Name = "",
                            Image = "http://hossshs-001-site1.ftempurl.com/images/logo_unemat.gif",
                            Button = "",
                            Title = "SGIA - Sistema de Gereciamento de Informações Acadêmicas",
                            Email = EmailOutlook,
                            Link = Link,
                            SubjectMatter = "Email de Confirmação",
                            Message = "Clique no link para confirmar seu cadastro."
                        };

                        Email.SubmitEmail(EmailSettings);

                        ViewData["Success"] = "Email enviado com sucesso, verifique sua caixa de email.";
                    }
                    else
                    {
                        ViewData["Error"] = "Erro Inesperado!";
                    }
                    
                    return RedirectToAction("Index");
                }

                return View();
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

                return RedirectToAction("Index");
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
                        IUserPasswordRepository passRep = new UserPasswordRepository();

                        var Guid = passRep.ChangeGuid(Model.Email);

                        string host = HttpContext.Request.Host.Host;

                        if (host.ToString() == "localhost" || host.StartsWith("205"))
                            host = HttpContext.Request.Host.Host + ":" + HttpContext.Request.Host.Port;

                        string Link = string.Format("{0}://{1}/Login/ChangePassword?Guid={2}", HttpContext.Request.Scheme, host, Guid);

                        Email.EmailSettingsOutlook EmailOutlook = new Email.EmailSettingsOutlook();

                        Email.SubmitEmailSettings EmailSettings = new Email.SubmitEmailSettings()
                        {
                            ClientEmail = Model.Email,
                            Name = "",
                            Image = "http://hossshs-001-site1.ftempurl.com/images/logo_unemat.gif",
                            Button = "Alterar Senha",
                            Title = "SGIA - Sistema de Gereciamento de Informações Acadêmicas",
                            Email = EmailOutlook,
                            Link = Link,
                            SubjectMatter = "Email de redefinição de senha",
                            Message = "Clique no link para redefinir sua Senha."
                        };

                        Email.SubmitEmail(EmailSettings);

                        TempData["Success"] = "Email enviado com sucesso, verifique sua caixa de email.";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Email Inválido!";
                        return View();
                    }
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

                return RedirectToAction("Index");
            }
        }

        public IActionResult ChangePassword(string Guid)
        {
            try
            {
                IUserRepository user = new UserRepository();
                IUserPasswordRepository passRep = new UserPasswordRepository();

                var Password = passRep.Get(a => a.Guid == Guid).FirstOrDefault();
                var User = user.GetById(Password.UserId);

                ChangePasswordLoginViewModel Model = new ChangePasswordLoginViewModel
                {
                    Email = User.Email,
                    Guid = Guid,
                };

                return View(Model);
            }
            catch (Exception Error)
            {
                #region + Log

                Logs.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                _LogRep.AddAll(Logs);

                #endregion

                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordLoginViewModel Model)
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
                        IUserPasswordRepository passRep = new UserPasswordRepository();

                        passRep.ChangePassword(Model.Guid, Model.Email, Model.Password);

                        TempData["Success"] = "Senha Alterada com sucesso";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Email Inválido!";
                        return View();
                    }
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

                return RedirectToAction("Index");
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
                    Password.Guid = null;

                    passRep.Attach(Password);
                    TempData["Success"] = "Cadastro confirmado";

                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception Error)
            {
                #region + Log

                Logs.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                _LogRep.AddAll(Logs);

                #endregion

                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }

        public IActionResult Exit()
        {
            try
            {
                _LoginUser.Exit();

                return RedirectToAction("Index");
            }
            catch (Exception Error)
            {
                #region + Log

                Logs.Add(new Log
                {
                    Description = Error.Message,
                    Origin = "Login",
                    UserChangeId = 1
                });

                _LogRep.AddAll(Logs);

                #endregion

                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
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