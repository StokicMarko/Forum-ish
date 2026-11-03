using ApiContracts.Post;
using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostRepository repository, IUserRepository userRepository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDTO>> GetSingle(int id)
    {
        Post post = await GetExistingPostAsync(id);
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
        if (await userRepository.GetSingleAsync(request.UserId) is null)
            throw new NotFoundException($"User with id '{request.UserId}' not found");

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
    public async Task<ActionResult<PostDTO>> Update(int id, [FromBody] UpdatePostDTO request)
    {
        Post post = await GetExistingPostAsync(id);
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
        await GetExistingPostAsync(id);
        await repository.DeleteAsync(id);
        return Ok();
    }

    private async Task<Post> GetExistingPostAsync(int id)
    {
        return await repository.GetSingleAsync(id)
               ?? throw new NotFoundException($"Post with id '{id}' not found");
    }
}
