namespace Se.Domain.Shared.ValueObjects;

public class Quantity : ValueObject<decimal>
{
    public Quantity(decimal value) : base(value)
    {
        if (value < 0.000001M)
            throw new ArgumentOutOfRangeException(nameof(value));
    }

    public static implicit operator Quantity(decimal value) => new(value);
}
