using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records.Service;

public class ViewPassword : PageModel
{
    public string RecordPassword { get; set; }
    public string Type { get; set; }
    public string Linkpage { get; set; }
    public string Linkname { get; set; }
    public int RecId { get; set; }
    public void OnGet(string view, string type, string linkpage, string linkname, int recordId)
    {
        RecordPassword = view;
        Type = type;
        Linkname = linkname;
        Linkpage = linkpage;
        RecId = recordId;
    }
}