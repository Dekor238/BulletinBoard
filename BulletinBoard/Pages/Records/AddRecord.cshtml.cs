using BulletinBoard.Data.Model;
using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records;

public class AddRecord : PageModel
{
    private readonly BulletinBoardService _bulletinBoardService;
    private readonly ILogger<AddRecord> _logger;
    
    public AddRecord(BulletinBoardService bulletinBoardService, ILogger<AddRecord> logger)
    {
        _bulletinBoardService = bulletinBoardService;
        _logger = logger;
    }

    public void OnGet()
    {
        
    }

    [BindProperty] 
    public AddRecordInputModel Input { get; set; }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (ModelState.IsValid)
            {
                string p = await _bulletinBoardService.AddRecord(Input);
                // return RedirectToPage("/Records/Service/ViewPassword", new {view = p});
                return RedirectToPage("/Records/Service/ViewPassword", new {
                    view = p, 
                    type = "объявление",
                    linkpage = $"/Index",
                    linkname = "Вернуться на Главную страницу",
                    recordId = 0
                });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured saving the Record");
        }

        return Page();
    }
    
}