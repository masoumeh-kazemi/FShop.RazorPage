namespace FShop.RazorPage.Models.Comments;


public class CommentDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string UserFullName { get; set; }
    public string ProductTitle { get; set; }
    public string Text { get; set; }
}

public class BaseFilterParam
{
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10; 
}

public class CommentFilterParam : BaseFilterParam
{
    public long UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CommentStatus CommentStatus { get; set; }
}


public class CommentFilterResult : BaseFilter<CommentDto, CommentFilterParam>
{

}


public enum CommentStatus
{
    Pennding,
    Accepted,
    Rejected
}