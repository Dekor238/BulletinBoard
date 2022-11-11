namespace BulletinBoard.InputModel;

public class RecordDetailViewModel
{
    public int Id { get; set; }
    public string EMail { get; set; }
    public string RecordTitle { get; set; }
    public string RecordText { get; set; }
    public int IsDeleted { get; set; }
}