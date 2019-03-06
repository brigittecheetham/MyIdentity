using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Infrastructure.Identity;
using Web;
using Web.ViewModels;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        //resolve from the current http context
        public ApplicationUserManager mngr => HttpContext.GetOwinContext().Get<ApplicationUserManager>();
        public ApplicationSignInManager signInMngr => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Title = model.Title,
                Name = model.Name,
                Surname = model.Surname,
                Cellphone = model.Cellphone,
                HomeAddress = model.HomeAddress

            };

            var identityResult = await mngr.CreateAsync(user, model.Password);

            if (identityResult.Succeeded)
            {
                var currentUser = mngr.FindByName(user.UserName);

                await mngr.AddToRoleAsync(currentUser.Id, RoleName.GeneralUser);

                await signInMngr.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return RedirectToAction("Index", "Member");
            }

            ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var signInStatus = await signInMngr.PasswordSignInAsync(model.Email, model.Password, false, false);

            switch (signInStatus)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Member");
                default:
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View(model);
            }
        }

        public ActionResult LogOff()
        {
            signInMngr.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}