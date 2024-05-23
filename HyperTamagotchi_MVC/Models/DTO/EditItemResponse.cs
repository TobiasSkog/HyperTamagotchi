using HyperTamagotchi_MVC.JsonConverters;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Models.DTO;

[JsonConverter(typeof(EditItemResponseConverter))]
public class EditItemResponse
{
    public ItemType Type { get; set; }
    public string Data { get; set; }
}
public enum ItemType
{
    Tamagotchi,
    ShoppingItem
}