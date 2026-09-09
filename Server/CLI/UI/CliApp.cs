using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp 
    (IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
{
    public async Task StartAsync()
    {
        await StartMainMenu();
        
        Console.WriteLine("Exiting the programm...");
    }

    private async Task StartMainMenu()
    {
        while (true)
        {
            printMenu();
            
            string? userInput = Console.ReadLine();

            switch (userInput) 
            {
                case "1":
                    CreateUserView userView = new CreateUserView(userRepository);
                    userView.newUser();
                    break;
                case "2":
                    CreatePostView postView = new CreatePostView(postRepository);
                    postView.newPost();
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine($"Invalid input: {userInput}");
                    break;
            }
        }
    }

    private void printMenu()
    {
        string menu = """
                      1 - Create a new User
                      2 - Create a new Post
                      3 - Add a new Comment
                      4 - View posts overview
                      5 - View specific post
                      0 - Exit
                      """;
        Console.WriteLine(menu);
    }
}