using BankAccount.Ports;
using BankAccount.Test.Doubles;

using Xunit.Abstractions;
using Xunit.Sdk;

namespace BankAccount.Test.Unit;

public class BankAccount_Should
{
    private readonly ITestOutputHelper _output;
    private readonly AccountService _accountService;
    private readonly BankStatementPrinterSpy _bankStatementPrinter;
    private readonly CalendarStub _calendarStub;

    public BankAccount_Should(ITestOutputHelper output)
    {
        _output = output;
        _calendarStub = new CalendarStub();
        _bankStatementPrinter = new BankStatementPrinterSpy();
        _accountService = new AccountService(_calendarStub, new FakeTransactionRepository(), _bankStatementPrinter);
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
                                 Date    || Amount || Balance
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
        string actual = _bankStatementPrinter.LastPrintOut;
        _output.WriteLine("EXPECTED:");
        _output.WriteLine(expected);
        _output.WriteLine("");
        _output.WriteLine("ACTUAL:");
        _output.WriteLine(actual);

        Assert.Equal(expected,
            actual,
            ignoreLineEndingDifferences: true,
            ignoreWhiteSpaceDifferences: true,
            ignoreAllWhiteSpace: true,
            ignoreCase: true);
    }
}