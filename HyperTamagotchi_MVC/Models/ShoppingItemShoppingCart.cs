namespace HyperTamagotchi_SharedModels.Models;

public class ShoppingItemShoppingCart
{
    public int ShoppingItemId { get; set; }
    public ShoppingItem ShoppingItem { get; set; }

    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}
