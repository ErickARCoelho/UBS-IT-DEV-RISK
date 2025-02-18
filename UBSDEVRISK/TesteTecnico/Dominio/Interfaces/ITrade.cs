namespace TesteTecnico.Dominio.Interfaces
{
    public interface ITrade
    {
        double value { get; }
        string clientSector { get; }
        DateTime nextPaymentDate { get; }
    }
}
