using System.Reflection;

namespace App.Feature;

public static partial class BaleConfig
{
    public static IServiceCollection AddWorkflowServices(this IServiceCollection services)
    {
        var commands = Assembly.GetExecutingAssembly().GetTypes().Where(x =>x.IsInterface==false && typeof(IWorkflowCommandHandler).IsAssignableFrom(x)).ToArray();
        foreach (var command in commands) services.AddScoped(typeof(IWorkflowCommandHandler), command);

        var flows = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface == false && typeof(IWorkflowFlowHandler).IsAssignableFrom(x)).ToArray();
        foreach (var flow in flows) services.AddScoped(typeof(IWorkflowFlowHandler), flow);

        return services;
    }
}