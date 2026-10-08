namespace ApiContracts.Comment;

public class CreateCommentDTO
{
    public string Body { get; set; }
    public int WroteBy { get; set; }
    public required int PostId { get; set; }
}