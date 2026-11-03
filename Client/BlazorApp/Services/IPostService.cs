using ApiContracts.Post;
namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(CreatePostDTO request);
    public Task UpdatePostAsync(int id, UpdatePostDTO request);
    public Task<PostDTO> GetPostAsync(int id);
    public Task<PostDTO[]> GetPostsAsync();
    public Task DeletePostAsync(int id);
}
