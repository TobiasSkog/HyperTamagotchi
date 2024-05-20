using HyperTamagotchi_MVC.Models;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    private async Task<ShoppingCart> GetShoppingCartById()
    {

        var shoppingCartIdClaim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimShoppingCart.ClaimName);

        //Needs to be better
        if (shoppingCartIdClaim == null || !int.TryParse(shoppingCartIdClaim.Value, out var shoppingCartId))
        {
            return new ShoppingCart();
        }

        var response = await _client.GetAsync($"api/ShoppingCarts/{shoppingCartId}");

        //Needs to be better
        if (!response.IsSuccessStatusCode)
        {
            return new ShoppingCart();
        }

        var jsonShoppingCart = await response.Content.ReadAsStringAsync();

        var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(jsonShoppingCart);

        return shoppingCart!;
    }


}
