using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDeskPlus.Tests.TestUtils;

internal class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _responder;

    public FakeHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responder)
    {
        _responder = responder;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responder(request));
    }

    public static HttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> responder, string? baseAddress = null)
    {
        var handler = new FakeHttpMessageHandler(responder);
        var client = new HttpClient(handler);
        if (!string.IsNullOrEmpty(baseAddress))
        {
            client.BaseAddress = new Uri(baseAddress);
        }
        return client;
    }
}
