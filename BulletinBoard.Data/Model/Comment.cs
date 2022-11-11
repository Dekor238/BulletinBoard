using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Data.Model;

public class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }
    
    [ForeignKey(nameof(Record))]
    public int RecordId { get; set; }
    
    public string UserName { get; set; }
    public string CommentText { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
    public string CommentPassword { get; set; }
    
    public virtual Record Record { get; set; }
}