using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TradeSoftCatalogTest.MVVM.Model
{
    /// <summary>
    /// Представление аналога в базе данных
    /// </summary>
    public class AnalogModel : INotifyPropertyChanged
    {
        private int _id;
        private string _article1;
        private string _manufacturer1;
        private string _article2;
        private string _manufacturer2;
        private int _trustLevel;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Article1
        {
            get => _article1;
            set => SetProperty(ref _article1, value);
        }

        public string Manufacturer1
        {
            get => _manufacturer1;
            set => SetProperty(ref _manufacturer1, value);
        }

        public string Article2
        {
            get => _article2;
            set => SetProperty(ref _article2, value);
        }

        public string Manufacturer2
        {
            get => _manufacturer2;
            set => SetProperty(ref _manufacturer2, value);
        }

        public int TrustLevel
        {
            get => _trustLevel;
            set => SetProperty(ref _trustLevel, value);
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
