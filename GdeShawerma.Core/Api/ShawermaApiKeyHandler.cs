using System.Text;

namespace GdeShawerma.Core.Api;

[UsedImplicitly]
internal class ShawermaApiKeyHandler : IRequestHandler<ShawermaApiKeyRequest, string>
{
    private readonly HttpClient _httpClient;
    private static string? _regId;


    public ShawermaApiKeyHandler(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Shawerma");
    }

    public async Task<string> Handle(ShawermaApiKeyRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_regId))
            return _regId;
        
        string guid = Guid.NewGuid().ToString("N");

        HttpResponseMessage response = await _httpClient.PostAsync("api/v1/Devices/saveGcmToken",
            new StringContent($"regId={guid}&oldRegId=", Encoding.UTF8, "application/x-www-form-urlencoded"),
            cancellationToken);

        response.EnsureSuccessStatusCode();

        //string content = await response.Content.ReadAsStringAsync(cancellationToken);
        _regId = guid;
        return guid;
    }
}