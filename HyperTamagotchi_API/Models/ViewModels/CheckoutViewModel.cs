using HyperTamagotchi_API.Models.DTO;

namespace HyperTamagotchi_API.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public string CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
