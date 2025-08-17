using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using ServiceDeskPlus.SDK;
using Xunit;

namespace ServiceDeskPlus.Tests;

public class RequestTests
{
    [Fact]
    public async Task GetAsync_ReturnsRequest_On200()
    {
        // Arrange
        var json = "{" +
                   "\"request\": { \"id\": \"1\" }," +
                   "\"response_status\": { \"status_code\": 2000, \"status\": \"success\" }" +
                   "}";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://example.test/api/v3/requests/1")
                .Respond("application/json", json);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://example.test/api/v3/");

        var requestApi = new Request(httpClient);

        // Act
        var result = await requestApi.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Request);
        Assert.Equal("1", result.Request!.Id);
        Assert.NotNull(result.ResponseStatus);
        Assert.Equal(2000, result.ResponseStatus!.StatusCode);
    }

    [Fact]
    public async Task GetAsync_ThrowsSdpApiException_On404_WithApiStatus()
    {
        // Arrange
        var errorJson = "{" +
                        "\"response_status\": {" +
                        "\"status_code\": 4000, " +
                        "\"messages\": [{ \"status_code\": 4007, \"type\": \"failed\", \"message\": \"Invalid URL\" }]," +
                        "\"status\": \"failed\" }" +
                        "}";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://example.test/api/v3/requests/999")
                .Respond(_ => new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "Not Found",
                    Content = new StringContent(errorJson, Encoding.UTF8, "application/json")
                });
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://example.test/api/v3/");

        var requestApi = new Request(httpClient);

        // Act + Assert
        var ex = await Assert.ThrowsAsync<SdpApiException>(() => requestApi.GetAsync(999));
        Assert.Equal(HttpStatusCode.NotFound, ex.HttpStatusCode);
        Assert.Equal(4000, ex.ApiStatusCode);
        Assert.NotNull(ex.Messages);
        Assert.Single(ex.Messages!);
        Assert.Equal(4007, ex.Messages![0].StatusCode);
        Assert.Equal("Invalid URL", ex.Messages![0].Message);
    }

    [Fact]
    public async Task ListAsync_ReturnsRequests_On200()
    {
        // Arrange
        var json = "{" +
                   "\"response_status\": [{ \"status_code\": 2000, \"status\": \"success\" }]," +
                   "\"list_info\": { \"has_more_rows\": false, \"start_index\": 1, \"row_count\": 2 }," +
                   "\"requests\": [ { \"id\": \"1\" }, { \"id\": \"2\" } ]" +
                   "}";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://example.test/api/v3/requests")
                .Respond("application/json", json);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://example.test/api/v3/");

        var requestApi = new Request(httpClient);

        // Act
        var result = await requestApi.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ResponseStatus);
        Assert.NotEmpty(result.ResponseStatus!);
        Assert.Equal(2000, result.ResponseStatus![0].StatusCode);
        Assert.NotNull(result.Requests);
        Assert.Equal(2, result.Requests!.Count);
        Assert.Equal("1", result.Requests![0].Id);
    }
}
