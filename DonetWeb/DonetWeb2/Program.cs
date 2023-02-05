using DonetWeb2.Authorization;
using DonetWeb2.JWT;
using EF_Identity;
using EFCore;
using EFCoreService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Zack.Commons;

namespace DonetWeb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                };
                c.AddSecurityDefinition("Authorization", scheme);
                var requirement = new OpenApiSecurityRequirement();
                requirement[scheme] = new List<string>();
                c.AddSecurityRequirement(requirement);
            });
            //配置文档获取连接字符串
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });

           
            //Identity标识框架的配置
            IServiceCollection services = builder.Services;

            //对IdDbContext进行配置
            services.AddDbContext<IdDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
            
            services.AddDataProtection();
            //调用AddIdentityCore添加标识框架的一些重要的基础服务
            services.AddIdentityCore<User>(options =>
            {
                // 对密码复杂度苛刻设置
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<Role>();
            var idBuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            //因为UserManager、RoleManager等服务被创建的时候需要注入非常多的服务，
            //所以我们在使用标识框架的时候也需要注入和初始化非常多的服务
            idBuilder.AddEntityFrameworkStores<IdDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();
            //Services  依赖注入
            builder.Services.AddTransient<MusicService>();
            builder.Services.AddTransient<UsersAndRolesService>();
            //添加授权处理器
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IAuthorizationHandler,MusicAuthorizationHandler>());

            //配置基于策略和声明的授权
            builder.Services.AddAuthorization(options =>                                 
            {
                
                //声明授权
                //options.AddPolicy("MusicPower", policy => policy.RequireClaim(ClaimTypes.Actor, ClaimTypes.GivenName));
                //策略授权
                options.AddPolicy("Music", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "MusAdd","MusUpdate",",MusDelete","MusSelect"})));
                options.AddPolicy("Role", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "RoleAdd", "RoleUpdate", "RoleDelete", "RoleSelect" })));
                options.AddPolicy("User", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "UserAdd", "UserUpdate", "UserDelete", "UserSelect" })));
            });
            //配置跨域
            string[] urls = new[] { "http://localhost:5173", "http://localhost:8080" };
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            //JWT配置
            services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.SigningKey);
                    var secKey = new SymmetricSecurityKey(keyBytes);
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = secKey
                    };
                });

            var app = builder.Build();
                
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            //用户鉴权
            app.UseAuthentication();

            //用户授权
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}