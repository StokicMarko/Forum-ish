namespace ApiContracts.Comment;

public class CommentDTO
{
    public required int Id { get; set; }
    public required string Body { get; set; }
    public required int WroteBy { get; set; }
    public required int PostId { get; set; }
}