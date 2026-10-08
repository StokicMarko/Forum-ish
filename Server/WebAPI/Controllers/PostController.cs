using ApiContracts.Post;
using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostRepository repository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDTO>> GetSingle(int id)
    {
        Post post = await repository.GetSingleAsync(id);
        return Ok(new PostDTO()
        {
            Id = post.Id,
            UserId = post.UserId,
            Body = post.Body,
            Title = post.Title
        });
    }

    [HttpGet]
    public ActionResult<IEnumerable<PostDTO>> GetMany()
    {
        IEnumerable<PostDTO> posts = repository.GetMany()
            .Select(post => new PostDTO() { Id = post.Id,  UserId = post.UserId, Body = post.Body, Title = post.Title });
        return Ok(posts);
    }
    
    [HttpPost]
    public async Task<ActionResult<PostDTO>> Create([FromBody] CreatePostDTO request)
    {

        VerifyIfUserExist(request.UserId);
        
        Console.WriteLine(request.UserId);
        Post created = await repository.AddAsync(new Post(0, request.Body,  request.Title, request.UserId));
        return Ok(new PostDTO
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDTO>> UpdatePost(int id, [FromBody] UpdatePostDTO request)
    {
        
        Post post = await repository.GetSingleAsync(id);
        post.Title = request.Title;
        post.Body = request.Body;

        await repository.UpdateAsync(post);

        return Ok(new PostDTO
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId
        });
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Post post = await repository.GetSingleAsync(id);
        if (post == null)
            return NotFound("Post not found");
        await repository.DeleteAsync(id);
        return Ok();
    }
    
    private void VerifyIfUserExist(int requestUserId)
    {
        bool exists = repository.GetMany()
            .Any(u => u.Id == requestUserId);

        if (!exists)
        {
            throw new NotFoundException($"Author of the post does not exist");
        }
    }
}