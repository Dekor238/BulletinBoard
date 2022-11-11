namespace BulletinBoard.InputModel;

public class CommentDetailViewModel
{
    public int CommentId { get; set; }
    public int RecordId { get; set; }
    public string UserName { get; set; }
    public string CommentText { get; set; }
    public DateTime Date { get; set; }
    public int IsDeleted { get; set; }
}