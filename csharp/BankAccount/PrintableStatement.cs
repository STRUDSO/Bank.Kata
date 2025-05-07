using BankAccount.Ports;

namespace BankAccount;

public class PrintableStatement
{
    private int balance = 0;
    private List<PrintableLine> lines = [];
    public string MakeStatement()
    {
        string dateAmountBalance = "Date       || Amount || Balance";
        foreach (var line in lines.OrderByDescending(x => x.Date))
        {
            dateAmountBalance += "\n" + line.Print();
        }
        return dateAmountBalance;
    }

    public void Add(Transfer trans)
    {
        balance += trans.Amount;
        lines.Add(new PrintableLine(trans, balance));
    }

    public static PrintableStatement Create(IEnumerable<Transfer> allTransactions)
    {
        PrintableStatement printableStatement = new();

        foreach (var trans in allTransactions.OrderBy(x => x.TransferDate))
        {
            printableStatement.Add(trans);
        }

        return printableStatement;
    }
}