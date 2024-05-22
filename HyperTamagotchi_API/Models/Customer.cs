using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_API.Models;

public class Customer : IdentityUser
{
    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "First must be between 1 and 30 characters")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 30 characters")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    public int AddressId { get; set; }
    public Address Address { get; set; }

    public string? RefreshToken { get; set; }
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }

    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<Tamagotchi> Tamagotchis { get; set; } = [];
}