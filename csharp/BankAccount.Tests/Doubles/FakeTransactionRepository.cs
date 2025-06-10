using BankAccount.Ports;

namespace BankAccount.Test.Doubles;

public class FakeTransactionRepository : ITransactionRepository
{
    private List<Transfer> _stored = [];

    public IEnumerable<Transfer> AllTransactions()
    {
        return _stored;
    }

    public void Add(Transfer transfer)
    {
        _stored.Add(transfer);
    }
}