using BankAccount.Ports;

namespace BankAccount.Test.Doubles;

public class FakeTransactionRepository : ITransactionRepository
{
    private List<object> _stored = [];

    public IEnumerable<object> AllTransactions()
    {
        return _stored;
    }

    public void Add(object transfer)
    {
        _stored.Add(transfer);
    }
}