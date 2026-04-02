using System.Collections.Generic;

namespace FieldOps.BuildingBlocks.Persistence.Entities;

public abstract class EntityBase<TId> : IEntity<TId>, IEquatable<EntityBase<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; } = default!;

    protected EntityBase()
    {
    }

    protected EntityBase(TId id) =>
        Id = id;

    public bool IsTransient() =>
        EqualityComparer<TId>.Default.Equals(Id, default!);

    public bool Equals(EntityBase<TId>? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        if (other.GetType() != GetType())
            return false;
        if (IsTransient() || other.IsTransient())
            return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj) =>
        obj is EntityBase<TId> other && Equals(other);

    public override int GetHashCode() =>
        IsTransient() ? base.GetHashCode() : HashCode.Combine(GetType(), Id);

    public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right) =>
        Equals(left, right);

    public static bool operator !=(EntityBase<TId>? left, EntityBase<TId>? right) =>
        !Equals(left, right);
}
