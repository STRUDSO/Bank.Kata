using System.Collections;

namespace BankAccount.Ports;

public interface ITransactionRepository
{
    IEnumerable<Transfer> AllTransactions();
    void Add(Transfer transfer);
}

public record Transfer(DateTime TransferDate, int Amount);