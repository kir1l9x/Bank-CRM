namespace Entities.Transactions;

public abstract record TransactionType
{
    public sealed record WriteOff : TransactionType
    {
        public override string ToString()
        {
            return "WriteOff";
        }
    }

    public sealed record Replenishment : TransactionType
    {
        public override string ToString()
        {
            return "Replenishment";
        }
    }
}