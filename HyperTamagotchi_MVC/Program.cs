using HyperTamagotchi_MVC.Repositories;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

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


        // Configure the HTTP request pipeline.
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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


//    var roles = new[] { "Admin", "Customer" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }
//}

//using (var scope = app.Services.CreateScope())
//{
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//    var users = new[] { "carro", "daniel", "tobias", "wille" };
//    var emails = new[] { ".admin@gmail.com", ".customer@gmail.com" };
//    string password = "Abc123!";

//    // if no account found with above user create a new user
//    foreach (var user in users)
//    {
//        foreach (var email in emails)
//        {
//            if (await userManager.FindByEmailAsync($"{user}{email}") == null)
//            {
//                IdentityUser appUser = new()
//                {
//                    Email = user + email,
//                    UserName = user + email,
//                    EmailConfirmed = true
//                };

//                await userManager.CreateAsync(appUser, password);
//                var role = email.Contains("admin") ? "Admin" : "Customer";
//                await userManager.AddToRoleAsync(appUser, role);
//            }
//        }
//    }
//}