using ApiContracts.Comment;

namespace BlazorApp.Services;

public class HttpCommentService(HttpClient client) : ICommentService
{
    public async Task<CommentDTO> AddCommentAsync(CreateCommentDTO request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("api/Comment", request);
        return await httpResponse.ReadContentAsync<CommentDTO>();
    }

    public async Task UpdateCommentAsync(int id, UpdateCommentDTO request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"api/Comment/{id}", request);
        await httpResponse.ThrowIfNotSuccessAsync();
    }

    public async Task<CommentDTO> GetCommentAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"api/Comment/{id}");
        return await httpResponse.ReadContentAsync<CommentDTO>();
    }

    public async Task<CommentDTO[]> GetCommentsAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("api/Comment");
        return await httpResponse.ReadContentAsync<CommentDTO[]>();
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"api/Comment/{id}");
        await httpResponse.ThrowIfNotSuccessAsync();
    }
}
