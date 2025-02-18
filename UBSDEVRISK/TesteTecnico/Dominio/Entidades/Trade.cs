using TesteTecnico.Dominio.Interfaces;

namespace TesteTecnico.Dominio.Entidades
{
    public class Trade : ITrade
    {
        public double value { get; set; }
        public string? clientSector { get; set; }
        public DateTime nextPaymentDate { get; set; }
    }
}
