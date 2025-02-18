using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnico.Dominio.Interfaces
{
    public interface ITradeCategory
    {
        bool isMatch(ITrade trade, DateTime referenceDate);
        string categoryName { get; }
    }
}
