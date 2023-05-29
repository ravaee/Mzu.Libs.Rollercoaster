using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mzu.Libs.Rollercoaster.Configurations;
using System.Collections.Concurrent;
using System.Reflection;

namespace Mzu.Libs.Rollercoaster;

public static class RollerCoasterMethodExecutor
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> semaphoreSlims = new();
    private static readonly ConcurrentDictionary<string, IHostedService> backgroundServices = new();

    private static IServiceProvider? _serviceProvider;

    private static Assembly? _assemblyToScan;

    public static void InitialRollerCoasterMethodExecutor(Assembly? assemblyToScan = null)
    {
        _serviceProvider = Session.ConfiguredOptions.ServiceProvider;
        _assemblyToScan = assemblyToScan ?? Assembly.GetEntryAssembly();

        var rollerCoasterMethods = (_assemblyToScan ?? throw new ArgumentNullException($"[{nameof(Assembly.GetEntryAssembly)}]"))
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            .Where(m => m.GetCustomAttributes(typeof(RollerCoasterAttribute), false).Length > 0)
            .ToList();

        foreach (var method in rollerCoasterMethods)
        {
            var attribute = (RollerCoasterAttribute)method.GetCustomAttributes(typeof(RollerCoasterAttribute), false)[0];

            var interval = attribute.Interval;
            string group = attribute.Group ?? throw new ArgumentNullException(nameof(attribute.Group));
            var name = attribute.Name ?? method.Name;

            var semaphoreSlim = semaphoreSlims.GetOrAdd(group, _ => new SemaphoreSlim(1));

            var backgroundService = new RollerCoasterBackgroundService(method, interval, semaphoreSlim);

            backgroundServices.TryAdd(name, backgroundService);

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

    public static void StartAllBackgroundService()
    {
        backgroundServices.ToList().ForEach(backgroundService =>
        {

            backgroundService.Value.StartAsync(CancellationToken.None);
        });
    }


}
