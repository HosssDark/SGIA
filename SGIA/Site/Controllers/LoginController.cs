using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.libraries.Login;
using Functions;
using System;

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

                if (string.IsNullOrEmpty(Model.Email))
                {
                    if (FunctionsValidate.ValidateEmail(Model.Email))
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
                        var password = _RepositoryUserPass.VerificationPassword(Model.Password);

                        if (password != null)
                        {
                            _LoginUser.SetUser(model);

                            return new RedirectResult(Url.Action("DashBoard", "Home"));
                        }
                        else
                        {
                            ViewData["Error"] = "Senha Inválido!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewData["Error"] = "Email Inválido!";
                        return View();
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

                if (string.IsNullOrEmpty(Model.Email))
                {
                    if (FunctionsValidate.ValidateEmail(Model.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("Email", "Obrigatório");

                if (string.IsNullOrEmpty(Model.Password))
                    ModelState.AddModelError("Password", "Obrigatório");

                if (string.IsNullOrEmpty(Model.ConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Obrigatório");

                if (Model.Password == Model.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Senha não ");

                #endregion

                if (ModelState.IsValid)
                {


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

                if (string.IsNullOrEmpty(Model.Email))
                {
                    if (FunctionsValidate.ValidateEmail(Model.Email))
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
            catch (Exception)
            {
                ViewData["Error"] = "Erro Inesperado!";
                return View();
            }
        }
    }
}