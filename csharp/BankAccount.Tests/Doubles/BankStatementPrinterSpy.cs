using BankAccount.Ports;

namespace BankAccount.Test.Doubles;

public class BankStatementPrinterSpy : IBankStatementPrinter
{
    public string? LastPrintOut { get; }
}