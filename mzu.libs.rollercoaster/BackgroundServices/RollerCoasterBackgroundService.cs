using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Mzu.Libs.Rollercoaster;

public class RollerCoasterBackgroundService : BackgroundService
{
    private readonly MethodInfo _method;
    private readonly int _interval;
    private readonly SemaphoreSlim _semaphoreSlim;

    public RollerCoasterBackgroundService(MethodInfo method, int? interval, SemaphoreSlim semaphoreSlim)
    {
        _method = method;
        _interval = interval.Value;
        _semaphoreSlim = semaphoreSlim;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_semaphoreSlim != null)
            {
                await _semaphoreSlim.WaitAsync(stoppingToken);
            }

            try
            {
                var target = _method.IsStatic ? null : CreateInstance(_method.DeclaringType);
                _method.Invoke(target, null); // Invoke the method on the target object
            }
            finally
            {
                _semaphoreSlim?.Release();
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private object CreateInstance(Type type)
    {
        var service = RollerCoasterMethodExecutor.GetService(type);
        if (service != null)
            return service;

        var constructors = type.GetConstructors();
        if (constructors.Length == 0)
            return Activator.CreateInstance(type);

        var constructor = constructors.FirstOrDefault(c => c.GetParameters().Length > 0);
        if (constructor == null)
            return Activator.CreateInstance(type);

        var parameters = constructor.GetParameters().Select(p => RollerCoasterMethodExecutor.GetService(p.ParameterType)).ToArray();
        return Activator.CreateInstance(type, parameters);
    }
}
