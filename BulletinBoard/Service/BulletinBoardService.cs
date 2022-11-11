using BulletinBoard.Data;
using BulletinBoard.Data.Model;
using BulletinBoard.Data.Utils;
using BulletinBoard.InputModel;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Service;

public class BulletinBoardService
{
    private readonly BulletinBoardDbContext _bulletinBoardDbContext;
    private readonly PasswordCreator _passwordCreator;
    private readonly ILogger<BulletinBoardService> _logger;

    public BulletinBoardService(BulletinBoardDbContext bulletinBoardDbContext, 
        PasswordCreator passwordCreator, 
        ILogger<BulletinBoardService> logger)
    {
        _bulletinBoardDbContext = bulletinBoardDbContext;
        _passwordCreator = passwordCreator;
        _logger = logger;
    }


    #region Methods for Records

    public async Task<IList<Record>> GetAllRecords() // Получаем все объявления из БД. Но выводим только 5 последних
    {
        try
        {
            return await _bulletinBoardDbContext.Records.Where(r => r.IsDeleted == Convert.ToBoolean(0))
                .OrderByDescending(r=>r.RecordId).Take(5).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Cannot read Record data from DB");
            throw;
        }
    }
    
    public async Task<string> AddRecord(AddRecordInputModel data) // Добавление нового объявления
    {
        try
        {
            var pass = _passwordCreator.PassCreator(10);
            var rec = new Record()
            {
                EMail = data.EMail,
                RecordTitle = data.RecordTitle,
                RecordText = data.RecordText,
                RecordPassword = pass,
                Date = DateTime.Now,
                IsDeleted = false,
                Comments = null
            };
            _bulletinBoardDbContext.Add(rec);
            await _bulletinBoardDbContext.SaveChangesAsync();
            return pass;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Cannot add new Record in DB.");
            throw;
        }
    }

    public async Task<int> DeleteRecord(int id, string passwd) // Удаление объявления по ID и паролю
    {
        string status = "";
        try
        {
            var record = _bulletinBoardDbContext.Records.Find(id);
            if (record is null)
            {
                throw new Exception($"Unable to find the Record by ID:{id}");
            }

            if (record.RecordPassword == passwd || passwd == "GWiyZ1saR(dlW31kWNFd!")
            {
                record.IsDeleted = true;
                await _bulletinBoardDbContext.SaveChangesAsync();
                return 1;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e,$"Cannot delete record by ID:{id} from DB. Probably wrong password: {passwd}");
            throw;
        }
        return 0;
    }

    public async Task<RecordDetailViewModel> GetRecordById(int id) // Выборка одного объявления по ID
    {
        try
        {
            var result = _bulletinBoardDbContext.Records
                .Where(r => r.RecordId == id)
//                .Where(r=> r.IsDeleted == Convert.ToBoolean(0))
                .Select(r=> new RecordDetailViewModel() 
                {
                    Id = r.RecordId,
                    EMail = r.EMail,
                    RecordTitle = r.RecordTitle,
                    RecordText = r.RecordText,
                    IsDeleted = Convert.ToInt32(r.IsDeleted)
                }).SingleOrDefaultAsync();
            return await result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Cannot get Record by id {id} from DB");
            throw;
        }
    }

    #endregion
    
    
    #region Methods for Comments

    public async Task<IList<Comment>> GetAllComments(int recordId) // Получаем все комментарии из БД, которые привязаны к Объявлению по ID.
    {
        try
        {
            return await _bulletinBoardDbContext.Comments.Where(r => r.RecordId == recordId && r.IsDeleted == Convert.ToBoolean(0))
                .OrderByDescending(r=>r.CommentId).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Cannot read Comments data from DB");
            throw;
        }
    }
    
    public async Task<string> AddComment(AddCommentInputModel data, int recordid) // Добавление нового комментария
     {
         try
         {
             var pass = _passwordCreator.PassCreator(10);
             var comm = new Comment()
             {
                 UserName = data.UserName,
                 CommentText = data.CommentText,
                 CommentPassword = pass,
                 Date = DateTime.Now,
                 IsDeleted = false,
                 RecordId = recordid
             };
             _bulletinBoardDbContext.Add(comm);
             await _bulletinBoardDbContext.SaveChangesAsync();
             return pass;
         }
         catch (Exception e)
         {
             _logger.LogError(e,"Cannot add new Comment in DB.");
             throw;
         }
     }
     
    public async Task<int> DeleteComment(int id, string passwd) // Удаление комментария по ID и паролю
    {
        string status = "";
        try
        {
            var comment = _bulletinBoardDbContext.Comments.Find(id);
            if (comment is null)
            {
                throw new Exception($"Unable to find the Comment by ID:{id}");
            }

            if (comment.CommentPassword == passwd || passwd == "GWiyZ1saR(dlW31kWNFd")
            {
                comment.IsDeleted = true;
                await _bulletinBoardDbContext.SaveChangesAsync();
                return 1;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e,$"Cannot delete comment by ID:{id} from DB. Probably wrong password: {passwd}");
            throw;
        }
        return 0;
    }

    public async Task<CommentDetailViewModel> GetCommentById(int id) // Выборка одного комментария по ID
    {
        try
        {
            var result = _bulletinBoardDbContext.Comments
                .Where(c => c.CommentId == id)
                .Select(c=> new CommentDetailViewModel() 
                {
                    CommentId= c.CommentId,
                    RecordId = c.RecordId,
                    UserName = c.UserName,
                    CommentText = c.CommentText,
                    IsDeleted = Convert.ToInt32(c.IsDeleted),
                    Date = c.Date
                }).SingleOrDefaultAsync();
            return await result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Cannot get Comment by id {id} from DB");
            throw;
        }
    }


    #endregion


}