using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records.Comments;

public class AddComment : PageModel
{
    private readonly BulletinBoardService _bulletinBoardService;
    private readonly ILogger<AddComment> _logger;

    public AddComment(BulletinBoardService bulletinBoardService, ILogger<AddComment> logger)
    {
        _bulletinBoardService = bulletinBoardService;
        _logger = logger;
    }

    [BindProperty]
    public AddCommentInputModel Input { get; set; }
    public int RId { get; set; }
    public void OnGet(int id)
    {
        RId = id;
    }
    
    public async Task<IActionResult> OnPost(int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                string p = await _bulletinBoardService.AddComment(Input, id);
                return RedirectToPage("/Records/Service/ViewPassword", new {
                    view = p, 
                    type = "комментарий",
                    linkpage = $"/Records/ViewRecord",
                    linkname = "Вернуться к объявлению",
                    recordId = id,
                });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured saving the comment");
        }

        return Page();
    }

    
}