using ApiContracts.Post;

namespace BlazorApp.Services;

public class HttpPostService(HttpClient client) : IPostService
{
    public async Task<PostDTO> AddPostAsync(CreatePostDTO request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("api/Post", request);
        return await httpResponse.ReadContentAsync<PostDTO>();
    }

    public async Task UpdatePostAsync(int id, UpdatePostDTO request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"api/Post/{id}", request);
        await httpResponse.ThrowIfNotSuccessAsync();
    }

    public async Task<PostDTO> GetPostAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"api/Post/{id}");
        return await httpResponse.ReadContentAsync<PostDTO>();
    }

    public async Task<PostDTO[]> GetPostsAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("api/Post");
        return await httpResponse.ReadContentAsync<PostDTO[]>();
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"api/Post/{id}");
        await httpResponse.ThrowIfNotSuccessAsync();
    }
}
