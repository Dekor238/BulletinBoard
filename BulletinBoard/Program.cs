using BulletinBoard.Data;
using BulletinBoard.Data.Utils;
using BulletinBoard.Service;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

//public IConfiguration Configuration {  get;  }
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();//.AddRazorRuntimeCompilation();
    //TODO: Отключить при продуктиве
    
    // SQLite DB - подключаем сервис использования SQLite базы данных
    builder.Services.AddDbContext<BulletinBoardDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection"));
    });

    builder.Services.AddScoped<PasswordCreator>();
    builder.Services.AddScoped<BulletinBoardService>();
    
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(Convert.ToInt32(builder.Configuration["ListenPorts:Port"])); // to listen for incoming http connection on port 5002
    });
    
    // NLog: Setup NLog for Dependency injection
    // Для расширенного логирования данных запроса HTML
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();


    var app = builder.Build();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseStatusCodePagesWithRedirects("/{0}");
    //app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    //app.UseAuthorization();
    app.MapRazorPages();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception"); // Логируем их в файл
    throw;
}
finally
{
    LogManager.Shutdown();
}