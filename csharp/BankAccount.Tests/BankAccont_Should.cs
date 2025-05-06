namespace BankAccount.Test;

public class BankAccont_Should
{
/*
Given a client makes a deposit of 1000 on 10-01-2012
   And a deposit of 2000 on 13-01-2012
   And a withdrawal of 500 on 14-01-2012
   When they print their bank statement
   Then they would see:

   Date       || Amount || Balance
   14/01/2012 || -500   || 2500
   13/01/2012 || 2000   || 3000
   10/01/2012 || 1000   || 1000
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
}