using FieldOps.BuildingBlocks.DependencyInjection.Markers;

namespace FieldOps.BuildingBlocks.Ids;

public sealed class Version7GuidGenerator : IGuidGenerator, ISingletonDependency
{
    public Guid NewGuid() => Guid.CreateVersion7();
}

