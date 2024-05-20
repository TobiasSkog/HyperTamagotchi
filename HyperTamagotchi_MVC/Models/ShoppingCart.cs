namespace HyperTamagotchi_MVC.Models;

public class ShoppingCart
{
    public int ShoppingCartId { get; set; }
    public List<ShoppingItem> ShoppingItems { get; set; } = [];
}
