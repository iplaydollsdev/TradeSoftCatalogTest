using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSoftCatalogTest.MVVM.Model;

namespace TradeSoftCatalogTest.MVVM.ViewModel
{
    /// <summary>
    /// Представление логики для окна Route
    /// </summary>
    public class RouteViewModel : INotifyPropertyChanged
    {
        public List<AnalogRoute> Routes { get; set; } = new();
        public List<AnalogChain> Chains { get; set; } = new();

        private AnalogRoute _selectedRoute;
        public AnalogRoute SelectedRoute
        {
            get { return _selectedRoute; }
            set
            {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));

                Chains = _selectedRoute?.Chains ?? new List<AnalogChain>();
                OnPropertyChanged(nameof(Chains));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public RouteViewModel(List<AnalogRoute> routes)
        {
            Routes = routes;
            if (routes.Count > 0)
            {
                SelectedRoute = routes[0];
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
