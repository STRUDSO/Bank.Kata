using BankAccount.Ports;

namespace BankAccount;

public class AccountService
{
    public AccountService(
        ICalendar calendarStub,
        ITransactionRepository transactionRepository,
        IBankStatementPrinter bankStatementPrinter)
    {

    }

    public void Deposit(int amount)
    {
    }

    public void Withdraw(int amount)
    {
    }

    public void PrintStatement()
    {
    }
}