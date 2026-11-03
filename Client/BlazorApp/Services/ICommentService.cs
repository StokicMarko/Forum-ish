using ApiContracts.Comment;
namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CommentDTO> AddCommentAsync(CreateCommentDTO request);
    public Task UpdateCommentAsync(int id, UpdateCommentDTO request);
    public Task<CommentDTO> GetCommentAsync(int id);
    public Task<CommentDTO[]> GetCommentsAsync();
    public Task DeleteCommentAsync(int id);
}
