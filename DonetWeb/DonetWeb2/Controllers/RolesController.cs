using DonetWeb2.Results;
using EF_Identity;
using EFCoreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DonetWeb2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles= "RequireAdministratorRole")]
    public class RolesController : ControllerBase
    {

        private readonly RoleManager<Role> _roleManager;

        private readonly UsersAndRolesService usersAndRolesService;

        public RolesController(RoleManager<Role> roleManager, UsersAndRolesService usersAndRolesService)
        {
            _roleManager = roleManager;
            this.usersAndRolesService = usersAndRolesService;
        }
        //创建角色
        [HttpGet]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _roleManager.CreateAsync(new Role { Name=roleName });
            if(result.Succeeded)
            {
                return Ok(ResponseResult.Success("创建成功！"));
            }

            return Ok(ResponseResult.Success(result.Errors+"创建失败"));
        }


        //删除角色
        [HttpGet]
        public async Task<IActionResult> DelRole(string roleName)
        {
            Role role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(ResponseResult.Success("创建成功！"));
            }

            return Ok(ResponseResult.Success(result.Errors + "创建失败"));
        }

        //获取所有角色

        [HttpGet]
        public ResponseResult SearchAllRoles()
        {

            List<Role> roles = usersAndRolesService.GetRoles();
           

            return ResponseResult.Success(roles);
        }
        //获取指定角色所拥有的声明
        [HttpGet]
        public async Task<ResponseResult> GetRoleClaimsAsync(string roleName)
        {
            Role role = await _roleManager.FindByNameAsync(roleName);

            IList<System.Security.Claims.Claim> claims = await _roleManager.GetClaimsAsync(role);

            return ResponseResult.Success(claims);

        }

    }
}
