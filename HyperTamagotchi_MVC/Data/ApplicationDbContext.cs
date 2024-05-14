using HyperTamagotchi_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_MVC.Data;
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
