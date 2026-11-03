namespace ApiContracts.Comment;

public class CreateCommentDTO
{
    public required string Body { get; set; }
    public required int WroteBy { get; set; }
    public required int PostId { get; set; }
}