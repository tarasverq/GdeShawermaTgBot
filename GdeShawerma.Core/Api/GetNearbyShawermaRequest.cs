using GdeShawerma.Core.Api.Dtos;

namespace GdeShawerma.Core.Api;

internal class GetNearbyShawermaRequest : IRequest<List<Restaurant>>
{
    public double Longitude { get; }
    public double Latitude { get; }

    public GetNearbyShawermaRequest(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}