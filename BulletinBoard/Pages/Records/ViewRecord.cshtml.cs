using BulletinBoard.Data.Model;
using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records;

public class ViewRecord : PageModel
{
    private readonly BulletinBoardService _bulletinBoardService;
    private readonly ILogger<ViewRecord> _logger;

    public ViewRecord(BulletinBoardService bulletinBoardService, ILogger<ViewRecord> logger)
    {
        _bulletinBoardService = bulletinBoardService;
        _logger = logger;
    }

    public RecordDetailViewModel Record { get; set; }
    public IList<Comment> Comment { get; set; }
    public async Task<IActionResult> OnGet(int id)
    {
        Record = await _bulletinBoardService.GetRecordById(id);
        if (Record == null)
        {
            return NotFound();
        }

        Comment = await _bulletinBoardService.GetAllComments(id);
        return Page();
    }
}