using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class ShawermaApiResponse<T> 
{
    [JsonPropertyName("code")]
    public long Code { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("result")]
    public T Result { get; set; }
}