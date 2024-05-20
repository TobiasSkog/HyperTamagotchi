using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_MVC.Models.DTO;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password does not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "First must be between 1 and 30 characters")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 30 characters")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Street address must be between 1 and 50 characters")]
    [DisplayName("Street Address")]
    public string StreetAddress { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "City must be between 1 and 50 characters")]
    [DisplayName("City")]
    public string City { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Zip code must be between 1 and 10 characters")]
    [DisplayName("Zip Code")]
    public string ZipCode { get; set; }
}
