using System.Runtime.InteropServices.JavaScript;

using BankAccount.Ports;

namespace BankAccount;

public class AccountService
{
    private readonly ICalendar _calendarStub;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBankStatementPrinter _bankStatementPrinter;

    public AccountService(
        ICalendar calendarStub,
        ITransactionRepository transactionRepository,
        IBankStatementPrinter bankStatementPrinter)
    {
        _calendarStub = calendarStub;
        _transactionRepository = transactionRepository;
        _bankStatementPrinter = bankStatementPrinter;
    }

    public void Deposit(int amount)
    {
        _transactionRepository.Add(new Transfer(_calendarStub.CurrentDate, amount));
    }

    public void Withdraw(int amount)
    {
        _transactionRepository.Add(new Transfer(_calendarStub.CurrentDate, -amount));
    }

    public void PrintStatement()
    {
        PrintableStatement printableStatement = PrintableStatement.Create(_transactionRepository.AllTransactions());

        _bankStatementPrinter.Print(printableStatement.MakeStatement());
    }
}