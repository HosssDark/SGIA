using Microsoft.AspNetCore.Mvc;
using Repository;
using Functions;
using System;
using Domain;
using System.Linq;
using System.Collections.Generic;
using static Site.Notification;

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
                            var password = _RepositoryUserPass.VerificationPassword(Model.Password);

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
            catch (Exception erro)
            {
                #region + Log

                Logs.Add(new Log
                {
                    Description = erro.Message,
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


                    ViewData["Success"] = "";
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception erro)
            {
                ViewData["Error"] = erro.Message;
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



                        //ViewData["Msg"] = retorno;
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

        public IActionResult Exit()
        {
            try
            {
                _LoginUser.Exit();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
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

                List.Add(new NotificationList
                {
                    Type = "Success",
                    Message = Message
                });
            }

            if (TempData.ContainsKey("Error"))
            {
                TempData.TryGetValue("Error", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                List.Add(new NotificationList
                {
                    Type = "Error",
                    Message = Message
                });
            }

            if (TempData.ContainsKey("Info"))
            {
                TempData.TryGetValue("Info", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                List.Add(new NotificationList
                {
                    Type = "Info",
                    Message = Message
                });
            }

            return Json(new { List = List });
        }
    }
}