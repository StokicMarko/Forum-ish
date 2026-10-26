using ApiContracts;
namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(CreateUserDTO request);
    public Task UpdateUserAsync(int id, UpdateUserDTO request);
    public Task<UserDTO> GetUserAsync(int id);
    public Task<UserDTO[]> GetUsersAsync();
    public Task DeleteUserAsync(int id);
}