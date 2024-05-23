namespace HyperTamagotchi_MVC.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public string OrderItemName { get; set; }
    public byte? Quantity { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; } = 1.00f;
}