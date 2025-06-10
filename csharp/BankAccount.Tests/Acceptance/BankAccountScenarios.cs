using BankAccount.Test.Doubles;

using Xunit.Abstractions;

namespace BankAccount.Test.Acceptance;

public class BankAccountScenarios
{
    private readonly AccountService _accountService;
    private readonly CalendarStub _calendarStub;
    private readonly BankStatementPrinterSpy _bankStatementPrinterSpy;
    private readonly ITestOutputHelper _output;

    /*
     * Design:
     * AccountService -> Calendar Port (Stub)
     * AccountService -> Transaction Repository (Fake)
     * AccountService -> IBankStatementPrinter (Spy)
     *
     * Transfer
     * PrintableStatement
     * PrintableStatementLine
     */
    public BankAccountScenarios(ITestOutputHelper output)
    {
        _output = output;
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
                                 14/01/2012 || -500   || 2500
                                 13/01/2012 || 2000   || 3000
                                 10/01/2012 || 1000   || 1000
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
        var actual = _bankStatementPrinterSpy.LastPrintOut;
        _output.WriteLine("EXPECTED:");
        _output.WriteLine(expected);
        _output.WriteLine("");
        _output.WriteLine("ACTUAL:");
        _output.WriteLine(actual ?? "NOTHING");

        Assert.Equal(expected,
            actual,
            ignoreLineEndingDifferences: true,
            ignoreWhiteSpaceDifferences: true,
            ignoreAllWhiteSpace: true,
            ignoreCase: true);
    }
}