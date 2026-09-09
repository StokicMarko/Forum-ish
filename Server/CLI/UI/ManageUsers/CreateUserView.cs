using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView(IUserRepository userRepository)
{
    public void newUser()
    {
        Console.WriteLine("Enter username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Enter password: ");
        string password = Console.ReadLine();
        
        userRepository.AddAsync(new User(username, password));
        
        Console.WriteLine("User created successfully");
    }
}