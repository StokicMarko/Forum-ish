using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly ManageUsersView manageUsersView;
    private readonly ManagePostsView managePostsView;

    public CliApp(IUserRepository userRepo, IPostRepository postRepo, ICommentRepository commentRepo)
    {
        manageUsersView = new ManageUsersView(userRepo);
        managePostsView = new ManagePostsView(postRepo, commentRepo, userRepo);
    }

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
                case "1": await manageUsersView.ShowAsync(); break;
                case "2": await managePostsView.ShowAsync(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }
}