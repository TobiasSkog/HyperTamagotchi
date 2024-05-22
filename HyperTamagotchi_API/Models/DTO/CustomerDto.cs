namespace HyperTamagotchi_API.Models.DTO;

public class CustomerDto(string id, string firstName, string lastName, string email, int addressId, int shoppingCartId)
{
    public string Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string FullName { get; set; } = $"{firstName} {lastName}";
    public string Email { get; set; } = email;
    public int AddressId { get; set; } = addressId;
    public int ShoppingCartId { get; set; } = shoppingCartId;
}
