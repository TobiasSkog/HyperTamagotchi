namespace HyperTamagotchi_MVC.Models.DTO;

public class CustomerDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int AddressId { get; set; }
    public int ShoppingCartId { get; set; }
}