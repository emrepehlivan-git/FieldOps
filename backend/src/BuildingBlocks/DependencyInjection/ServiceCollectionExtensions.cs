using System.Reflection;
using FieldOps.BuildingBlocks.DependencyInjection.Markers;
using FieldOps.BuildingBlocks.Guards;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.BuildingBlocks.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMarkerRegistrations(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        Guard.ThrowIfNullOrEmpty(assemblies);
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.Where(MarkerRegistration.MatchesMarker<ITransientDependency>))
            .As(t => [MarkerRegistration.ResolveServiceType(t)])
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.Where(MarkerRegistration.MatchesMarker<IScopedDependency>))
            .As(t => [MarkerRegistration.ResolveServiceType(t)])
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.Where(MarkerRegistration.MatchesMarker<ISingletonDependency>))
            .As(t => [MarkerRegistration.ResolveServiceType(t)])
            .WithSingletonLifetime());

        return services;
    }
}
