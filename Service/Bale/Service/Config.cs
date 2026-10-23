using System.Reflection;

namespace Bale;

public static partial class BaleConfig
{
    public static IServiceCollection AddBaleConfig(this IServiceCollection services)
    {
        var commands = Assembly.GetExecutingAssembly().GetTypes().Where(x =>x.IsInterface==false && typeof(ICommandHandler).IsAssignableFrom(x)).ToArray();
        foreach (var command in commands) services.AddScoped(typeof(ICommandHandler), command);

        var flows = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface == false && typeof(IFlowHandler).IsAssignableFrom(x)).ToArray();
        foreach (var flow in flows) services.AddScoped(typeof(IFlowHandler), flow);

        return services;
    }
}