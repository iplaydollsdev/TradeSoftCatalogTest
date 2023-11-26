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
    /// <summary>
    /// Представление логики для окна FindAnalog
    /// </summary>
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

        /// <summary>
        /// Проивзодит поиск аналога с помощью класса RouteFinder
        /// </summary>
        /// <param name="obj"></param>
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
