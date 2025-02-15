using WebApi.Interfaces.StartupTasks;

namespace WebApi.Extensions;

public static class HostExtension
{
    public static IHost RunStartupTasks(this IHost host)
    {
        var startupTasks = host.Services.GetServices<IStartupTask>();

        foreach (var startupTask in startupTasks)
        {
            startupTask.Execute();
        }

        return host;
    }
}

