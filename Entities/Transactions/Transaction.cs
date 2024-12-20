namespace Entities.Transactions;

public class Transaction
{
    public Guid Id { get; }

    public Guid AccountId { get; }

    public Money Amount { get; }

    public TransactionType Type { get; }

    public DateTime Date { get; }

    private Transaction(Guid id, Guid accountId, Money amount, TransactionType type, DateTime date)
    {
        Id = id;
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Date = date;
    }

    public static TransactionBuilder Builder()
    {
        return new TransactionBuilder();
    }

    public override string ToString()
    {
        return $"{Id}\n" +
               $"{AccountId}\n" +
               $"{Amount}\n" +
               $"{Type}\n";
    }

    public class TransactionBuilder
    {
        private Guid? _id;
        private Guid? _accountId;
        private Money? _amount;
        private TransactionType? _type;
        private DateTime? _date;

        public TransactionBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }

        public TransactionBuilder SetAccountId(Guid accountId)
        {
            _accountId = accountId;
            return this;
        }

        public TransactionBuilder SetAmount(Money amount)
        {
            _amount = amount;
            return this;
        }

        public TransactionBuilder SetType(string type)
        {
            switch (type)
            {
                case "Replenishment":
                    _type = new TransactionType.Replenishment();
                    break;
                case "WriteOff":
                    _type = new TransactionType.WriteOff();
                    break;
            }

            return this;
        }

        public TransactionBuilder SetDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public Transaction Build()
        {
            return new Transaction(
                _id ?? Guid.NewGuid(),
                _accountId ?? throw new NullReferenceException(),
                _amount ?? throw new NullReferenceException(),
                _type ?? throw new NullReferenceException(),
                _date ?? DateTime.Now);
        }
    }
}