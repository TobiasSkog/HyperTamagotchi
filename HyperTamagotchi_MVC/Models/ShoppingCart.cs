namespace HyperTamagotchi_SharedModels.Models;

public class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public Customer Customer { get; set; }

    public ICollection<ShoppingItem> Items { get; set; } = [];
}
