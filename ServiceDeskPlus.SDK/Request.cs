using System.Text.Json;
using ServiceDeskPlus.SDK.Models;

namespace ServiceDeskPlus.SDK;

public class Request
{
    private readonly HttpClient _httpClient;

    public Request(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RequestGetResponse> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var endpoint = $"requests/{id}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cancellationToken);
        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        // Try to extract API response_status regardless of HTTP code
        var apiStatus = TryExtractResponseStatus(json);

        if (!response.IsSuccessStatusCode)
        {
            throw SdpApiException.From(response.StatusCode, response.ReasonPhrase, apiStatus, json, endpoint);
        }

        var result = JsonSerializer.Deserialize<RequestGetResponse>(json);
        if (result is null)
            throw new JsonException("Failed to deserialize GetAsync response to RequestGetResponse.");

        // Validate API status for success
        if (result.ResponseStatus is { StatusCode: not 2000 })
        {
            throw SdpApiException.From(response.StatusCode, response.ReasonPhrase, result.ResponseStatus, json, endpoint);
        }

        return result;
    }

    public async Task<RequestListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        var endpoint = "requests";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cancellationToken);
        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        // Try to extract API response_status regardless of HTTP code
        var apiStatus = TryExtractResponseStatus(json);

        if (!response.IsSuccessStatusCode)
        {
            throw SdpApiException.From(response.StatusCode, response.ReasonPhrase, apiStatus, json, endpoint);
        }

        var result = JsonSerializer.Deserialize<RequestListResponse>(json);
        if (result is null)
            throw new JsonException("Failed to deserialize ListAsync response to RequestListResponse.");

        // Validate API status for success (take first if array present)
        var statusToCheck = result.ResponseStatus != null && result.ResponseStatus.Count > 0
            ? result.ResponseStatus[0]
            : null;
        if (statusToCheck is { StatusCode: not 2000 })
        {
            throw SdpApiException.From(response.StatusCode, response.ReasonPhrase, statusToCheck, json, endpoint);
        }

        return result;
    }

    private static ResponseStatus? TryExtractResponseStatus(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return null;
            if (!root.TryGetProperty("response_status", out var rs)) return null;
            if (rs.ValueKind == JsonValueKind.Object)
            {
                return rs.Deserialize<ResponseStatus>();
            }
            if (rs.ValueKind == JsonValueKind.Array)
            {
                var arr = rs.Deserialize<List<ResponseStatus>>();
                return arr != null && arr.Count > 0 ? arr[0] : null;
            }
        }
        catch
        {
            // ignore parsing errors; we'll fall back to HTTP details
        }
        return null;
    }
}