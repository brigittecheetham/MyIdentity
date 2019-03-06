using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index()
        {
            List<MemberViewModel> members = new List<MemberViewModel>();

            if (User.IsInRole(RoleName.Administrator))
            {
                return View("Index", members);
            }

            return View("IndexReadOnly", members);
        }

        public ActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(MemberViewModel member)
        {
            return RedirectToAction("Index", "Member");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            return RedirectToAction("Index", "Member");
        }



    }
}