using Microsoft.Extensions.DependencyInjection;
using Mzu.Libs.Rollercoaster.Configurations;
using Mzu.Libs.Rollercoaster.Options;


namespace Mzu.Libs.Rollercoaster.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection RegisterRollerCoaster(
        this IServiceCollection services,
        Action<RollerCoasterOptions>? configureOptions)
    {
        RollerCoasterOptions options = new();

        configureOptions?.Invoke(options);

        Session.ConfiguredOptions = options;

        return services;
    }

}
