using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradeSoftCatalogTest.Core;
using TradeSoftCatalogTest.MVVM.Model;
using TradeSoftCatalogTest.MVVM.View;

namespace TradeSoftCatalogTest.MVVM.ViewModel
{
    /// <summary>
    /// Представление логики для окна MainWindow
    /// </summary>
    public class MainViewModel
    {
        public ObservableCollection<AnalogModel> AnalogModels { get; set; } = new();
        public ICommand ShowAddWindowCommand { get; set; }
        public ICommand ShowEditWindowCommand { get; set; }
        public ICommand ShowFindWindowCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        

        public MainViewModel()
        {
            GetAnalogModels();

            ShowAddWindowCommand = new RelayCommand(ShowAddWindow, CanShowAddWindow);
            ShowEditWindowCommand = new RelayCommand(ShowEditWindow, CanShowEditwindow);
            ShowFindWindowCommand = new RelayCommand(ShowFindWindow, CanShowFindWindow);
            DeleteCommand = new RelayCommand(DeleteCommandExecute, CanDeleteCommandExecute);
        }

        private bool CanShowFindWindow(object arg)
        {
            return true;
        }

        private void ShowFindWindow(object obj)
        {
            FindAnalog findAnalogWindow = new();
            findAnalogWindow.ShowDialog();
        }

        private bool CanShowAddWindow(object parameter)
        {
            return true;
        }

        private void ShowAddWindow(object parameter)
        {
            AddAnalog addAnalogWindow = new(AnalogModels);
            addAnalogWindow.ShowDialog();
        }
        private bool CanShowEditwindow(object parameter)
        {
            return true;
        }

        private void ShowEditWindow(object parameter)
        {
            if (parameter is AnalogModel analogModel)
            {
                EditAnalog editAnalogWindow = new(analogModel);
                editAnalogWindow.ShowDialog();
            }
        }

        private void GetAnalogModels()
        {
            try
            {
                using (var context = new AnalogContext())
                {
                    var models = context.AnalogModels.ToList();
                    foreach (var model in models)
                    {
                        AnalogModels.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool CanDeleteCommandExecute(object parameter)
        {
            return true;
        }


        private async void DeleteCommandExecute(object parameter)
        {
            if (parameter is AnalogModel analogModel)
            {
                using (var context = new AnalogContext())
                {
                    var existingAnalog = context.AnalogModels.Find(analogModel.Id);

                    if (existingAnalog != null)
                    {
                        context.AnalogModels.Remove(existingAnalog);
                        AnalogModels.Remove(analogModel);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

    }
}
