using System.Text.Json.Serialization;

namespace HmInput.Models;

internal class HmResponse
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("itemsShown")]
    public int? ItemsShown { get; set; }

    [JsonPropertyName("products")]
    public List<HmProduct>? Products { get; set; }
}
