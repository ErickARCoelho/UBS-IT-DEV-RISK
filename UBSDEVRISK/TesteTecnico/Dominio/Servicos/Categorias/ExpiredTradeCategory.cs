using TesteTecnico.Dominio.Interfaces;

namespace TesteTecnico.Dominio.Servicos.Categorias
{
    public class ExpiredTradeCategory : ITradeCategory
    {
        public string categoryName => "EXPIRED";

        public bool isMatch(ITrade trade, DateTime referenceDate)
        {
            return (referenceDate - trade.nextPaymentDate).TotalDays > 30;
        }
    }
}
