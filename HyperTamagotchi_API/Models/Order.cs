using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_API.Models;

public class Order
{
    public int OrderId { get; set; }

    public string CustomerId { get; set; }
    public Customer Customer { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippingDate { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
