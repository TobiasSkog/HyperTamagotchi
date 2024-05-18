namespace HyperTamagotchi_SharedModels.Models;

public class Order
{
    public int OrderId { get; set; }

    public Customer Customer { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippingDate { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public ICollection<ShoppingItem> Items { get; set; } = [];
}
