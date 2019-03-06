using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Data;

namespace Infrastructure.Identity
{
    public class ApplicationRoleStore : IRoleStore<ApplicationRole, int>, IQueryableRoleStore<ApplicationRole,int>
    {
        private IdentityContext _context;

        public IQueryable<ApplicationRole> Roles
        {
            get
            {
                List<ApplicationRole> roles = new List<ApplicationRole>();

                using (SqlCommand cmd = _context.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name
                                    FROM ApplicationRole";
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            roles.Add(new ApplicationRole
                            {
                                Id = reader.GetFieldValue<int>(0),
                                Name = reader.GetTextReader(1).ReadToEnd()
                            });
                        }
                    }
                }

                return roles.AsQueryable();

            }
        }

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


        public async Task<ApplicationRole> FindByIdAsync(int roleId)
        {
            ApplicationRole role = new ApplicationRole();

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, Name
                                    FROM ApplicationRole
                                    WHERE Name = @roleId ";
                cmd.Parameters.AddWithValue("@roleId", roleId);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                {
                    while (await reader.ReadAsync())
                    {
                        role.Id = await reader.GetFieldValueAsync<int>(0);
                        role.Name = await reader.GetTextReader(1).ReadToEndAsync();
                    }
                }
            }

            return role;
        }

        public async Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            ApplicationRole role = new ApplicationRole();

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, Name
                                    FROM ApplicationRole
                                    WHERE Name = @roleName ";
                cmd.Parameters.AddWithValue("@roleName", roleName);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                {
                    while (await reader.ReadAsync())
                    {
                        role.Id = await reader.GetFieldValueAsync<int>(0);
                        role.Name = await reader.GetTextReader(1).ReadToEndAsync();
                    }
                }
            }

            return role;
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }
    }
}
