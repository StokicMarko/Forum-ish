using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> _users = [];
    public UserInMemoryRepository()
    {
        _ = AddAsync(new User("BeastMaestro", "1234")).Result;
        _ = AddAsync(new User("BeanChaos", "4321")).Result;
        _ = AddAsync(new User("SUperProgrammer_87", "1243")).Result;
        _ = AddAsync(new User("alhe", "2143")).Result;
    }
    public Task<User> AddAsync(User user)
    {
        user.Id = _users.Any()
            ? _users.Max(p => p.Id) + 1
            : 1;
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingPost = _users.SingleOrDefault(u => u.Id == user.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }

        _users.Remove(existingPost);
        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = _users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        _users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? userToGet = _users.SingleOrDefault(u => u.Id == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetMany()
    {
        return _users.AsQueryable();
    }
}