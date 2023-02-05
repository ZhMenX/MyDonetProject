using DonetWeb2.JWT;
using DonetWeb2.Results;
using EF_Identity;
using EFCoreModel.Entity;
using EFCoreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace DonetWeb2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly UsersAndRolesService userService;

        private readonly RoleManager<Role> _roleManager;   

        public UsersController(UserManager<User> userManager, UsersAndRolesService userService, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            this.userService = userService;
            _roleManager = roleManager;
        }

        //查询指定用户
        [HttpGet]
        public ResponseResult SearchByName(string name)
        {
            List<User> userList = userService.GetUsersByName(name);
            return ResponseResult.Success(userList);
        }
        
        //为用户修改角色
        [HttpPost]
        public async Task<ResponseResult> UpdateRoleByUserName(UpdateRoleByUserNameRequest request)
        {
            //判断角色是否存在
            bool v = await _roleManager.RoleExistsAsync(request.UserName);
            if(v == true)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);
                var r = await _userManager.AddToRoleAsync(user, request.RoleName);
                if(r.Succeeded==false)
                {
                    return ResponseResult.Error(r.Errors,500);
                }
            }

            return ResponseResult.Success("修改成功!"); 
        }

        //获取所有用户
        [HttpGet]
        public ResponseResult SearchAllUsers()
        {

            List<User> users = userService.GetUsers();

            return ResponseResult.Success(users);
        }
        //添加用户
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(UserRequest userRequest)
        {
            User user = await _userManager.FindByNameAsync(userRequest.UserName);
            if (user == null)
            {
                user = new User
                {
                    UserName = userRequest.UserName,
                    Email = userRequest.email,
                    EmailConfirmed = true,
                };
                var r = await _userManager.CreateAsync(user, userRequest.newPassword);
                if (!r.Succeeded)
                {
                    return BadRequest(r.Errors);
                }
                //自动配置会员角色
                r = await _userManager.AddToRoleAsync(user, "member");
                if (!r.Succeeded)
                {
                    return BadRequest(r.Errors);
                }
            }
            return Ok();
        }
        //修改用户信息
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> UpdateUser(UserRequest request)
        {        
            User user = await _userManager.FindByNameAsync(request.UserName);
            user.Email = request.email;
            
            await _userManager.UpdateAsync(user); 
   
            return ResponseResult.Success(await _userManager.FindByNameAsync(request.UserName));
        }

        //修改用户密码
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> UpdateUserPassword(UserRequest request)
        {
            User user = new User { Email = request.email, UserName = request.UserName };
            IdentityResult identityResult = await _userManager.ChangePasswordAsync(user,request.currentPassword,request.newPassword);
            if (identityResult.Succeeded == true)
            {
                return ResponseResult.Success("修改密码成功！");
            }
            return ResponseResult.Error(identityResult.Errors,500);
        }


        //删除用户
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> DelUser(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            var r = await _userManager.DeleteAsync(user);
            if(r.Succeeded == true)
            {
                return ResponseResult.Success("删除成功！");
            }
            return ResponseResult.Error(r.Errors,500);
        }

    }
}
