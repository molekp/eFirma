using System.Web.Mvc;
using BussinessLogic.AuthorizationLogic;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Account;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers
{
    public class AccountController : Controller
    {

        public IMyLogOnValidation LogOnValidation { get; set; }
        public IFormsAuthentication FormsAuthenticationWrapper { get; set; }
        public IUserManagementLogic UserManagementLogic { get; set; }

        public AccountController()
        {
            LogOnValidation = new MyLogOnValidation();
            FormsAuthenticationWrapper = new FormsAuthenticationWrapper();
            UserManagementLogic = RepositoryLogicFactory.GetUserManagementLogic();
        }
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnDto a_logOnDto, string a_returnUrl)
        {
            if (false == ModelState.IsValid)
                return RedirectToLocal(a_returnUrl);

            bool isLogOnCorrect = LogOnValidation.CheckPassword(a_logOnDto.UserName, a_logOnDto.Password);
            if (false == isLogOnCorrect)
            {
                ModelState.AddModelError("wrongLogInError", Resource.wrongLogInError);
                return View("Login");
            }

            FormsAuthenticationWrapper.DoAuth(a_logOnDto.UserName, a_logOnDto.RememberMe);

            return RedirectToLocal(a_returnUrl);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthenticationWrapper.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserDto a_userDto)
        {
            if (ModelState.IsValid )
            {
                if(!UserManagementLogic.DoesUserExists(a_userDto.UserName)){
                
                    UserManagementLogic.CreateUser(a_userDto);
                    FormsAuthenticationWrapper.DoAuth(a_userDto.UserName, a_userDto.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ModelState.AddModelError("", Resource.userExists);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(a_userDto);
        }

        

        //
        // GET: /Account/Manage

        public ActionResult Manage(string a_returnUrl=null)
        {
            ViewBag.ReturnUrl = a_returnUrl;
            var model = UserManagementLogic.GetUserForEdit(User.Identity.Name);

            return View(model);
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(EditUserDto a_editUserDto)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
                if (ModelState.IsValid)
                {
                    bool isLogOnCorrect = LogOnValidation.CheckPassword(User.Identity.Name, a_editUserDto.OldPassword);
                    if (isLogOnCorrect)
                    {
                        bool changePasswordSucceeded = UserManagementLogic.EditUser(a_editUserDto);
                        if (changePasswordSucceeded)
                        {
                            return RedirectToAction("Details");
                        }
                        ModelState.AddModelError("", "Something went wrong. Pleas try again later.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect.");
                    }
                }
            return View(a_editUserDto);
        }
        

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details(string a_returnUrl = null)
        {
            ViewBag.ReturnUrl = a_returnUrl;
            var model = UserManagementLogic.GetUserForDetails(User.Identity.Name);
            return View(model);
        }
    }

}
