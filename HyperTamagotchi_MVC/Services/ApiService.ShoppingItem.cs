using HyperTamagotchi_MVC.Models;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    public async Task<IEnumerable<ShoppingItem>> GetAllShoppingItemsAsync()
    {
        var response = await _client.GetAsync("api/ShoppingItem");
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shoppingItems = JsonConvert.DeserializeObject<IEnumerable<ShoppingItem>>(jsonResponse);
        return shoppingItems!;
    }

    public async Task<ShoppingItem> GetShoppingItemByIdAsync(int? id)
    {
        var response = await _client.GetAsync($"api/ShoppingItem/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shoppingItem = JsonConvert.DeserializeObject<ShoppingItem>(jsonResponse);

        return shoppingItem!;
    }
}
