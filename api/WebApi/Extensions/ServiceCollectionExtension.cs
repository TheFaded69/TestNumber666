using WebApi.Interfaces.StartupTasks;

namespace WebApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddStartupTask<T>(this IServiceCollection services) where T : class, IStartupTask
    {
        return services.AddTransient<IStartupTask, T>();
    }
}
