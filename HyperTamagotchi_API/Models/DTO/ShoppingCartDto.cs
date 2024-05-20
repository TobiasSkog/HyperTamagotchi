namespace HyperTamagotchi_API.Models.DTO;

public class ShoppingCartDto
{
    public int ShoppingCartId { get; set; }
    public ICollection<ShoppingItem> ShoppingItems { get; set; } = [];
}