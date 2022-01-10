using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class Checkin
{
    [JsonPropertyName("lastDate")]
    public long LastDate { get; set; }

    [JsonPropertyName("count")]
    public long Count { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("user")]
    public User User { get; set; }
}