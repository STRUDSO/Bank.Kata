using BankAccount.Ports;

namespace BankAccount;

public class AccountService
{
    public AccountService(ICalendar calendarStub, ITransactionRepository transactionRepository)
    {

    }

    public void Deposit(int amount)
    {
    }

    public void Withdraw(int amount)
    {
        throw new NotImplementedException();
    }

    public void PrintStatement()
    {
        throw new NotImplementedException();
    }
}