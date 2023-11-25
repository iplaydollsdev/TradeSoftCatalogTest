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
    /// Interaction logic for EditAnalog.xaml
    /// </summary>
    public partial class EditAnalog : Window
    {
        public EditAnalog(AnalogModel analogModel)
        {
            InitializeComponent();
            EditAnalogViewModel viewModel = new(analogModel);
            this.DataContext = viewModel;
        }
    }
}
