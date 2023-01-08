namespace Mps.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public override int GetHashCode() => GetEqualityComponents()
        .Aggregate(default(int), HashCode.Combine);

    public override bool Equals(object? obj) => Equals(obj as ValueObject);

    public bool Equals(ValueObject? other) =>
        other?.GetEqualityComponents().SequenceEqual(GetEqualityComponents()) ?? false;

    protected abstract IEnumerable<object> GetEqualityComponents();
}