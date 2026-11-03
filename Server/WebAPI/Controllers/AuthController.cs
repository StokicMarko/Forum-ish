using ApiContracts;
using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginUserDTO  loginUserDTO)
    {
        User userToCheck = await userRepository.GetUserByUsernameAsync(loginUserDTO.Username);
        
        if (userToCheck is null)
            throw new NotFoundException($"User with username '{loginUserDTO.Username}' not found");

        if (userToCheck.Password != loginUserDTO.Password)
            throw new WrongDataException("Password does not correspond");

        return Ok(new UserDTO
        {
            Id = userToCheck.Id,
            Username = userToCheck.Username,
        });
    }
}