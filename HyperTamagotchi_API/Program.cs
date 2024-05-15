using HyperTamagotchi_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API;
public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add services to the container.
        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

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

        app.Run();
    }
}
