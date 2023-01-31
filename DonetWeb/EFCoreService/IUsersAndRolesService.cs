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

        //获取
    }

}
