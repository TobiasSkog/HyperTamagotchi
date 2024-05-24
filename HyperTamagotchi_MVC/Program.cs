using HyperTamagotchi_MVC.Middleware;
using HyperTamagotchi_MVC.Repositories;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HyperTamagotchi_MVC;
public class Program
{
    public static void Main(string[] args)
    {
        //client.BaseAddress = new Uri(builder.Configuration["ApiUri:Tamagotchi"]!));
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))

        var builder = WebApplication.CreateBuilder(args);

        var tamagotchiUri = Environment.GetEnvironmentVariable("TamagotchiUri") ?? builder.Configuration["ApiUri:Azure"];
        var issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? builder.Configuration["Jwt:Issuer"];
        var audience = Environment.GetEnvironmentVariable("JwtAudience") ?? builder.Configuration["Jwt:Audience"];
        var jwtkey = Environment.GetEnvironmentVariable("JwtKey") ?? builder.Configuration["Jwt:Key"];

        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient("API Tamagotchi", client =>
            client.BaseAddress = new Uri(tamagotchiUri!));

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
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey!))

            };
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.Name = "jwtToken";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = true;
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Home/Index";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ApiServices>();
        builder.Services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();

        //builder.Services.AddAntiforgery(options =>
        //{
        //    options.HeaderName = "X-XSRF-TOKEN";
        //});

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            //app.UseExceptionHandler("/Home/Error");
            //app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseJwtMiddleware();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        Console.WriteLine(tamagotchiUri);
        Console.WriteLine(issuer);
        Console.WriteLine(audience);
        Console.WriteLine(jwtkey);
        app.Run();
    }
}