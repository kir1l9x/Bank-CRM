namespace Entities;

public class Money : IEquatable<Money>, IComparable<Money>
{
    private const int Scale = 2;

    public decimal Amount { get; private set; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        Amount = Math.Round(amount, Scale);
    }

    public static Money operator +(Money a, Money b)
    {
        return new Money(a.Amount + b.Amount);
    }

    public static Money operator -(Money a, Money b)
    {
        return new Money(a.Amount - b.Amount);
    }

    public static Money operator *(Money a, decimal multiplier)
    {
        return new Money(a.Amount * multiplier);
    }

    public static Money operator /(Money a, decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return new Money(a.Amount / divisor);
    }

    public int CompareTo(Money? other)
    {
        return other is null ? 1 : Amount.CompareTo(other.Amount);
    }

    public bool Equals(Money? other)
    {
        return other is not null && Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        return obj is Money other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }

    public static bool operator ==(Money a, Money b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Money a, Money b)
    {
        return !a.Equals(b);
    }

    public static bool operator >(Money a, Money b)
    {
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        return a.Amount < b.Amount;
    }

    public static bool operator >=(Money a, Money b)
    {
        return a.Amount >= b.Amount;
    }

    public static bool operator <=(Money a, Money b)
    {
        return a.Amount <= b.Amount;
    }

    public override string ToString()
    {
        return $"{Amount:F2}";
    }
}
