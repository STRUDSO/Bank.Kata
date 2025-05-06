namespace BankAccount.Ports;

public interface IBankStatementPrinter
{
    void Print(string text);
}