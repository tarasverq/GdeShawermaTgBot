using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Api.Dtos;

public class RestaurantExtended : Restaurant
{
    [JsonPropertyName("workTime")]
    public string WorkTime { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("site")]
    public string Site { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("phoneNumber")]
    public object PhoneNumber { get; set; }

    [JsonPropertyName("hasVisa")]
    public bool HasVisa { get; set; }

    [JsonPropertyName("hasFalafel")]
    public bool HasFalafel { get; set; }

    [JsonPropertyName("hasBeer")]
    public bool HasBeer { get; set; }

    [JsonPropertyName("hasHygiene")]
    public bool HasHygiene { get; set; }

    [JsonPropertyName("hasOnCoal")]
    public bool HasOnCoal { get; set; }

    [JsonPropertyName("hasWC")]
    public bool HasWc { get; set; }

    [JsonPropertyName("hasDelivery")]
    public bool HasDelivery { get; set; }

    [JsonPropertyName("checkinCount")]
    public long? CheckinCount { get; set; }

    [JsonPropertyName("tipsPhoneNumber")]
    public object TipsPhoneNumber { get; set; }

    [JsonPropertyName("tipsUserName")]
    public object TipsUserName { get; set; }

    [JsonPropertyName("supplierId")]
    public object SupplierId { get; set; }

    [JsonPropertyName("menuId")]
    public object MenuId { get; set; }

    [JsonPropertyName("commentsCount")]
    public long? CommentsCount { get; set; }

    [JsonPropertyName("checkins")]
    public List<Checkin> Checkins { get; set; }

    [JsonPropertyName("checkinByDevice")]
    public object CheckinByDevice { get; set; }

    [JsonPropertyName("discountsNumber")]
    public long? DiscountsNumber { get; set; }

    [JsonPropertyName("lastDiscount")]
    public object LastDiscount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}