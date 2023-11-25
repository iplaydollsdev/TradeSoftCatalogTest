using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSoftCatalogTest.MVVM.Model
{
    public class AnalogChain
    {
        public List<AnalogModel> Models { get; set; }
        public AnalogChain() 
        {
            Models = new List<AnalogModel>();
        }
        public void AddModel(AnalogModel model)
        {
            Models.Add(model);
        }
    }
}
