using WebApi.Interfaces.StartupTasks;

namespace WebApi.StartupTasks;

public class DatabaseMigrationStartupTask : IStartupTask
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigrationStartupTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Execute()
    {
        using var scope = _serviceProvider.CreateScope();

        // No need for UseInMemoryDatabase
        /*
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        */
    }
}
