namespace Entities;

public class Post(string body, string title, int id)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } =  title;
    public string Body { get; set; } = body;
    public int UserId { get; set; }
}