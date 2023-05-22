using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Reflection;

namespace Mzu.Libs.Rollercoaster;

public static class RollerCoasterMethodExecutor
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> semaphoreSlims = new ConcurrentDictionary<string, SemaphoreSlim>();
    private static readonly ConcurrentDictionary<string, IHostedService> backgroundServices = new ConcurrentDictionary<string, IHostedService>();

    private static IServiceProvider? _serviceProvider;

    public static void ExecuteRollerCoasterMethods(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var rollerCoasterMethods = (Assembly.GetEntryAssembly() ?? throw new ArgumentNullException($"[{nameof(Assembly.GetEntryAssembly)}]"))
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            .Where(m => m.GetCustomAttributes(typeof(RollerCoasterAttribute), false).Length > 0)
            .ToList();

        foreach (var method in rollerCoasterMethods)
        {
            var attribute = (RollerCoasterAttribute)method.GetCustomAttributes(typeof(RollerCoasterAttribute), false)[0];
            var interval = attribute.Interval;
            var group = attribute.Group;
            var name = attribute.Name ?? method.Name;

            var semaphoreSlim = semaphoreSlims.GetOrAdd(group, _ => new SemaphoreSlim(1));

            var backgroundService = new RollerCoasterBackgroundService(method, interval, semaphoreSlim);

            if(backgroundServices.TryAdd(name, backgroundService))
            {
                var task = backgroundService.StartAsync(CancellationToken.None);
            }  
        }
    }

    public static object? GetService(Type serviceType)
    {
        using var scope = _serviceProvider?.CreateScope();
        return scope?.ServiceProvider?.GetService(serviceType);
    }

    public static void StopBackgroundService(string name)
    {
        if (backgroundServices.TryGetValue(name, out var backgroundService))
        {
            backgroundService.StopAsync(CancellationToken.None);

            return;
        }

        throw new Exception($"No Service running with the name [{name}]");
    }

    public static void StartBackgroundService(string name)
    {
        if (backgroundServices.TryGetValue(name, out var backgroundService))
        {
            backgroundService?.StartAsync(CancellationToken.None);

            return;
        }

        throw new Exception($"No Service found with the name [{name}].");
    }


}
