using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView(IPostRepository postRepository)
{
    public void newPost()
    {
        Console.WriteLine("Please enter a name for the new post:");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter a description for the new post:");
        string description = Console.ReadLine();

        postRepository.AddAsync(new Post(name, description, 0));
        
        Console.WriteLine("Post created");
    }
}