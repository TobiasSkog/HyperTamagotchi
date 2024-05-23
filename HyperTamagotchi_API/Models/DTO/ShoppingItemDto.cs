using HyperTamagotchi_API.Helpers.ExchangeRate;

namespace HyperTamagotchi_API.Models.DTO;

public class ShoppingItemDto
{
    public int ShoppingItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte Stock { get; set; }
    public float Price { get; set; }
    public string ImagePath { get; set; }
    public byte? Quantity { get; set; }
    public CurrencyType CurrencyType { get; set; } = CurrencyType.SEK;
    public float Discount { get; set; } = 1.00f;
}
