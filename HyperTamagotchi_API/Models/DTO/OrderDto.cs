namespace HyperTamagotchi_API.Models.DTO;

public class OrderDto
{
    public int OrderId { get; set; }

    public CustomerDto Customer { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippingDate { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public List<ShoppingItemDto> Items { get; set; } = [];
}

public class OrderDtoCheckout
{
    public int OrderId { get; set; }

    public Customer Customer { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippingDate { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public List<ShoppingItemOrderDTO> Items { get; set; } = [];
}