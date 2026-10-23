using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Common;

public static partial class ServiceConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var scopedServices = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface == false && typeof(IScoped).IsAssignableFrom(x)).ToArray();
        foreach (var service in scopedServices) services.AddScoped(service);

        var hostedServices = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface == false && typeof(IHosted).IsAssignableFrom(x)).ToArray();
        foreach (var service in hostedServices) services.AddSingleton(typeof(IHostedService), service);

        return services;
    }
}

public interface IScoped { }
public interface IHosted { }