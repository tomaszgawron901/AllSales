using System.Text.Json.Serialization;

namespace HmInput.Models;

internal class HmProduct
{
    [JsonPropertyName("articleCode")]
    public string? ArticleCode { get; set; }
    
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("price")]
    public string? Price { get; set; }

    [JsonPropertyName("redPrice")]
    public string? RedPrice { get; set; }
}
