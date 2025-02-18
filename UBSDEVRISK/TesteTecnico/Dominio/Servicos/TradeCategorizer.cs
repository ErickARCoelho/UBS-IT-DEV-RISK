using System.Globalization;
using TesteTecnico.Dominio.Entidades;
using TesteTecnico.Dominio.Interfaces;

namespace TesteTecnico.Dominio.Servicos
{
    public class TradeCategorizer
    {
        private readonly IEnumerable<ITradeCategory> _categories;

        public TradeCategorizer(IEnumerable<ITradeCategory> categories) => _categories = categories;

        public List<string> ProcessInput(List<string> inputLines)
        {
            ValidateInputForProcessing(inputLines, out List<string> results, out DateTime referenceDate, out int nrOperacoes);

            if (results.Count > 0)
                return results;

            for (int i = 0; i < nrOperacoes; i++)
            {
                string tradeLine = inputLines[i + 2];
                var parts = tradeLine.Split(' ');

                if (parts.Length != 3)
                {
                    results.Add($"UNCATEGORIZED - Formato de registro inválido no trade {i + 1}.");
                    continue;
                }

                if (!double.TryParse(parts[0], out double value))
                {
                    results.Add($"UNCATEGORIZED - Valor inválido no trade {i + 1}.");
                    continue;
                }

                string clientSector = parts[1];

                if (!DateTime.TryParseExact(parts[2], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime nextPaymentDate))
                {
                    results.Add($"UNCATEGORIZED - Data do próximo pagamento inválida no trade {i + 1}.");
                    continue;
                }

                var trade = new Trade
                {
                    value = value,
                    clientSector = clientSector,
                    nextPaymentDate = nextPaymentDate
                };

                string category = CategorizeTrade(trade, referenceDate);
                if (category == "UNCATEGORIZED")
                    results.Add($"UNCATEGORIZED - Nenhuma regra de categorização se aplicou no trade {i + 1}.");
                else
                    results.Add(category);
            }
            return results;
        }

        private static List<string> ValidateInputForProcessing(List<string> inputLines, out List<string> results, out DateTime referenceDate, out int nrOperacoes)
        {
            results = [];
            referenceDate = default;
            nrOperacoes = 0;

            if (inputLines == null || inputLines.Count < 2)
            {
                results.Add("ERROR: A entrada deve conter ao menos 2 linhas (data de referência e quantidade de operações).");
                return results;
            }

            if (!DateTime.TryParseExact(inputLines[0], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
            {
                results.Add("ERROR: Data de referência inválida.");
            }

            if (!int.TryParse(inputLines[1], out nrOperacoes))
            {
                results.Add("ERROR: Quantidade de operações inválida.");
            }

            if (inputLines.Count < nrOperacoes + 2)
            {
                results.Add("ERROR: A quantidade de operações informada é maior do que os registros disponíveis.");
            }

            return results;
        }

        public string CategorizeTrade(ITrade trade, DateTime referenceDate)
        {
            foreach (var category in _categories)
            {
                if (category.isMatch(trade, referenceDate))
                    return category.categoryName;
            }

            return "UNCATEGORIZED";
        }

    }
}
