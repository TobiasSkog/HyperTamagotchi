using HyperTamagotchi_MVC.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HyperTamagotchi_MVC.JsonConverters;

public class EditItemResponseConverter : JsonConverter<EditItemResponse>
{
    public override EditItemResponse? ReadJson(JsonReader reader, Type objectType, EditItemResponse? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        var type = (ItemType)jsonObject["type"].Value<int>();
        var data = jsonObject["data"].ToString();
        return new EditItemResponse
        {
            Type = type,
            Data = data
        };
    }

    public override void WriteJson(JsonWriter writer, EditItemResponse? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("type");
        writer.WriteValue((int)value.Type);
        writer.WritePropertyName("data");
        writer.WriteRawValue(value.Data);
        writer.WriteEndObject();
    }
}
