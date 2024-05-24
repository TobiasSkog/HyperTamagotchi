using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Mapper;
using HyperTamagotchi_API.Models.GoogleMaps;
using HyperTamagotchi_API.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HyperTamagotchi_API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var issuer = Environment.GetEnvironmentVariable("JwtIssuer");
        issuer ??= builder.Configuration["Jwt:Issuer"];
        var audience = Environment.GetEnvironmentVariable("JwtAudience");
        audience ??= builder.Configuration["Jwt:Audience"];
        var jwtkey = Environment.GetEnvironmentVariable("JwtKey");
        jwtkey ??= builder.Configuration["Jwt:Key"];
        var googleMapsApiKey = Environment.GetEnvironmentVariable("GoogleMapsKey");
        googleMapsApiKey ??= builder.Configuration["ApiKey:GoogleMaps"];

        builder.Services.AddHttpClient<TimeDelivery>()
                .ConfigureHttpClient(client =>
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                });

        builder.Services.AddTransient<TimeDelivery>(provider =>
        {
            var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
            var apiKey = googleMapsApiKey;
            var logger = provider.GetRequiredService<ILogger<TimeDelivery>>();
            return new TimeDelivery(httpClient, apiKey, logger);
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddControllers();


        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAutoMapper(typeof(TamagotchiApiMappingProfile));

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tamagotchi API", Version = "v0.01" });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {

                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "Oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("AzureConnection")));

        builder.Services.AddScoped<IJwtService, JwtService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "Tamagotchi",
                policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

        });
        //options.AddPolicy(name: "AllowSpecificOrigin",
        //    policy =>
        //    {
        //        policy.WithOrigins("https://hypertamagotchimvc.azurewebsites.net")
        //              .AllowAnyMethod()
        //              .AllowAnyHeader();
        //    });

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
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("Tamagotchi");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        //await ControllDatabaseForData(app);
        Console.WriteLine(issuer);
        Console.WriteLine(audience);
        Console.WriteLine(jwtkey);
        app.Run();
    }

    //static async Task ControllDatabaseForData(WebApplication app)
    //{
    //    using (var scope = app.Services.CreateScope())
    //    {
    //        var services = scope.ServiceProvider;
    //        try
    //        {
    //            var context = services.GetRequiredService<ApplicationDbContext>();
    //            await context.SeedDataAsync(services);
    //        }
    //        catch (Exception ex)
    //        {
    //            var logger = services.GetRequiredService<ILogger<Program>>();
    //            logger.LogError(ex, "An error occured creating the DB.");
    //        }
    //    }
    //}
}