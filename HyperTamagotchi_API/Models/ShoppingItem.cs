using HyperTamagotchi_API.Helpers.ExchangeRate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyperTamagotchi_API.Models;

public class ShoppingItem
{
    public int ShoppingItemId { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Item name must be between 1 and 50 characters")]
    [DisplayName("Product Name")]
    public virtual string Name { get; set; }


    [StringLength(100, MinimumLength = 0, ErrorMessage = "Description can maximum be of 100 characters")]
    public string? Description { get; set; }


    [Required]
    [Range(0, 256, ErrorMessage = "Stock must be between 0 and 256")]
    [DisplayName("Amount In Stock")]
    public byte Stock { get; set; }


    [Required]
    public float Price { get; set; }

    [Required]
    [DisplayName("Currency")]
    [Column(TypeName = "nvarchar(3)")]
    public CurrencyType CurrencyType { get; set; } = CurrencyType.SEK;

    [Required]
    [Range(0, 2, ErrorMessage = "Discount must be between 0 and 2")]
    public float Discount { get; set; } = 1.00f;

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Image path must be between 3 and 100 characters")]
    [DisplayName("Image Name (Image.jpg)")]
    public string ImagePath { get; set; } = @"404.jpg";

    // Shopping Cart specifics
    [Range(0, 256, ErrorMessage = "Quantity must be between 0 and 256")]
    public byte? Quantity { get; set; }
    public ICollection<ShoppingItemShoppingCart> Items { get; set; } = [];

    // Navigation to order
    public ICollection<ShoppingItemOrder> Orders { get; set; } = [];
}


//public static class ShoppingItemEndpoints
//{
//    public static void MapShoppingItemEndpoints(this IEndpointRouteBuilder routes)
//    {
//        var group = routes.MapGroup("/api/ShoppingItem").WithTags(nameof(ShoppingItem));

//        group.MapGet("/", async (ApplicationDbContext db) =>
//        {
//            return await db.ShoppingItems.ToListAsync();
//        })
//        .WithName("GetAllShoppingItems")
//        .WithOpenApi();

//        group.MapGet("/{id}", async Task<Results<Ok<ShoppingItem>, NotFound>> (int shoppingitemid, ApplicationDbContext db) =>
//        {
//            return await db.ShoppingItems.AsNoTracking()
//                .FirstOrDefaultAsync(model => model.ShoppingItemId == shoppingitemid)
//                is ShoppingItem model
//                    ? TypedResults.Ok(model)
//                    : TypedResults.NotFound();
//        })
//        .WithName("GetShoppingItemById")
//        .WithOpenApi();

//        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int shoppingitemid, ShoppingItem shoppingItem, ApplicationDbContext db) =>
//        {
//            var affected = await db.ShoppingItems
//                .Where(model => model.ShoppingItemId == shoppingitemid)
//                .ExecuteUpdateAsync(setters => setters
//                  .SetProperty(m => m.ShoppingItemId, shoppingItem.ShoppingItemId)
//                  .SetProperty(m => m.Name, shoppingItem.Name)
//                  .SetProperty(m => m.Description, shoppingItem.Description)
//                  .SetProperty(m => m.Stock, shoppingItem.Stock)
//                  .SetProperty(m => m.Price, shoppingItem.Price)
//                  .SetProperty(m => m.CurrencyType, shoppingItem.CurrencyType)
//                  .SetProperty(m => m.Discount, shoppingItem.Discount)
//                  .SetProperty(m => m.ImagePath, shoppingItem.ImagePath)
//                  .SetProperty(m => m.Quantity, shoppingItem.Quantity)
//                  );
//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("UpdateShoppingItem")
//        .WithOpenApi();

//        group.MapPost("/", async (ShoppingItem shoppingItem, ApplicationDbContext db) =>
//        {
//            db.ShoppingItems.Add(shoppingItem);
//            await db.SaveChangesAsync();
//            return TypedResults.Created($"/api/ShoppingItem/{shoppingItem.ShoppingItemId}", shoppingItem);
//        })
//        .WithName("CreateShoppingItem")
//        .WithOpenApi();

//        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int shoppingitemid, ApplicationDbContext db) =>
//        {
//            var affected = await db.ShoppingItems
//                .Where(model => model.ShoppingItemId == shoppingitemid)
//                .ExecuteDeleteAsync();
//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("DeleteShoppingItem")
//        .WithOpenApi();
//    }
//}