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
    }

    public void PrintStatement()
    {
        string header = "Date       || Amount || Balance";
        foreach(var transaction in _transactionRepository.AllTransactions())
        {
            header += "\n" +transaction.Print();
        }
        _bankStatementPrinter.Print(header);
    }
}