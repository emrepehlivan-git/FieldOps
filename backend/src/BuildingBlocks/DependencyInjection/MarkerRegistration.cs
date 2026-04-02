using FieldOps.BuildingBlocks.DependencyInjection.Markers;

namespace FieldOps.BuildingBlocks.DependencyInjection;

internal static class MarkerRegistration
{
    private static readonly Type[] LifetimeMarkers =
    [
        typeof(ITransientDependency),
        typeof(IScopedDependency),
        typeof(ISingletonDependency)
    ];

    internal static bool IsLifetimeMarker(Type type) =>
        LifetimeMarkers.Contains(type);

    internal static Type ResolveServiceType(Type implementation)
    {
        if (implementation.ContainsGenericParameters)
            return implementation;

        var expectedName = $"I{implementation.Name}";
        foreach (var iface in implementation.GetInterfaces())
        {
            if (!iface.IsPublic || iface.Name != expectedName)
                continue;
            if (IsLifetimeMarker(iface))
                continue;
            return iface;
        }

        return implementation;
    }

    internal static bool MatchesMarker<TMarker>(Type type) where TMarker : class =>
        type is { IsClass: true, IsAbstract: false }
        && !type.ContainsGenericParameters
        && typeof(TMarker).IsAssignableFrom(type);
}
