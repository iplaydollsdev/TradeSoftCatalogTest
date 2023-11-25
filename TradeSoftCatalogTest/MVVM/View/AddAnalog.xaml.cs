﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddAnalog.xaml
    /// </summary>
    public partial class AddAnalog : Window
    {
        public AddAnalog(ObservableCollection<AnalogModel> analogModels)
        {
            InitializeComponent();
            AddAnalogViewModel viewModel = new(analogModels);
            this.DataContext = viewModel;
        }
    }
}