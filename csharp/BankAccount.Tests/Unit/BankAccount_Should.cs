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

    [Fact]
    public void Deposit_Amount()
    {
        Deposit(500, new DateTime(2012, 1, 13));

        PrintBankStatement();

        ExpectedPrintedStatement("""
                                 Date       || Amount || Balance
                                 13/01/2012 || 500    || 500
                                 """);

    }

    [Fact]
    public void WithDraw_Amount()
    {
        Deposit(500, new DateTime(2012, 1, 13));
        WithDraw(200, new DateTime(2012, 1, 14));

        PrintBankStatement();

        ExpectedPrintedStatement("""
                                 Date       || Amount || Balance
                                 14/01/2012 || -200    || 300
                                 13/01/2012 || 500    || 500
                                 """);

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