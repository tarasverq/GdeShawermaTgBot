using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class Restaurant
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("picture")]
    public string Picture { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("ratesCount")]
    public long RatesCount { get; set; }

    [JsonPropertyName("type")]
    public object Type { get; set; }

    [JsonPropertyName("rate")]
    public double Rate { get; set; }

    [JsonPropertyName("visits")]
    public long Visits { get; set; }

    [JsonPropertyName("hasOwner")]
    public bool HasOwner { get; set; }

    [JsonPropertyName("hasOwnerActive")]
    public bool HasOwnerActive { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("isFavorite")]
    public bool IsFavorite { get; set; }

    [JsonPropertyName("reviews")]
    [JsonIgnore]
    public List<object> Reviews { get; set; }
    
    [JsonPropertyName("comments")]
    [JsonIgnore]
    public List<Comment> Comments { get; set; }
}