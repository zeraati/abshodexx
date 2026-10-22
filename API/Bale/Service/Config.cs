using System.Reflection;

namespace Bale;

public static partial class BaleConfig
{
    public static IServiceCollection AddBaleConfig(this IServiceCollection services)
    {
        services.AddScoped<Service>();

        var commands = Assembly.GetEntryAssembly()!.GetTypes().Where(x => typeof(CommandHandler).IsAssignableFrom(x)).ToArray();
        foreach (var command in commands) services.AddScoped(typeof(CommandHandler), command);

        var flows = Assembly.GetEntryAssembly()!.GetTypes().Where(x => typeof(IFlowHandler).IsAssignableFrom(x)).ToArray();
        foreach (var flow in flows) services.AddScoped(typeof(IFlowHandler), flow);


        services.AddScoped<Dispatcher>();
        return services;
    }
}