namespace HyperTamagotchi_MVC.Models;

public class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public Customer Customer { get; set; }

    public ICollection<ShoppingItem> Items { get; set; } = [];
}
