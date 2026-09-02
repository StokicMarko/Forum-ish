using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> _users;
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