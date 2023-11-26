using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradeSoftCatalogTest.Core;
using TradeSoftCatalogTest.MVVM.Model;
using TradeSoftCatalogTest.MVVM.View;

namespace TradeSoftCatalogTest.MVVM.ViewModel
{
    /// <summary>
    /// Представление логики для окна EditAnalog
    /// </summary>
    public class EditAnalogViewModel
    {
        public ICommand EditAnalogCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public AnalogModel EditableAnalogModel { get; set; }
        public int Id { get; set; }
        public string Article1 { get; set; }
        public string Manufacturer1 { get; set; }
        public string Article2 { get; set; }
        public string Manufacturer2 { get; set; }
        public int TrustLevel { get; set; }

        public EditAnalogViewModel(AnalogModel editableAnalogModel)
        {
            EditAnalogCommand = new RelayCommand(EditCommandExecute, CanEditCommandExecute);
            CloseWindowCommand = new RelayCommand(CloseWindowExecute, CanCloseWindowExecute);

            EditableAnalogModel = editableAnalogModel;
            Id = editableAnalogModel.Id;
            Article1 = editableAnalogModel.Article1;
            Manufacturer1 = editableAnalogModel.Manufacturer1;
            Article2 = editableAnalogModel.Article2;
            Manufacturer2 = editableAnalogModel.Manufacturer2;
            TrustLevel = editableAnalogModel.TrustLevel;
        }

        private bool CanCloseWindowExecute(object parameter)
        {
            return true;
        }

        private void CloseWindowExecute(object parameter)
        {
            CloseWindow();
        }

        private bool CanEditCommandExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Изменяет данные в базе данных и коллекции в MainWindow на измененные
        /// </summary>
        /// <param name="parameter"></param>
        private async void EditCommandExecute(object parameter)
        {
            using (var context = new AnalogContext())
            {
                var existingAnalog = context.AnalogModels.Find(Id);

                if (existingAnalog != null)
                {
                    existingAnalog.Article1 = Article1;
                    existingAnalog.Manufacturer1 = Manufacturer1;
                    existingAnalog.Article2 = Article2;
                    existingAnalog.Manufacturer2 = Manufacturer2;
                    existingAnalog.TrustLevel = TrustLevel;

                    await context.SaveChangesAsync();
                }

                EditableAnalogModel.Id = Id;
                EditableAnalogModel.Article1 = Article1;
                EditableAnalogModel.Manufacturer1 = Manufacturer1;
                EditableAnalogModel.Article2 = Article2;
                EditableAnalogModel.Manufacturer2 = Manufacturer2;
                EditableAnalogModel.TrustLevel = TrustLevel;

                CloseWindow();
            }
        }
        private void CloseWindow()
        {
            var window = Application.Current.Windows.OfType<EditAnalog>().SingleOrDefault();

            if (window != null)
            {
                window.Close();
            }
        }

    }
}
