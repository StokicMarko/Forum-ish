using System.Text.Json;

namespace BlazorApp.Services;

internal static class HttpResponseExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task ThrowIfNotSuccessAsync(this HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }

    public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage httpResponse)
    {
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<T>(response, JsonOptions)!;
    }
}
