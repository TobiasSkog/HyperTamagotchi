namespace HyperTamagotchi_MVC.Models;

public class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }


    public ICollection<ShoppingItemShoppingCart> Items { get; set; } = [];
}
