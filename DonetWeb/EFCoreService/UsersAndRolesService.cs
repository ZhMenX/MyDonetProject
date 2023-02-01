using EF_Identity;
using EFCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreService
{
    public class UsersAndRolesService : IUsersAndRolesService
    {
        private readonly IdDbContext context;

        private readonly RoleManager<Role> roleManager;

        public UsersAndRolesService(IdDbContext context, RoleManager<Role> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<List<string>> GetClaimsValueByRoleAsync()
        {
            List<Role> roles =GetRoles();
            List<string> values = new List<string>();
            foreach (var role in roles)
            {
                IList<System.Security.Claims.Claim> claims = await roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    values.Add(claim.Value);
                }
            }
            return values;
        }

        //获取所有角色
        public List<Role> GetRoles()
        {
            return context.Roles.Where(r => r.Id > 0).ToList();
        }
        //获取所有用户
        public List<User> GetUsers()
        {
            List<User> users = context.Users.Where(u => u.Id > 0).ToList();
            
            return users;
        }

 
    }
}
