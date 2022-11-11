using BulletinBoard.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Data;

public class BulletinBoardDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Record> Records { get; set; }

    public BulletinBoardDbContext(DbContextOptions<BulletinBoardDbContext> options) : base (options)
    {
    }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Строка подключения к БД
        optionsBuilder.UseSqlite("Filename=bulletinboard.db");
    }
}