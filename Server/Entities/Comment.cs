namespace Entities;

public class Comment(string body, int id, int wroteBy)
{
    public int Id { get; set; } = id;
    public string Body { get; set; } = body;
    public int WroteBy { get; set; } = wroteBy;
}