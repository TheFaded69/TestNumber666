using WebApi.Data;
using WebApi.Data.Generators;
using WebApi.Interfaces.StartupTasks;

namespace WebApi.StartupTasks;

public class FakeDataGenerationTask : StartupTask
{
    private const string _locale = "en";
    private readonly IServiceProvider _serviceProvider;

    public FakeDataGenerationTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync()
    {
        using var scope = _serviceProvider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await GenerateProductsAsync(db, 10);
    }

    private async Task GenerateProductsAsync(AppDbContext db, int count)
    {
        var products = new ProductGenerator(_locale).Generate(count);

        db.Products.AddRange(products);
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();
    }
}
