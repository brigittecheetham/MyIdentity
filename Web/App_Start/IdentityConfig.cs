using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Infrastructure.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Web
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store) : base(store) { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(new IdentityContext()));

            manager.UserLockoutEnabledByDefault = false;

            return manager;
        }
    }
}