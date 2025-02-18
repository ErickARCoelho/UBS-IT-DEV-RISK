using TesteTecnico.Dominio.Interfaces;

namespace TesteTecnico.Dominio.Servicos.Categorias
{
    public class MediumRiskTradeCategory : ITradeCategory
    {
        public string categoryName => "MEDIUMRISK";

        public bool isMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.value > 1000000 && trade.clientSector.Equals("Public", StringComparison.OrdinalIgnoreCase);
        }
    }
}
