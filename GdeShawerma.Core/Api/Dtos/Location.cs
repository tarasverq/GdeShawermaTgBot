using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}