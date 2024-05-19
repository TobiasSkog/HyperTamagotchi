using HyperTamagotchi_MVC.Middleware;
using HyperTamagotchi_MVC.Repositories;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HyperTamagotchi_MVC;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpClient("API Tamagotchi", client =>
            client.BaseAddress = new Uri(
                builder.Configuration.GetValue<string>("ApiUri:Tamagotchi")!));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var userClaims = context.Principal.Identity as ClaimsIdentity;
                    if (userClaims != null)
                    {
                        var roleClaims = userClaims.FindAll(userClaims.RoleClaimType);
                        if (roleClaims != null && roleClaims.Any())
                        {
                            var claims = roleClaims.Select(role => new Claim(ClaimTypes.Role, role.Value)).ToList();
                            userClaims.AddClaims(claims);
                        }
                    }
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/Account/Login";
              options.AccessDeniedPath = "/Account/AccessDenied";
              options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ApiServices>();
        builder.Services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseJwtMiddleware();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}