using HyperTamagotchi_MVC.Models.DTO;

namespace HyperTamagotchi_MVC.Models
{
    public class CheckoutModel
    {
        public string CustomerId { get; set; }
        public List<CheckoutItemDto> Items { get; set; }
    }
}
