using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text.Json;
using WebApi.Data;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;
using WebApi.StartupTasks;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(builder.Configuration.GetValue<LogEventLevel>("Logging:LogLevel:Default"))
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Logs:Serilog"], rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options => options
    .UseInMemoryDatabase(databaseName: "webapi")
#if DEBUG
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
#endif
);

builder.Services.AddDataProtection().PersistKeysToDbContext<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
builder.Services.AddCors(option => option.AddDefaultPolicy(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Services
builder.Services
    .AddScoped<IProductService, ProductService>();

// Startup tasks
builder.Services
    .AddStartupTask<DatabaseMigrationStartupTask>()
    .AddStartupTask<FakeDataGenerationTask>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

app.RunStartupTasks();
app.Run();
