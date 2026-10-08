namespace Entities;

public class Comment(string body, int wroteBy, int postId)
{
    public int Id { get; set; }
    public string Body { get; set; } = body;
    public int WroteBy { get; set; } = wroteBy;
    public int PostId { get; set; } = postId;
}