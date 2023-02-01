using EF_Identity;
using EFCoreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DonetWeb2.Authorization
{
    public class MusicAuthorizationHandler : AuthorizationHandler<MusicRequirement>
    {

        private readonly UsersAndRolesService usersAndRolesService;

        public MusicAuthorizationHandler( UsersAndRolesService usersAndRolesService)
        {
            this.usersAndRolesService = usersAndRolesService;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, MusicRequirement requirement)
        {


            //从数据库中获取权限信息，这里使用声明来表示

            List<string> values = await usersAndRolesService.GetClaimsValueByRoleAsync();
            
            //如果为空
            if (values.LongCount() == 0)
            {
                return Task.CompletedTask;
            }
            //验证角色是否权限
            foreach(var value in values)
            {
                if(requirement.Permissions.Contains(value))
                {
                    context.Succeed(requirement);
                }
            }


            return Task.CompletedTask;
        }
    }
}
