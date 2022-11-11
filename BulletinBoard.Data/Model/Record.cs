using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Data.Model;

public class Record
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecordId { get; set; }
    
    public string EMail { get; set; }
    public string RecordTitle { get; set; }
    public string RecordText { get; set; }
    public string RecordPassword { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }

    [InverseProperty(nameof(Comment.Record))]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}