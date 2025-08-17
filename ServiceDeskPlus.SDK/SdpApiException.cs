using System.Net;
using ServiceDeskPlus.SDK.Models;

namespace ServiceDeskPlus.SDK;

public class SdpApiException : Exception
{
    public HttpStatusCode? HttpStatusCode { get; }
    public string? ReasonPhrase { get; }
    public int? ApiStatusCode { get; }
    public string? ApiStatus { get; }
    public List<ResponseMessage>? Messages { get; }
    public string? RawBody { get; }
    public string? Endpoint { get; }

    public SdpApiException(
        string message,
        HttpStatusCode? httpStatusCode = null,
        string? reasonPhrase = null,
        int? apiStatusCode = null,
        string? apiStatus = null,
        List<ResponseMessage>? messages = null,
        string? rawBody = null,
        string? endpoint = null,
        Exception? innerException = null) : base(message, innerException)
    {
        HttpStatusCode = httpStatusCode;
        ReasonPhrase = reasonPhrase;
        ApiStatusCode = apiStatusCode;
        ApiStatus = apiStatus;
        Messages = messages;
        RawBody = rawBody;
        Endpoint = endpoint;
    }

    public static SdpApiException From(HttpStatusCode? httpStatusCode, string? reasonPhrase, ResponseStatus? responseStatus, string? rawBody, string? endpoint)
    {
        string detail = BuildMessage(httpStatusCode, reasonPhrase, responseStatus);
        return new SdpApiException(
            detail,
            httpStatusCode,
            reasonPhrase,
            responseStatus?.StatusCode,
            responseStatus?.Status,
            responseStatus?.Messages,
            rawBody,
            endpoint);
    }

    private static string BuildMessage(HttpStatusCode? httpStatusCode, string? reasonPhrase, ResponseStatus? responseStatus)
    {
        var parts = new List<string>();
        if (httpStatusCode != null)
        {
            parts.Add($"HTTP {(int)httpStatusCode} {reasonPhrase}");
        }
        if (responseStatus != null)
        {
            parts.Add($"API {responseStatus.StatusCode} {responseStatus.Status}");
            var firstMsg = responseStatus.Messages?.FirstOrDefault();
            if (firstMsg != null)
            {
                parts.Add($"Message: [{firstMsg.StatusCode}] {firstMsg.Type} - {firstMsg.Message}");
            }
        }
        if (parts.Count == 0)
        {
            return "ServiceDesk Plus API error";
        }
        return string.Join(" | ", parts);
    }
}
