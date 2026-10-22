using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bale;

public static partial class BaleConfig
{
    public static IServiceCollection AddBaleConfig(this IServiceCollection services)
    {
        services.AddScoped<Service>();

        var commands = Assembly.GetCallingAssembly().GetTypes().Where(x => typeof(ICommandHandler).IsAssignableFrom(x)).ToArray();
        foreach (var command in commands) services.AddScoped(typeof(ICommandHandler), command);

        var flows = Assembly.GetCallingAssembly().GetTypes().Where(x => typeof(IFlowHandler).IsAssignableFrom(x)).ToArray();
        foreach (var flow in flows) services.AddScoped(typeof(IFlowHandler), flow);


        services.AddScoped<Dispatcher>();
        return services;
    }
}