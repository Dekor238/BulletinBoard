using BulletinBoard.Data.Model;
using BulletinBoard.Data.Utils;
using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly BulletinBoardService _bulletinBoardService;
    
    public IndexModel(ILogger<IndexModel> logger, BulletinBoardService bulletinBoardService)
    {
        _logger = logger;
        _bulletinBoardService = bulletinBoardService;
    }

    public IList<Record> AllRecords { get; private set; }
    public IList<Comment> Comments { get; private set; }
    public DateTime f { get; set; }
    public async Task OnGet()
    {
        AllRecords = await _bulletinBoardService.GetAllRecords();
        foreach (var r in AllRecords)
        {
            Comments = await _bulletinBoardService.GetAllComments(r.RecordId);
        }
    }
}