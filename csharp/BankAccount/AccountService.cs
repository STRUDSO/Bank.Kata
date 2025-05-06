using BankAccount.Ports;

namespace BankAccount;

public class AccountService
{
    private readonly IBankStatementPrinter _bankStatementPrinter;

    public AccountService(
        ICalendar calendarStub,
        ITransactionRepository transactionRepository,
        IBankStatementPrinter bankStatementPrinter)
    {
        _bankStatementPrinter = bankStatementPrinter;
    }

    public void Deposit(int amount)
    {
    }

    public void Withdraw(int amount)
    {
    }

    public void PrintStatement()
    {
        _bankStatementPrinter.Print("Date       || Amount || Balance");
    }
}