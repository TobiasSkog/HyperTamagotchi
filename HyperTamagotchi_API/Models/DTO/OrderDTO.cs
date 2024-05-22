namespace HyperTamagotchi.Common.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public List<ShoppingItemOrderDTO> Items { get; set; } = new();
    }
}
