using EF_Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreService
{
    public interface IUsersAndRolesService
    {
        //获取所有用户
        List<User> GetUsers();

        //获取角色
        List<Role> GetRoles();

        //获取所有角色所拥有的声明
        Task<List<string>> GetClaimsValueByRoleAsync();

        List<User> GetUsersByName(string name);

        List<Role> GetRolesByName(string name);


    }

}
