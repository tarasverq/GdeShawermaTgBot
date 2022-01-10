using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using GdeShawerma.Core.Api.Dtos;

namespace GdeShawerma.Core.Api;

[UsedImplicitly]
internal class GetNearbyShawermaRequestHandler : IRequestHandler<GetNearbyShawermaRequest, List<Restaurant>?>
{
    private readonly IMediator _mediator;
    private readonly HttpClient _httpClient;

    public GetNearbyShawermaRequestHandler(IHttpClientFactory clientFactory, IMediator mediator)
    {
        _mediator = mediator;
        _httpClient = clientFactory.CreateClient("Shawerma");
    }

    public async Task<List<Restaurant>?> Handle(GetNearbyShawermaRequest request,
        CancellationToken cancellationToken)
    {
        string regKey = await _mediator.Send(new ShawermaApiKeyRequest(), cancellationToken);

        Dictionary<string, string> query = new Dictionary<string, string>
        {
            ["regId"] = regKey,
            ["lat"] = request.Latitude.ToString(CultureInfo.InvariantCulture),
            ["lng"] = request.Longitude.ToString(CultureInfo.InvariantCulture),
        };
        string url = QueryHelpers.AddQueryString("api/v1/Places/getNearby", query);

        HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        ShawermaApiResponse<List<Restaurant>>? data = 
            JsonSerializer.Deserialize<ShawermaApiResponse<List<Restaurant>>>(content);
        if (data.Code != 200)
        {
           //todo throw
        }
        return data.Result;
    }
}