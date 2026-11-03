using ApiContracts.Comment;
using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(
    ICommentRepository repository,
    IPostRepository postRepository,
    IUserRepository userRepository) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDTO>> GetSingle(int id)
    {
        Comment comment = await GetExistingCommentAsync(id);
        return Ok(new CommentDTO
        {
            Id = comment.Id,
            Body = comment.Body,
            WroteBy = comment.WroteBy,
            PostId = comment.PostId
        });
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommentDTO>> GetMany()
    {
        IEnumerable<CommentDTO> comments = repository.GetMany()
            .Select(c => new CommentDTO{Id = c.Id, Body = c.Body, WroteBy = c.WroteBy, PostId = c.PostId});
        return Ok(comments);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO request)
    {
        if (await postRepository.GetSingleAsync(request.PostId) is null)
            throw new NotFoundException($"Post with id '{request.PostId}' not found");
        if (await userRepository.GetSingleAsync(request.WroteBy) is null)
            throw new NotFoundException($"User with id '{request.WroteBy}' not found");

        Comment comment = new Comment(request.Body, request.WroteBy, request.PostId);
        Comment created = await repository.AddAsync(comment);

        return Ok(new CommentDTO
        {
            Id = created.Id,
            Body = created.Body,
            WroteBy = created.WroteBy,
            PostId = created.PostId
        });
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDTO>> Update(int id, [FromBody] UpdateCommentDTO request)
    {
        Comment comment = await GetExistingCommentAsync(id);
        comment.Body = request.Body;

        await repository.UpdateAsync(comment);

        return Ok(new CommentDTO()
        {
            Id = comment.Id,
            Body = comment.Body,
            WroteBy = comment.WroteBy,
            PostId = comment.PostId
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await GetExistingCommentAsync(id);
        await repository.DeleteAsync(id);
        return Ok();
    }

    private async Task<Comment> GetExistingCommentAsync(int id)
    {
        return await repository.GetSingleAsync(id)
               ?? throw new NotFoundException($"Comment with id '{id}' not found");
    }
}
