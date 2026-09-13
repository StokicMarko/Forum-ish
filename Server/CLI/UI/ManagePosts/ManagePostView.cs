using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ManagePostsView(
    IPostRepository postRepository,
    ICommentRepository commentRepository,
    IUserRepository userRepository)
{
    public async Task ShowAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n-- Manage Posts --");
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. View posts overview");
            Console.WriteLine("3. View single post");
            Console.WriteLine("4. Update post");
            Console.WriteLine("5. Delete post");
            Console.WriteLine("6. View posts by user");
            Console.WriteLine("7. Add comment to post");
            Console.WriteLine("0. Back");
            Console.Write("Choice: ");

            switch (Console.ReadLine())
            {
                case "1": await CreatePostAsync(); break;
                case "2": await ViewPostsOverviewAsync(); break;
                case "3": await ViewSinglePostAsync(); break;
                case "4": await UpdatePostAsync(); break;
                case "5": await DeletePostAsync(); break;
                case "6": await PostsByUserAsync(); break;
                case "7": await AddCommentAsync(); break;
                case "0": back = true; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private async Task CreatePostAsync()
    {
        Console.Write("User Id: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var user = await userRepository.GetSingleAsync(userId);
        if (user is null)
        {
            Console.WriteLine("No user with that Id exists.");
            return;
        }

        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Body: ");
        string body = Console.ReadLine() ?? "";

        var post = new Post(title, body, userId);
        var created = await postRepository.AddAsync(post);
        Console.WriteLine($"Created post with Id {created.Id}");
    }

    private async Task ViewPostsOverviewAsync()
    {
        var posts = postRepository.GetMany();
        if (!posts.Any())
        {
            Console.WriteLine("No posts yet.");
            return;
        }

        foreach (Post p in posts)
            Console.WriteLine($"{p.Id}: {p.Title}");
    }

    private async Task ViewSinglePostAsync()
    {
        Console.Write("Post Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        Post post = await postRepository.GetSingleAsync(id);
        if (post is null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.WriteLine($"\nTitle: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");

        var comments =  commentRepository.GetMany();
        var postComments = comments.Where(c => c.WroteBy == id).ToList();

        if (!postComments.Any())
        {
            Console.WriteLine("No comments yet.");
            return;
        }

        Console.WriteLine("Comments:");
        foreach (Comment c in postComments)
            Console.WriteLine($"  - {c.Body} (User {c.WroteBy})");
    }

    private async Task UpdatePostAsync()
    {
        Console.Write("Post Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var post = await postRepository.GetSingleAsync(id);
        if (post is null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.Write($"New title (blank to keep '{post.Title}'): ");
        string title = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(title)) post.Title = title;

        Console.Write("New body (blank to keep current): ");
        string body = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(body)) post.Body = body;

        await postRepository.UpdateAsync(post);
        Console.WriteLine("Post updated.");
    }

    private async Task DeletePostAsync()
    {
        Console.Write("Post Id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var post = await postRepository.GetSingleAsync(id);
        if (post is null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        await postRepository.DeleteAsync(id);
        Console.WriteLine("Post deleted.");
    }

    private async Task PostsByUserAsync()
    {
        Console.Write("User Id: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var posts = postRepository.GetMany();
        List<Post> userPosts = posts.Where(p => p.UserId == userId).ToList();

        if (!userPosts.Any())
        {
            Console.WriteLine("This user has no posts.");
            return;
        }

        foreach (Post p in userPosts)
            Console.WriteLine($"{p.Id}: {p.Title}");
    }

    private async Task AddCommentAsync()
    {
        Console.Write("Post Id: ");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var post = await postRepository.GetSingleAsync(postId);
        if (post is null)
        {
            Console.WriteLine("No post with that Id exists.");
            return;
        }

        Console.Write("User Id: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        var user = await userRepository.GetSingleAsync(userId);
        if (user is null)
        {
            Console.WriteLine("No user with that Id exists.");
            return;
        }

        Console.Write("Comment: ");
        string body = Console.ReadLine() ?? "";

        var comment = new Comment(body, postId, userId);
        var created = await commentRepository.AddAsync(comment);
        Console.WriteLine($"Added comment with Id {created.Id}");
    }
}