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
            //�����ĵ���ȡ�����ַ���
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });

           
            //Identity��ʶ��ܵ�����
            IServiceCollection services = builder.Services;

            //��IdDbContext��������
            services.AddDbContext<IdDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
            
            services.AddDataProtection();
            //����AddIdentityCore��ӱ�ʶ��ܵ�һЩ��Ҫ�Ļ�������
            services.AddIdentityCore<User>(options =>
            {
                // �����븴�Ӷȿ�������
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<Role>();
            var idBuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            //��ΪUserManager��RoleManager�ȷ��񱻴�����ʱ����Ҫע��ǳ���ķ���
            //����������ʹ�ñ�ʶ��ܵ�ʱ��Ҳ��Ҫע��ͳ�ʼ���ǳ���ķ���
            idBuilder.AddEntityFrameworkStores<IdDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();
            //Services  ����ע��
            builder.Services.AddTransient<MusicService>();
            builder.Services.AddTransient<UsersAndRolesService>();
            //�����Ȩ������
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IAuthorizationHandler,MusicAuthorizationHandler>());

            //���û��ڲ��Ժ���������Ȩ
            builder.Services.AddAuthorization(options =>                                 
            {
                
                //������Ȩ
                //options.AddPolicy("MusicPower", policy => policy.RequireClaim(ClaimTypes.Actor, ClaimTypes.GivenName));
                //������Ȩ
                options.AddPolicy("Music", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "MusAdd","MusUpdate",",MusDelete","MusSelect"})));
                options.AddPolicy("Role", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "RoleAdd", "RoleUpdate", "RoleDelete", "RoleSelect" })));
                options.AddPolicy("User", policy => policy.Requirements.Add(new MusicRequirement(new List<string> { "UserAdd", "UserUpdate", "UserDelete", "UserSelect" })));
            });
            //���ÿ���
            string[] urls = new[] { "http://localhost:5173", "http://localhost:8080" };
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            //JWT����
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

            //�û���Ȩ
            app.UseAuthentication();

            //�û���Ȩ
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}