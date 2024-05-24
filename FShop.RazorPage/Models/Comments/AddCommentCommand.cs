namespace FShop.RazorPage.Models.Comments;

public class AddCommentCommand
{
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
}

public class EditCommentCommand
{
    public string Text { get; set; }
    public long CommentId { get; set; }
    public long UserId { get; set; }
}

public class DeleteCommentCommand
{
    public string Text { get; set; }
    public long CommentId { get; set; }
    public long UserId { get; set; }
}

public class ChangeStatusCommand
{
    public long CommentId { get; set; }
    public CommentStatus Status { get; set; }
}