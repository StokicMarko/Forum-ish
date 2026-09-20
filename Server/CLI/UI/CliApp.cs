using RepositoryContracts;

namespace CLI.UI;

public class CliApp(IUserRepository userRepo, IPostRepository postRepo, ICommentRepository commentRepo)
{
    private readonly ManageUsersView _manageUsersView = new(userRepo);
    private readonly ManagePostsView _managePostsView = new(postRepo, commentRepo, userRepo);

    public async Task StartAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== Forum-ish CLI ===");
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Manage Posts");
            Console.WriteLine("0. Exit");
            Console.Write("Choice: ");

            switch (Console.ReadLine())
            {
                case "1": await _manageUsersView.ShowAsync(); break;
                case "2": await _managePostsView.ShowAsync(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }
}