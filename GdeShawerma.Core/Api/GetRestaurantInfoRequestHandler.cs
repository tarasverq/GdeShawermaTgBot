using System.Globalization;
using System.Text.Json;
using GdeShawerma.Core.Api.Dtos;
using GdeShawerma.Core.Helpers;
using Microsoft.AspNetCore.WebUtilities;

namespace GdeShawerma.Core.Api;

[UsedImplicitly]
internal class GetRestaurantInfoRequestHandler : IRequestHandler<GetRestaurantInfoRequest, RestaurantExtended>
{
    private readonly IMediator _mediator;
    private readonly HttpClient _httpClient;

    public GetRestaurantInfoRequestHandler(IHttpClientFactory clientFactory, IMediator mediator)
    {
        _mediator = mediator;
        _httpClient = clientFactory.CreateClient("Shawerma");
    }

    public async Task<RestaurantExtended> Handle(GetRestaurantInfoRequest request, CancellationToken cancellationToken)
    {
        string regKey = await _mediator.Send(new ShawermaApiKeyRequest(), cancellationToken);

        Dictionary<string, string> query = new Dictionary<string, string>
        {
            ["regId"] = regKey,
            ["placeId"] = request.PlaceId.ToString(CultureInfo.InvariantCulture),
            ["limitComments"] = request.LimitComments.ToString(CultureInfo.InvariantCulture).ToLower(),
        };
        string url = QueryHelpers.AddQueryString("api/v1/Places/getInfo", query);

        HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        ShawermaApiResponse<RestaurantExtended>? data =
            JsonSerializer.Deserialize<ShawermaApiResponse<RestaurantExtended>>(content,
                new JsonSerializerOptions()
                {
                    Converters = { new NullToFalseConverter() }
                });
        if (data.Code != 200)
        {
            //todo throw
        }

        return data.Result;
    }
}