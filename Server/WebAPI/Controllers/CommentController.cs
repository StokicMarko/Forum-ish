using System.Collections;
using ApiContracts.Comment;
using ApiContracts.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(
    ICommentRepository repository, 
    IPostRepository postRepository, 
    IUserRepository userRepository) : Controller
{
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDTO>> GetSingle(int id)
    {
        Comment comment = await repository.GetSingleAsync(id);
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
        IEnumerable comments = repository.GetMany()
            .Select(c => new CommentDTO{Id = c.Id, Body = c.Body, WroteBy = c.WroteBy, PostId = c.PostId});
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO request)
    {
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
        Comment comment = await repository.GetSingleAsync(id);
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
        Comment comment = await repository.GetSingleAsync(id);
        if (comment == null)
            return NotFound("Comment not found");
        await repository.DeleteAsync(id);
        return Ok();
    }
}