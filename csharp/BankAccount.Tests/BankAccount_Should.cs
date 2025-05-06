using BankAccount.Ports;
using BankAccount.Test.Doubles;

namespace BankAccount.Test;

public class BankAccount_Should
{
    private readonly AccountService _accountService;
    private readonly CalendarStub _calendarStub;

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
    public BankAccount_Should()
    {
        _calendarStub = new CalendarStub();
        _accountService = new AccountService(_calendarStub, new FakeTransactionRepository());

    }

    [Fact]
    public void DepositTest()
    {
        Deposit(of: 1000, on: new(2012, 1, 13));
        Deposit(of: 2000, on: new(2012, 1, 13));
        WithDraw(of: 5000, on: "14-01-2012");
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

    private void WithDraw(int of, string on)
    {
        throw new NotImplementedException();
    }

    private void PrintBankStatement()
    {
        throw new NotImplementedException();
    }

    private void ExpectedPrintedStatement(string s)
    {
        throw new NotImplementedException();
    }
}