using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CurricularUnitFormManager.Models.Account;
using CurricularUnitFormManager.Models.Individual;
using CurricularUnitFormManager.Models.Mail;
using CurricularUnitFormManager.Models.Repository;

namespace CurricularUnitFormManager.Controllers
{
    public class AccountController : Controller
    {
        private const String ErrorMsgLoginFailed = "Utilizador ou password inválida.";

        readonly static IndividualRepository _userRepository = IndividualRepository.Instance;


        public ActionResult Index()
        {
            return View("Login");
        }

        #region Activate Account

        public ActionResult Activate(int ActivationCode)
        {
            AccountModels.ActivateModel model = new AccountModels.ActivateModel();
            model.ActivationCode = ActivationCode;
            return View(model);
        }


        [HttpPost]
        public ActionResult Confirm(AccountModels.ActivateModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.ConfirmRegistration(model.UserName, model.Password, model.ActivationCode))
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    HttpCookie cookie = new HttpCookie("user", model.UserName);
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Data Invalid");
            return View(model);
        }
        #endregion

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountModels.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.Register(model))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Data Invalid");
            return View(model);
        }

        #endregion

        #region Passwords
        
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(AccountModels.ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = _userRepository.ChangePassWord(((Individual)currentUser).UserName,model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        
        #endregion

        #region Login/Logout
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            
            HttpCookie cookie = new HttpCookie("user");
            cookie.Expires = new DateTime(1900,01,01,00,00,00);
            HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(AccountModels.LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    HttpCookie cookie = new HttpCookie("user", model.UserName);
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);


        //    if (Models.Individual.IndividualAuthentication.authenticateUser(username, password))
        //    {
        //        HttpCookie authCookie = CreateAuthCookie(username, isPersistent);

        //        Response.Cookies.Add(authCookie);

        //        result = Request.IsAjaxRequest()
        //                     ? Content(Url.Action("Index", "Home"), "text/plain") as ActionResult
        //                     : RedirectToAction("Index", "Home") as ActionResult;

        //        //return Content(FormsAuthentication.GetRedirectUrl(username, false));
        //    }
        //    else
        //    {
        //        // Authentication failed
        //        result = Request.IsAjaxRequest()
        //                     ? Content("{ \"errorMessage\": \"" + ErrorMsgLoginFailed + "\"}", "application/json") as ActionResult
        //                     : RedirectToAction("Index", "Home") as ActionResult;

        //        if (Request.IsAjaxRequest())
        //            TempData["errorMessage"] = ErrorMsgLoginFailed;

        //        //Response.StatusCode = 401;
        //        //Response.StatusDescription = "Authentication Failed, user id and password doesn't match";
        //        //FormsAuthentication.RedirectToLoginPage();

        //    }

        //    return result;

            
        }

        //cria um cookie autorizado
        //private static HttpCookie CreateAuthCookie(String username, bool isPersistent)
        //{
        //    var ticket = new FormsAuthenticationTicket(1, // Version
        //                                                   username, // Username
        //                                                   DateTime.Now, // Issue date
        //                                                   DateTime.Now.AddYears(10), // Expiration
        //                                                   isPersistent, // isPersistent
        //                                                   "", // User data
        //                                                   "/"); // Path

        //    HttpCookie authCookie = new HttpCookie(Models.User.AuthCookieName, FormsAuthentication.Encrypt(ticket))
        //    {
        //        Path = ticket.CookiePath
        //    };

        //    // Um cookie é de sessão se a data de expiração não for afectada
        //    if (ticket.IsPersistent)
        //        authCookie.Expires = ticket.Expiration;

        //    return authCookie;
        //}

        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

    }
}
