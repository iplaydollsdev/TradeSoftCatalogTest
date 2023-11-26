using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSoftCatalogTest.MVVM.Model
{
    /// <summary>
    /// Представление полного пути от исходного товара до искомого в виде коллекции связей
    /// </summary>
    public class AnalogRoute
    {
        public List<AnalogChain> Chains { get; set; } = new();
    }
}
