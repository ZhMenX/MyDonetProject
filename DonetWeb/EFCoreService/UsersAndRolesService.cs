using EF_Identity;
using EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreService
{
    public class UsersAndRolesService : IUsersAndRolesService
    {
        private readonly IdDbContext context;

        public UsersAndRolesService(IdDbContext context)
        {
            this.context = context;
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
