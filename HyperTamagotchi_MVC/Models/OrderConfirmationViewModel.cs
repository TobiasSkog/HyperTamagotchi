namespace HyperTamagotchi_MVC.Models
{
    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}
