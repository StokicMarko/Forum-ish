using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService(HttpClient client) : IUserService
{
    private readonly HttpClient _client = client;

    public async Task<UserDTO> AddUserAsync(CreateUserDTO request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task UpdateUserAsync(int id, UpdateUserDTO request)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO[]> GetUsersAsync()
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("/api/User");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}