using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRoleStore : IRoleStore<ApplicationRole, int>
    {
        private IdentityContext _context;

        public ApplicationRoleStore(IdentityContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task CreateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }


        public Task<ApplicationRole> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }
    }
}
