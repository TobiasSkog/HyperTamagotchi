using HyperTamagotchi_API.Helpers.ExchangeRate;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.TamagotchiProperties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
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
            Name = "Tamagotchi Food Pack",
            Description = "Nutritious food pack to keep your Tamagotchi healthy and happy.",
            Stock = (byte)200,
            Price = 75.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Food_Pack.png"
        },
        new
        {
            ShoppingItemId = 2,
            Name = "Tamagotchi Water Bottle",
            Description = "Portable water bottle to keep your Tamagotchi hydrated.",
            Stock = (byte)150,
            Price = 50.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Water_Bottle.png"
        },
        new
        {
            ShoppingItemId = 3,
            Name = "Tamagotchi Bed",
            Description = "Cozy bed for your Tamagotchi to sleep and rest comfortably.",
            Stock = (byte)100,
            Price = 200.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Bed.png"
        },
        new
        {
            ShoppingItemId = 4,
            Name = "Tamagotchi Exercise Wheel",
            Description = "Fun exercise wheel to keep your Tamagotchi active and fit.",
            Stock = (byte)120,
            Price = 150.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Exercise_Wheel.png"
        },
        new
        {
            ShoppingItemId = 5,
            Name = "Tamagotchi Cleaning Kit",
            Description = "Essential cleaning kit to maintain your Tamagotchi's hygiene.",
            Stock = (byte)80,
            Price = 100.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Cleaning_Kit.png"
        },
        new
        {
            ShoppingItemId = 6,
            Name = "Tamagotchi Toy Set",
            Description = "A set of fun toys to entertain your Tamagotchi.",
            Stock = (byte)170,
            Price = 60.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Toy_Set.png"
        },
        new
        {
            ShoppingItemId = 7,
            Name = "Tamagotchi Health Supplement",
            Description = "Vitamins and supplements for your Tamagotchi's wellbeing.",
            Stock = (byte)130,
            Price = 90.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Health_Supplement.png"
        },
        new
        {
            ShoppingItemId = 8,
            Name = "Tamagotchi Travel Carrier",
            Description = "Convenient carrier for traveling with your Tamagotchi safely.",
            Stock = (byte)90,
            Price = 180.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Travel_Carrier.png"
        },
        new
        {
            ShoppingItemId = 9,
            Name = "Tamagotchi Bath Kit",
            Description = "Complete bath kit to keep your Tamagotchi clean and fresh.",
            Stock = (byte)110,
            Price = 85.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Bath_Kit.png"
        },
        new
        {
            ShoppingItemId = 10,
            Name = "Tamagotchi First Aid Kit",
            Description = "Essential first aid items for your Tamagotchi's minor injuries.",
            Stock = (byte)75,
            Price = 120.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/First_Aid_Kit.png"
        },
        new
        {
            ShoppingItemId = 11,
            Name = "Tamagotchi Grooming Kit",
            Description = "Comprehensive grooming kit for your Tamagotchi's fur and nails.",
            Stock = (byte)95,
            Price = 110.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Grooming_Kit.png"
        },
        new
        {
            ShoppingItemId = 12,
            Name = "Tamagotchi Blanket",
            Description = "Soft and warm blanket for your Tamagotchi to snuggle in.",
            Stock = (byte)140,
            Price = 70.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Blanket.png"
        },
        new
        {
            ShoppingItemId = 13,
            Name = "Tamagotchi Feeding Dish",
            Description = "Stylish feeding dish perfect for serving Tamagotchi meals.",
            Stock = (byte)160,
            Price = 45.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Feeding_Dish.png"
        },
        new
        {
            ShoppingItemId = 14,
            Name = "Tamagotchi Sun Hat",
            Description = "Adorable sun hat to protect your Tamagotchi from the sun.",
            Stock = (byte)180,
            Price = 55.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Sun_Hat.png"
        },
        new
        {
            ShoppingItemId = 15,
            Name = "Tamagotchi ID Tag",
            Description = "Personalized ID tag with your Tamagotchi’s name and info.",
            Stock = (byte)200,
            Price = 35.00f,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = "Assets/ShoppingItem/Id_Tag.png"
        }
        );
        modelBuilder.Entity<Tamagotchi>().HasData(
            new
            {
                ShoppingItemId = 16,
                Name = "Rocker Rick",
                Description = "Meet Rocker Rick, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Rocker/Rocker_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Rocker,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 17,
                Name = "Rocker Rhonda",
                Description = "Meet Rocker Rhonda, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Rocker/Rocker_Child_Blue.png",
                TamagotchiColor = TamagotchiColor.Blue,
                TamagotchiType = TamagotchiType.Rocker,
                Mood = TamagotchiMood.Tired,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 18,
                Name = "Rocker Rex",
                Description = "Meet Rocker Rex, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Rocker/Rocker_Adult_Red.png",
                TamagotchiColor = TamagotchiColor.Red,
                TamagotchiType = TamagotchiType.Rocker,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
            },
            new
            {
                ShoppingItemId = 19,
                Name = "Farmer Fred",
                Description = "Meet Farmer Fred, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Farmer,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 20,
                Name = "Farmer Fiona",
                Description = "Meet Farmer Fiona, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Child_Green.png",
                TamagotchiColor = TamagotchiColor.Green,
                TamagotchiType = TamagotchiType.Farmer,
                Mood = TamagotchiMood.Hungry,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 21,
                Name = "Farmer Frank",
                Description = "Meet Farmer Frank, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Adult_Red.png",
                TamagotchiColor = TamagotchiColor.Red,
                TamagotchiType = TamagotchiType.Farmer,
                Mood = TamagotchiMood.Sad,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
            },
            new
            {
                ShoppingItemId = 22,
                Name = "Developer Darin",
                Description = "Meet Developer Darin, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Developer/Developer_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Developer,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 23,
                Name = "Developer Daisy",
                Description = "Meet Developer Daisy, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Developer/Developer_Child_Blue.png",
                TamagotchiColor = TamagotchiColor.Blue,
                TamagotchiType = TamagotchiType.Developer,
                Mood = TamagotchiMood.Bored,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 24,
                Name = "Developer Dave",
                Description = "Meet Developer Dave, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Developer/Developer_Adult_Green.png",
                TamagotchiColor = TamagotchiColor.Green,
                TamagotchiType = TamagotchiType.Developer,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
            },
            new
            {
                ShoppingItemId = 25,
                Name = "Athlete Alex",
                Description = "Meet Athlete Alex, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Athlete/Athlete_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Athlete,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 26,
                Name = "Athlete Annie",
                Description = "Meet Athlete Annie, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Athlete/Athlete_Child_Green.png",
                TamagotchiColor = TamagotchiColor.Green,
                TamagotchiType = TamagotchiType.Athlete,
                Mood = TamagotchiMood.Tired,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 27,
                Name = "Athlete Arnold",
                Description = "Meet Athlete Arnold, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Athlete/Athlete_Adult_Red.png",
                TamagotchiColor = TamagotchiColor.Red,
                TamagotchiType = TamagotchiType.Athlete,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
            },
            new
            {
                ShoppingItemId = 28,
                Name = "Constructor Colin",
                Description = "Meet Constructor Colin, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Constructor/Constructor_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Constructor,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 29,
                Name = "Constructor Cindy",
                Description = "Meet Constructor Cindy, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Constructor/Constructor_Child_Blue.png",
                TamagotchiColor = TamagotchiColor.Blue,
                TamagotchiType = TamagotchiType.Constructor,
                Mood = TamagotchiMood.Bored,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 30,
                Name = "Constructor Carl",
                Description = "Meet Constructor Carl, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Constructor/Constructor_Adult_Green.png",
                TamagotchiColor = TamagotchiColor.Green,
                TamagotchiType = TamagotchiType.Constructor,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
            },
            new
            {
                ShoppingItemId = 31,
                Name = "Philosopher Phil",
                Description = "Meet Philosopher Phil, the egg Tamagotchi.",
                Stock = (byte)100,
                Price = 200.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Philosopher/Philosopher_Egg_Default.png",
                TamagotchiColor = TamagotchiColor.Default,
                TamagotchiType = TamagotchiType.Philosopher,
                Mood = TamagotchiMood.Happy,
                TamagotchiStage = TamagotchiStage.Egg,
                Experience = (byte)0
            },
            new
            {
                ShoppingItemId = 32,
                Name = "Philosopher Pippa",
                Description = "Meet Philosopher Pippa, the child Tamagotchi.",
                Stock = (byte)100,
                Price = 250.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Philosopher/Philosopher_Child_Red.png",
                TamagotchiColor = TamagotchiColor.Red,
                TamagotchiType = TamagotchiType.Philosopher,
                Mood = TamagotchiMood.Hungry,
                TamagotchiStage = TamagotchiStage.Child,
                Experience = (byte)50
            },
            new
            {
                ShoppingItemId = 33,
                Name = "Philosopher Pete",
                Description = "Meet Philosopher Pete, the adult Tamagotchi.",
                Stock = (byte)100,
                Price = 300.00f,
                CurrencyType = CurrencyType.SEK,
                Discount = 1.00f,
                ImagePath = "Assets/Tamagotchi/Philosopher/Philosopher_Adult_Blue.png",
                TamagotchiColor = TamagotchiColor.Blue,
                TamagotchiType = TamagotchiType.Philosopher,
                Mood = TamagotchiMood.Sad,
                TamagotchiStage = TamagotchiStage.Adult,
                Experience = (byte)100
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
