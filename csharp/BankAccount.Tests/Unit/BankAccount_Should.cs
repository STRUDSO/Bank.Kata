using BankAccount.Ports;
using BankAccount.Test.Doubles;

namespace BankAccount.Test.Unit;

public class BankAccount_Should
{
    private readonly AccountService _accountService;
    private readonly BankStatementPrinterSpy _bankStatementPrinter;
    private readonly CalendarStub _calendarStub;
    private readonly ITransactionRepository _transactionRepository;

    public BankAccount_Should()
    {
        _calendarStub = new CalendarStub();
        _transactionRepository = new FakeTransactionRepository();
        _bankStatementPrinter = new BankStatementPrinterSpy();
        _accountService = new AccountService(_calendarStub, _transactionRepository, _bankStatementPrinter);
    }

    [Fact]
    public void Print_Account_Header()
    {
        PrintBankStatement();

        ExpectedPrintedStatement("Date       || Amount || Balance");
    }



    private void Deposit(int of, DateTime on)
    {
        _calendarStub.ReturnOnce(on);
        _accountService.Deposit(of);
    }

    private void WithDraw(int of, DateTime dateTime)
    {
        _calendarStub.ReturnOnce(dateTime);
        _accountService.Withdraw(of);
    }

    private void PrintBankStatement()
    {
        _accountService.PrintStatement();
    }

    private void ExpectedPrintedStatement(string expected)
    {
        Assert.Equal(expected, _bankStatementPrinter.LastPrintOut);
    }
}