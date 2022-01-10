using GdeShawerma.Core.Api.Dtos;

namespace GdeShawerma.Core.Api;

internal class GetRestaurantInfoRequest : IRequest<RestaurantExtended>
{
    public long PlaceId { get; }
    public bool LimitComments { get; }

    public GetRestaurantInfoRequest(long placeId, bool limitComments = true)
    {
        PlaceId = placeId;
        LimitComments = limitComments;
    }
}