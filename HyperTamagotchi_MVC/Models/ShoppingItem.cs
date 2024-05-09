using HyperTamagotchi_MVC.Models.ExchangeRate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyperTamagotchi_MVC.Models;

public class ShoppingItem
{
    public int ShoppingItemId { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Item name must be between 1 and 50 characters")]
    [DisplayName("Product")]
    public string ShoppingItemName { get; set; }


    [StringLength(100, MinimumLength = 0, ErrorMessage = "Description can maximum be of 100 characters")]
    public string? Description { get; set; }


    [Required]
    [Range(0, 256, ErrorMessage = "Stock must be between 0 and 256")]
    [DisplayName("Amount In Stock")]
    public byte Stock { get; set; }

    //How to maximize cost efficiency when using Azure and the decimal datatype?

    [Required]
    public float Price { get; set; }

    [Required]
    [DisplayName("Currency")]
    [Column(TypeName = "nvarchar(3)")]
    public CurrencyType CurrencyType { get; set; } = CurrencyType.SEK;

    [Required]
    [Range(0, 2, ErrorMessage = "Discount must be between 0 and 2")]
    public float Discount { get; set; } = 1.00f;

    // Shopping Cart specifics
    [Range(0, 256, ErrorMessage = "Quantity must be between 0 and 256")]
    public byte? Quantity { get; set; }
    public ICollection<ShoppingItemShoppingCart> Items { get; set; } = [];

    // Navigation to order
    public ICollection<ShoppingItemOrder> Orders { get; set; } = [];
}
