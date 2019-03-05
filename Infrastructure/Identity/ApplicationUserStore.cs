﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Data;

namespace Infrastructure.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser, int>
    {
        private IdentityContext _context;

        public ApplicationUserStore(IdentityContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = @"
                INSERT INTO ApplicationUser (
                    UserName, Title, Name, Surname, Cellphone, HomeAddress, PasswordHash) 
                VALUES (
                    @UserName, @Title, @Name, @surname, @Cellphone, @HomeAddress, @PasswordHash)";
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Title", user.Title);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Surname", user.Surname);
                cmd.Parameters.AddWithValue("@Cellphone", user.Cellphone);
                cmd.Parameters.AddWithValue("@HomeAddress", user.HomeAddress);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            ApplicationUser user = new ApplicationUser();

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = @"SELECT UserName, Title, Name, Surname, Cellphone, HomeAddress 
                                    FROM ApplicationUser 
                                    WHERE Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        user.Id = await reader.GetFieldValueAsync<int>(0);
                        user.UserName = await reader.GetFieldValueAsync<string>(1);
                        user.Name = await reader.GetFieldValueAsync<string>(3);
                        user.Surname = await reader.GetFieldValueAsync<string>(4);
                    }
                }
            }

            return user; 
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            ApplicationUser user = new ApplicationUser();

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, UserName, Title, Name, Surname, Cellphone, HomeAddress 
                                    FROM ApplicationUser 
                                    WHERE UserName = @UserName ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                {
                    while (await reader.ReadAsync())
                    {
                        user.Id = await reader.GetFieldValueAsync<int>(0);
                        user.UserName = await reader.GetTextReader(1).ReadToEndAsync();
                        user.Title = await reader.GetTextReader(2).ReadToEndAsync();
                        user.Name = await reader.GetTextReader(3).ReadToEndAsync();
                        user.Surname = await reader.GetTextReader(4).ReadToEndAsync();
                        user.Cellphone = await reader.GetTextReader(5).ReadToEndAsync();
                        user.HomeAddress = await reader.GetTextReader(6).ReadToEndAsync();
                    }
                }
            }

            return user;
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            string passwordHash = string.Empty;

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = "SELECT PasswordHash FROM ApplicationUser WHERE Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        passwordHash = reader.GetTextReader(0).ReadToEnd();
                    }
                }
            }

            return Task.FromResult<string>(passwordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            bool hasPassword = false;

            using (SqlCommand cmd = _context.CreateCommand())
            {
                cmd.CommandText = "SELECT PasswordHash FROM ApplicationUser WHERE Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hasPassword = !string.IsNullOrEmpty(reader.GetTextReader(0).ReadToEnd());
                    }
                }
            }

            return Task.FromResult<bool>(hasPassword);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult<Object>(null);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}