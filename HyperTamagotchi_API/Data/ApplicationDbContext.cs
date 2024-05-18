using HyperTamagotchi_API.Helpers.ExchangeRate;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.TamagotchiProperties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingItemOrder>().HasKey(sio => new { sio.ShoppingItemId, sio.OrderId });
        modelBuilder.Entity<ShoppingItemShoppingCart>().HasKey(sisc => new { sisc.ShoppingItemId, sisc.ShoppingCartId });

        modelBuilder.Entity<Order>()
            .HasIndex(o => o.OrderDate);

        modelBuilder.Entity<ShoppingItem>()
           .HasDiscriminator<string>("Discriminator")
           .HasValue<ShoppingItem>("ShoppingItem")
           .HasValue<Tamagotchi>("Tamagotchi");

        var adminRoleId = Guid.NewGuid().ToString();
        var customerRoleId = Guid.NewGuid().ToString();
        var adminUserId = Guid.NewGuid().ToString();
        var customerUserId = Guid.NewGuid().ToString();
        var hasher = new PasswordHasher<Customer>();

        modelBuilder.Entity<IdentityRole>().HasData(
            new { Id = adminRoleId, Name = "Admin", NormalizedName = "Admin".ToUpper() },
            new { Id = customerRoleId, Name = "Customer", NormalizedName = "Customer".ToUpper() }
            );

        modelBuilder.Entity<Address>().HasData(
            new { AddressId = 1, StreetAddress = "Timmermansgatan 2A", City = "Kiruna", ZipCode = "98137" },
            new { AddressId = 2, StreetAddress = "Rundvägen 11D", City = "Örnsköldsvik", ZipCode = "89144" }
            );
        modelBuilder.Entity<ShoppingCart>().HasData(
            new { ShoppingCartId = 1, CustomerId = adminUserId },
            new { ShoppingCartId = 2, CustomerId = customerUserId }
            );

        modelBuilder.Entity<Customer>().HasData(
            new
            {
                Id = adminUserId,
                FirstName = "Admin",
                LastName = "Adminsson",
                AddressId = 1,
                ShoppingCartId = 1,
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = (DateTimeOffset?)null,
                LockoutEnabled = true,
                AccessFailedCount = 0

            },
            new
            {
                Id = customerUserId,
                FirstName = "Tobias",
                LastName = "Skog",
                AddressId = 2,
                ShoppingCartId = 2,
                UserName = "tobias@gmail.com",
                NormalizedUserName = "tobias@gmail.com".ToUpper(),
                Email = "tobias@gmail.com",
                NormalizedEmail = "tobias@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = (DateTimeOffset?)null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );


        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new { UserId = adminUserId, RoleId = adminRoleId },
            new { UserId = customerUserId, RoleId = customerRoleId }
            );

        modelBuilder.Entity<ShoppingItem>().HasData(
            new
            {
                ShoppingItemId = 1,
                Name = "Banana",
                Description = "Restores 10 energy to your Tamagotchi",
                Stock = (byte)50,
                Price = 25.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "none.png"
            },
            new
            {
                ShoppingItemId = 2,
                Name = "Sports Drank",
                Description = "Restores 25 energy to your Tamagotchi",
                Stock = (byte)25,
                Price = 50.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "none.png"
            },
            new
            {
                ShoppingItemId = 3,
                Name = "Rice",
                Description = "Restores 1 energy to your Tamagotchi",
                Stock = (byte)250,
                Price = 10.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "none.png"
            }
            );
        modelBuilder.Entity<Tamagotchi>().HasData(
            new
            {
                ShoppingItemId = 4,
                Name = "Developer Darin",
                Description = "Meet the young developer Darin",
                Stock = (byte)10,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Developer/Dev_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Developer,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 5,
                Name = "Developer Juaaaahhhn",
                Description = "Meet the senior developer Juaaaahhhn",
                Stock = (byte)2,
                Price = 255.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Developer/Dev_Child_Green.png",
                TamagotchiColor = TamagotchiColor.Green,
                TamagotchiType = TamagotchiType.Developer,
                Mood = TamagotchiMood.Tired,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 6,
                Name = "Farmer Shaarraaa",
                Description = "Meet the farmer Shaarraaa",
                Stock = (byte)6,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Child_Blue.png",
                TamagotchiColor = TamagotchiColor.Blue,
                TamagotchiType = TamagotchiType.Farmer,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 7,
                Name = "Farmer Ghäärryyy",
                Description = "Meet the farmer Ghäärryyy",
                Stock = (byte)3,
                Price = 50.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Egg_Red.png",
                TamagotchiColor = TamagotchiColor.Red,
                TamagotchiType = TamagotchiType.Farmer,
                Mood = TamagotchiMood.Bored,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            }
            );
        // This line is used if using identity and if having a overrided OnModelCreating
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Address> Address { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingItem> ShoppingItems { get; set; }
    public DbSet<ShoppingItemOrder> ShoppingItemOrders { get; set; }
    public DbSet<ShoppingItemShoppingCart> ShoppingItemShoppingCarts { get; set; }
    public DbSet<Tamagotchi> Tamagotchis { get; set; }
}
