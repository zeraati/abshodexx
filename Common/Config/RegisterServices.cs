using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Common;

public static partial class ServiceConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies.Append(Assembly.GetCallingAssembly()).Distinct())
        {
            var types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract).ToList();

            // Scoped Services
            var scopedServices = types.Where(x => typeof(IScoped).IsAssignableFrom(x));
            foreach (var service in scopedServices) services.AddScoped(service);

            // Hosted Services
            var hostedServices = types.Where(x => typeof(IHosted).IsAssignableFrom(x));
            foreach (var service in hostedServices) services.AddSingleton(typeof(IHostedService), service);
        }

        return services;
    }
}

public interface IScoped { }
public interface IHosted { }