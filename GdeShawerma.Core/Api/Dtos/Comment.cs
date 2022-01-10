using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class Comment
{
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("banned")]
    public bool? Banned { get; set; }

    [JsonPropertyName("answer")]
    public object Answer { get; set; }

    [JsonPropertyName("date")]
    public long? Date { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("user")]
    public User User { get; set; }
}