using BankAccount.Ports;
using BankAccount.Test.Doubles;

using Xunit.Abstractions;
using Xunit.Sdk;

namespace BankAccount.Test;

public class BankAccountScenarios
{
    private readonly ITestOutputHelper _output;
    private readonly AccountService _accountService;
    private readonly CalendarStub _calendarStub;
    private readonly BankStatementPrinterSpy _bankStatementPrinterSpy;

    public BankAccountScenarios(ITestOutputHelper _output)
    {
        this._output = _output;
        _calendarStub = new CalendarStub();
        _bankStatementPrinterSpy = new BankStatementPrinterSpy();
        _accountService = new AccountService(_calendarStub, new FakeTransactionRepository(), _bankStatementPrinterSpy);
    }

    [Fact]
    public void DepositTest()
    {
        Deposit(of: 1000, on: new(2012, 1, 13));
        Deposit(of: 2000, on: new(2012, 1, 10));
        WithDraw(of: 500, on: new(2012, 1, 14));
        PrintBankStatement();
        ExpectedPrintedStatement("""
                                 Date       || Amount || Balance
                                 14/01/2012 || -500    || 2500
                                 13/01/2012 || 1000    || 3000
                                 10/01/2012 || 2000    || 2000
                                 """
        );
    }

    private void Deposit(int of, DateTime on)
    {
        _calendarStub.ReturnOnce(on);
        _accountService.Deposit(of);
    }

    private void WithDraw(int of, DateTime on)
    {
        _calendarStub.ReturnOnce(on);
        _accountService.Withdraw(of);
    }

    private void PrintBankStatement()
    {
        _accountService.PrintStatement();
    }

    private void ExpectedPrintedStatement(string expected)
    {
        string actual = _bankStatementPrinterSpy.LastPrintOut;
        // OPIONION: For this use case the output from xunit was unsatisfactory
        _output.WriteLine("EXPECTED:");
        _output.WriteLine(expected);
        _output.WriteLine("");
        _output.WriteLine("ACTUAL:");
        _output.WriteLine(actual);
        Assert.Equal(expected,
            actual,
            ignoreLineEndingDifferences: true,
            ignoreWhiteSpaceDifferences: true,
            ignoreCase: true);
    }
}