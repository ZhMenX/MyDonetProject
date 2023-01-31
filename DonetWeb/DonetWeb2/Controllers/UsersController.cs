using DonetWeb2.Results;
using EF_Identity;
using EFCoreModel.Entity;
using EFCoreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DonetWeb2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
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
                var r = await _userManager.CreateAsync(user, userRequest.Password);
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
        //修改用户
        [HttpGet]
        public ResponseResult UpdateUser()
        {

            List<User> users = userService.GetUsers();

            return ResponseResult.Success(users);
        }
        //删除用户
        [HttpGet]
        public ResponseResult DelUser()
        {

            List<User> users = userService.GetUsers();

            return ResponseResult.Success(users);
        }

    }
}
