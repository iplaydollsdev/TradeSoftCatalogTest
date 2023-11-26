using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TradeSoftCatalogTest.MVVM.Model;
using TradeSoftCatalogTest.MVVM.ViewModel;

namespace TradeSoftCatalogTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for Route.xaml
    /// </summary>
    public partial class Route : Window
    {
        public Route(List<AnalogRoute> routes)
        {
            InitializeComponent();
            RouteViewModel viewModel = new(routes);
            this.DataContext = viewModel;            
        }
    }
}
