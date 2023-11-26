using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSoftCatalogTest.MVVM.Model
{
    /// <summary>
    /// Представление связи одного товара с другим
    /// </summary>
    public class AnalogChain
    {
        public string AtricleFrom {  get; set; }
        public string AtricleTo { get; set; }
    }
}
