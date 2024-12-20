namespace Entities.Accounts;

public class Account
{
    public Guid Id { get; }

    public string AccountNumber { get; }

    public Guid UserId { get; }

    public Money Balance { get; private set; }

    private Account(Guid id, string accountNumber, Guid userId, Money balance)
    {
        Id = id;
        AccountNumber = accountNumber;
        UserId = userId;
        Balance = balance;
    }

    public static AccountBuilder Builder()
    {
        return new AccountBuilder();
    }

    public void UpdateMoney(Money money)
    {
        Balance = money;
    }

    public override string ToString()
    {
        return $"{Id}\n" +
               $"{AccountNumber}\n" +
               $"{UserId}\n" +
               $"{Balance}";
    }

    public class AccountBuilder
    {
        private Guid? _id;
        private string? _accountNumber;
        private Guid? _userId;
        private Money? _balance;

        public AccountBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }

        public AccountBuilder SetAccountNumber(string accountNumber)
        {
            _accountNumber = accountNumber;
            return this;
        }

        public AccountBuilder SetUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public AccountBuilder SetBalance(Money balance)
        {
            _balance = balance;
            return this;
        }

        public Account Build()
        {
            return new Account(
                _id ?? Guid.NewGuid(),
                _accountNumber ?? throw new NullReferenceException(nameof(_accountNumber)),
                _userId ?? throw new NullReferenceException(nameof(_userId)),
                _balance ?? new Money(0));
        }
    }
}