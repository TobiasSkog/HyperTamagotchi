namespace HyperTamagotchi_API.Models;

public class ShoppingItemOrder
{
    public int ShoppingItemId { get; set; }
    public ShoppingItem ShoppingItem { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
}
