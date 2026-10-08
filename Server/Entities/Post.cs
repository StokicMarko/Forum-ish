namespace Entities;

public class Post(int id, string body, string title, int userId)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } =  title;
    public string Body { get; set; } = body;
    public int UserId { get; set; } = userId;
}