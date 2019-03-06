using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Identity;
using Web.ViewModels;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        public ApplicationUserManager mngr => HttpContext.GetOwinContext().Get<ApplicationUserManager>();

        public ActionResult Index()
        {

            var members = mngr.Users.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                HomeAddress = x.HomeAddress,
                Cellphone = x.Cellphone,
                UserName = x.UserName,
                Title = x.Title
            }).ToList();


            if (User.IsInRole(RoleName.Administrator))
            {
                return View("Index", members);
            }

            return View("IndexReadOnly", members);
        }

        public async Task<ActionResult> Edit(int Id)
        {

            var member = await mngr.FindByIdAsync(Id);

            MemberViewModel model = new MemberViewModel
            {
                Cellphone = member.Cellphone,
                HomeAddress = member.HomeAddress,
                Id = member.Id,
                Name = member.Name,
                Surname = member.Surname,
                Title = member.Title,
                UserName = member.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MemberViewModel member)
        {
            ApplicationUser user = new ApplicationUser
            {
                Cellphone = member.Cellphone,
                HomeAddress = member.HomeAddress,
                Id = member.Id,
                Name = member.Name,
                Surname = member.Surname,
                Title = member.Title,
                UserName = member.UserName
            };

            await mngr.UpdateAsync(user);

            return RedirectToAction("Index", "Member");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var user = await mngr.FindByIdAsync(Id);
            await mngr.DeleteAsync(user);

            return RedirectToAction("Index", "Member");
        }
    }
}