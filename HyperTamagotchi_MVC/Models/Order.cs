using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_MVC.Models;

public class Order
{
    public int OrderId { get; set; }

    // Limit the ASP.NET GUID ID Strings to less than 450...
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippingDate { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public ICollection<ShoppingItemOrder> Items { get; set; } = [];
}
