namespace Messaging;

public readonly struct Unit : IEquatable<Unit>
{
    public bool Equals(Unit other) => true;

    public override bool Equals(object? obj) => obj is Unit other && this.Equals(other);

    public override int GetHashCode() => 0;

    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    public static bool operator !=(Unit left, Unit right) => !left.Equals(right);
        
    public static Unit Default { get; } = new Unit();
}