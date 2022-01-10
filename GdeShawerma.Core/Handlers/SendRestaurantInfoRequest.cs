using GdeShawerma.Core.Api.Dtos;
using Location = Telegram.Bot.Types.Location;

namespace GdeShawerma.Core.Handlers;

internal class SendRestaurantInfoRequest : IRequest<Message>
{
    public long ChatId { get; }
    public Restaurant Restaurant { get; }
    public Location UserLocation { get; init; }

    public SendRestaurantInfoRequest(long chatId, Restaurant restaurant, Location userLocation = null)
    {
        ChatId = chatId;
        Restaurant = restaurant;
        UserLocation = userLocation;
    }
}