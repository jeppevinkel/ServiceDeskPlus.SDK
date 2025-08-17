using System.Net.Http.Headers;

namespace ServiceDeskPlus.SDK;

public class SdpClient : IDisposable
{
    private readonly HttpClient _httpClient;
    public readonly Request Request;

    public SdpClient(string hostAddress, string authToken)
    {
        var handler = new HttpClientHandler() 
        { 
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri($"{hostAddress}/api/v3/"),
        };

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.manageengine.sdp.v3+json"));
        _httpClient.DefaultRequestHeaders.Add("AuthToken", authToken);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ServiceDeskPlus.SDK");

        Request = new Request(_httpClient);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _httpClient.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SdpClient()
    {
        Dispose(false);
    }
}