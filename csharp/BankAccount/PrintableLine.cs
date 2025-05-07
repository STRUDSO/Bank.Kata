using BankAccount.Ports;

namespace BankAccount;

public record PrintableLine
{
    private readonly Transfer _transfer;
    private readonly int _balance;

    public PrintableLine(Transfer transfer, int balance)
    {
        _transfer = transfer;
        _balance = balance;
    }

    public DateTime Date => _transfer.TransferDate;

    public string Print()
    {
        return _transfer.TransferDate.ToShortDateString() + " || " + _transfer.Amount + "    || " + _balance;
    }
}