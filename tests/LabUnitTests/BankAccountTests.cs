using BankAccountApp;
using System.Globalization;

namespace LabUnitTests;

public sealed class BankAccountTests
{
    [Theory]
    [InlineData("Trung Pham", 1000, 200, 800)]
    [InlineData("Ky Pham", 500, 100, 400)]
    [InlineData("Thai Pham", 300, 50, 250)]
    [InlineData("Boundary", 100, 100, 0)]
    public void Debit_ValidPartitions_UpdatesBalance(string name, decimal initial, decimal amount, decimal expected)
    {
        var account = new BankAccount(name, initial);
        account.Debit(amount);
        Assert.Equal(expected, account.Balance);
    }

    [Fact]
    public void Debit_AmountAboveBalance_ThrowsInsufficientFunds() =>
        Assert.Throws<InvalidOperationException>(() => new BankAccount("Ky Trung Pham", 100).Debit(150));

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Operations_NonPositiveAmount_Throw(decimal amount)
    {
        var account = new BankAccount("BVA", 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(amount));
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(amount));
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(amount));
    }

    [Fact]
    public void Credit_ValidAmount_IncreasesBalance()
    {
        var account = new BankAccount("Credit", 100);
        account.Credit(25);
        Assert.Equal(125, account.Balance);
    }

    [Fact]
    public void Debit_FromCsv_HandlesExpectedBalanceOrError()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", "BankAccountTestData.csv");
        foreach (var line in File.ReadLines(path).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            var cells = line.Split(',');
            var account = new BankAccount(cells[0], decimal.Parse(cells[1], CultureInfo.InvariantCulture));
            var amount = decimal.Parse(cells[2], CultureInfo.InvariantCulture);
            if (cells[3] == "Insufficient funds")
            {
                Assert.Throws<InvalidOperationException>(() => account.Debit(amount));
            }
            else
            {
                account.Debit(amount);
                Assert.Equal(decimal.Parse(cells[3], CultureInfo.InvariantCulture), account.Balance);
            }
        }
    }
}
