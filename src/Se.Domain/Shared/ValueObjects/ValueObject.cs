namespace Se.Domain.Shared.ValueObjects;

public abstract class ValueObject<TValue>
{
    protected ValueObject(TValue value)
    {
        Value = value;
    }

    public TValue Value { get; }

    public static implicit operator TValue(ValueObject<TValue> obj) => obj.Value;
}