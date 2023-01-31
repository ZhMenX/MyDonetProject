using DonetWeb2.JWT;
using DonetWeb2.Results;
using EF_Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DonetWeb2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public LoginController(ILogger<LoginController> logger, UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _logger = logger;
            _userManager = userManager;   
            _roleManager = roleManager;
        }


        //登录
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest,[FromServices] IOptions<JWTOptions> jwtOptions)
        {

            string userName = loginRequest.UserName;
            string password = loginRequest.Password;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound($"用户名{userName}不存在!");
            }
            var islocked = await _userManager.IsLockedOutAsync(user);
            if (islocked)
            {
                return BadRequest("用户已锁定！");
            }
            var success = await _userManager.CheckPasswordAsync(user, password);

            //生成JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string jwtToken = BuildToken(claims, jwtOptions.Value);

            if (success)
            {
                //如果登录成功返回登陆者数据
                var LoginUser = await _userManager.FindByNameAsync(userName);
                var isAdmin = await _userManager.IsInRoleAsync(LoginUser, "admin");

                return Ok(ResponseResult.LoginSuccess(LoginUser,jwtToken,isAdmin));
            }
            else
            {
                var r = await _userManager.AccessFailedAsync(user);
                if (!r.Succeeded)
                {
                    return BadRequest("访问失败信息写入错误！");
                }
                else
                {
                    return BadRequest("失败！");
                }
            }

        }
        //注销登录
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
        private static string BuildToken(IEnumerable<Claim> claims, JWTOptions options)
        {
            DateTime expires = DateTime.Now.AddSeconds(options.ExpireSeconds);
            byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
            var secKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(secKey,
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(expires: expires,
                signingCredentials: credentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
