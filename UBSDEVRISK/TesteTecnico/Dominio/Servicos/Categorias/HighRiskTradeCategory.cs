using TesteTecnico.Dominio.Interfaces;

namespace TesteTecnico.Dominio.Servicos.Categorias
{
    public class HighRiskTradeCategory : ITradeCategory
    {
        public string categoryName => "HIGHRISK";

        public bool isMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.value > 1000000 && trade.clientSector.Equals("Private", StringComparison.OrdinalIgnoreCase);
        }
    }
}
