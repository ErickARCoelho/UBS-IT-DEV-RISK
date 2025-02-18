using TesteTecnico.Dominio.Interfaces;
using TesteTecnico.Dominio.Servicos;
using TesteTecnico.Dominio.Servicos.Categorias;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputLines = new List<string>();
        string line;

        Console.WriteLine("Informe a entrada. Ao final pressione a tecla ENTER duas vezes para realizar o processamento: ");
        while ((line = Console.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
        {
            inputLines.Add(line);
        }
        var categories = new List<ITradeCategory>
            {
                new ExpiredTradeCategory(),
                new HighRiskTradeCategory(),
                new MediumRiskTradeCategory()
            };

        var categorizer = new TradeCategorizer(categories);
        var results = categorizer.ProcessInput(inputLines);

        foreach (var result in results)
            Console.WriteLine(result);
    }
}