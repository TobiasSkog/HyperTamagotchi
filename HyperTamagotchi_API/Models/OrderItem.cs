namespace HyperTamagotchi_API.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int ShoppingItemId { get; set; }
    public ShoppingItem ShoppingItem { get; set; }
    public byte? Quantity { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; } = 1.00f;
}