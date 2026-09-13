using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ManageUsersView(IUserRepository userRepository)
{
    public async Task ShowAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n-- Manage Users --");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. List all users");
            Console.WriteLine("3. Update user");
            Console.WriteLine("4. Delete user");
            Console.WriteLine("0. Back");
            Console.Write("Choice: ");

            switch (Console.ReadLine())
            {
                case "1": await CreateUserAsync(); break;
                case "2": await ListUsersAsync(); break;
                case "3": await UpdateUserAsync(); break;
                case "4": await DeleteUserAsync(); break;
                case "0": back = true; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private async Task CreateUserAsync()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";

        var existing = userRepository.GetMany();
        if (existing.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Username already taken.");
            return;
        }

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        User user = new User(username, password);
        User created = await userRepository.AddAsync(user);
        Console.WriteLine($"Created user with Id {created.Id}");
    }

    private async Task ListUsersAsync()
    {
        var users = userRepository.GetMany();
        foreach (User u in users)
            Console.WriteLine($"{u.Id}: {u.Username}");
    }

    private async Task UpdateUserAsync()
    {
        Console.Write("User Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        User user = await userRepository.GetSingleAsync(id);
        if (user is null) { Console.WriteLine("Not found."); return; }

        Console.Write($"New username (blank to keep '{user.Username}'): ");
        string input = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(input)) user.Username = input;

        await userRepository.UpdateAsync(user);
        Console.WriteLine("Updated.");
    }

    private async Task DeleteUserAsync()
    {
        Console.Write("User Id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        await userRepository.DeleteAsync(id);
        Console.WriteLine("Deleted.");
    }
}