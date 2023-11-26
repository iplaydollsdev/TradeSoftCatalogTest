using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradeSoftCatalogTest.Core;
using TradeSoftCatalogTest.MVVM.View;

namespace TradeSoftCatalogTest.MVVM.ViewModel
{
    public class FindAnalogViewModel : INotifyPropertyChanged
    {
        public string ArticleFrom { get; set; }
        public string ArticleTo { get; set; }
        public int RecursionSteps { get; set; } = 5;
        public ICommand FindCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



        public FindAnalogViewModel()
        {
            FindCommand = new RelayCommand(FindCommandExecute, CanFindCommandExecute);
        }

        private bool CanFindCommandExecute(object arg)
        {
            return true;
        }

        private void FindCommandExecute(object obj)
        {
            var result = RouteFinder.Find(ArticleFrom, ArticleTo, RecursionSteps);
            if (result.Count > 0)
            {
                Route routeWindow = new(result);
                routeWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show($"Искомый товар с артикулом {ArticleTo} не найден за {RecursionSteps} шагов");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
