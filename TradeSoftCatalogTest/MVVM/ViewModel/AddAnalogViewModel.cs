using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using TradeSoftCatalogTest.Core;
using TradeSoftCatalogTest.MVVM.Model;
using TradeSoftCatalogTest.MVVM.View;

namespace TradeSoftCatalogTest.MVVM.ViewModel
{
    public class AddAnalogViewModel
    {
        public ICommand AddAnalogCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ObservableCollection<AnalogModel> AnalogModelsToUpdate { get; set; }

        public string Article1 {  get; set; }
        public string Manufacturer1 { get; set; }
        public string Article2 { get; set; }
        public string Manufacturer2 { get; set; }
        public int TrustLevel {  get; set; }

        public AddAnalogViewModel(ObservableCollection<AnalogModel> analogModels)
        {
            AddAnalogCommand = new RelayCommand(AddCommandExecute, CanAddCommandExecute);
            CloseWindowCommand = new RelayCommand(CloseWindowExecute, CanCloseExecute);
            AnalogModelsToUpdate = analogModels;
        }

        private bool CanCloseExecute(object parameter)
        {
            return true;
        }

        private void CloseWindowExecute(object parameter)
        {
            CloseWindow();
        }

        private bool CanAddCommandExecute(object parameter)
        {
            return true;
        }
        private async void AddCommandExecute(object parameter)
        {
            using (var context = new AnalogContext())
            {
                var analogModel = new AnalogModel() {
                    Article1 = this.Article1, 
                    Manufacturer1 = this.Manufacturer1, 
                    Article2 = this.Article2, 
                    Manufacturer2 = this.Manufacturer2, 
                    TrustLevel = this.TrustLevel
                };

                context.AnalogModels.Add(analogModel);
                AnalogModelsToUpdate.Add(analogModel);
                await context.SaveChangesAsync();
            }

            CloseWindow();
        }
        private void CloseWindow()
        {
            var window = Application.Current.Windows.OfType<AddAnalog>().SingleOrDefault();

            if (window != null)
            {
                window.Close();
            }
        }

    }
}
