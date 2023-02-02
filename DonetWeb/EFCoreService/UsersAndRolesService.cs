using EF_Identity;
using Microsoft.AspNetCore.Identity;

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

        //获取指定角色
        public List<Role> GetRolesByName(string name)
        {
            List<Role> roles = context.Roles.Where(r=>r.Name.Contains(name).Equals(true)).ToList();

            return roles;
        }

        //获取所有用户
        public List<User> GetUsers()
        {
            List<User> users = context.Users.Where(u => u.Id > 0).ToList();
            
            return users;
        }

        //获取指定用户
        public List<User> GetUsersByName(string name)
        {

            List<User> users = context.Users.Where(u=>u.UserName.Contains(name)).ToList();
            return users;
        }


    }
}
