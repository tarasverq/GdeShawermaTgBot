namespace GdeShawerma.Core.Handlers;

internal class SendRestaurantExtendedInfoRequest : IRequest
{
    public long ChatId { get; }
    public long RestaurantId { get; }

    public SendRestaurantExtendedInfoRequest(long chatId, long restaurantId)
    {
        ChatId = chatId;
        RestaurantId = restaurantId;
    }
}