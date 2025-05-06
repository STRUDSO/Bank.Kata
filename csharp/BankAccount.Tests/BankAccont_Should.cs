namespace BankAccount.Test;

public class BankAccont_Should
{
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

    [Fact]
    public void DepositTest()
    {
        Deposit(of: 1000, on: "10-01-2012");
        Deposit(of: 2000, on: "13-01-2012");
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

    private void Deposit(int of, string on)
    {
        throw new NotImplementedException();
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