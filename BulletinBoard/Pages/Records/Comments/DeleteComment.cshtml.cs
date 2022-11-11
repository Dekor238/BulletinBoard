using BulletinBoard.InputModel;
using BulletinBoard.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulletinBoard.Pages.Records.Comments;

public class DeleteComment : PageModel
{
    private readonly BulletinBoardService _bulletinBoardService;
    private readonly ILogger<DeleteComment> _logger;

    public DeleteComment(BulletinBoardService bulletinBoardService, ILogger<DeleteComment> logger)
    {
        _bulletinBoardService = bulletinBoardService;
        _logger = logger;
    }
    [BindProperty]
    public CommentDetailViewModel Comment { get; set; }
    public string Password { get; set; }
    
    public async Task OnGet(int id)
    {
        Comment = await _bulletinBoardService.GetCommentById(id);
    }

    public async Task<IActionResult> OnPost(int id, string password)
    {
        try
        {
            if (await _bulletinBoardService.DeleteComment(id, password) == 0)
            {
                ViewData["DelStatusComment"] = $"Введенный пароль не совпадает с паролем комментария. Удаление не возможно!";
            }
            else 
                ViewData["DelStatusComment"] = $"Комментарий удален из системы";
            
            Comment = await _bulletinBoardService.GetCommentById(id);
            return Page();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot delete comment");
            throw;
        }
    }

}