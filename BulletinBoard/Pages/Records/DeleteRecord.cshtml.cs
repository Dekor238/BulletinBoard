using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records;

public class DeleteRecord : PageModel
{
    private readonly BulletinBoardService _bulletinBoardService;
    private readonly ILogger<DeleteRecord> _logger;

    public DeleteRecord(BulletinBoardService bulletinBoardService, ILogger<DeleteRecord> logger)
    {
        _bulletinBoardService = bulletinBoardService;
        _logger = logger;
    }

    [BindProperty]
    public RecordDetailViewModel Record { get; set; }
    public string Password { get; set; }
    
    public async Task<IActionResult> OnGet(int id)
    {
        Record = await _bulletinBoardService.GetRecordById(id);
        if (Record == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPost(int id, string password)
    {
        try
        {
            if (await _bulletinBoardService.DeleteRecord(id, password) == 0)
            {
                ViewData["DelStatusRecord"] = $"Введенный пароль не совпадает с паролем объявления. Удаление не возможно!";
            }
            else 
                ViewData["DelStatusRecord"] = $"Объявление удалено из системы";

            Record = await _bulletinBoardService.GetRecordById(id);
            return Page();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot delete record");
            throw;
        }
    }
}