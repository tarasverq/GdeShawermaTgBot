using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public partial class User
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("photoSmall")]
    public Uri? PhotoSmall { get; set; }

    [JsonPropertyName("registrationDate")]
    public long? RegistrationDate { get; set; }
}