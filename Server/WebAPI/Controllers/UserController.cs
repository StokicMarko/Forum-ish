using ApiContracts;
using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository repository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetSingle(int id)
    {
        User user = await repository.GetSingleAsync(id);
        return Ok(new UserDTO { Id = user.Id, Username = user.Username });
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDTO>> GetMany()
    {
        IEnumerable<UserDTO> users = repository.GetMany()
            .Select(u => new UserDTO { Id = u.Id, Username = u.Username });
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO request)
    {
        VerifyUserNameIsAvailable(request.Username);

        User created = await repository.AddAsync(new User(request.Username, request.Password));
        return Ok(new UserDTO { Id = created.Id, Username = created.Username });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> Update([FromBody] UpdateUserDTO newUser, int id)
    {
        User user = await repository.GetSingleAsync(id);
        if (user == null)
            return NotFound("User not found");
        
        await repository.UpdateAsync(new User(newUser.Username, newUser.Password) { Id = id });
        return Ok(new UserDTO{ Id = id, Username = newUser.Username });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        User user = await repository.GetSingleAsync(id);
        if (user == null)
            return NotFound("User not found");
        await repository.DeleteAsync(id);
        return Ok();
    }

    private void VerifyUserNameIsAvailable(string userName)
    {
        bool exists = repository.GetMany()
            .Any(u => u.Username.ToLower() == userName.ToLower());

        if (exists)
        {
            throw new ConflictException($"Username '{userName}' is already taken");
        }
    }
}