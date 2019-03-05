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

        public ApplicationUserManager mngr => HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

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
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

            return View(model);
        }
    }
}