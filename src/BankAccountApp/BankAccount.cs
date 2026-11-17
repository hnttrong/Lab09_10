namespace BankAccountApp;

public sealed class BankAccount
{
    public BankAccount(string customerName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new ArgumentException("Customer name is required.", nameof(customerName));
        }

        if (initialBalance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");
        }

        CustomerName = customerName;
        Balance = initialBalance;
    }

    public string CustomerName { get; }

    public decimal Balance { get; private set; }

    public void Debit(decimal amount)
    {
        ValidatePositiveAmount(amount);
        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        ValidatePositiveAmount(amount);
        Balance += amount;
    }

    public void Withdraw(decimal amount) => Debit(amount);

    private static void ValidatePositiveAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }
    }
}
