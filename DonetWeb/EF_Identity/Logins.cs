using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Identity
{
    public record LoginRequest(string UserName, string Password);

    public record UserRequest(string UserName, string Password,string email);

    public record UpdateRoleByUserNameRequest(string UserName, string RoleName);
}
