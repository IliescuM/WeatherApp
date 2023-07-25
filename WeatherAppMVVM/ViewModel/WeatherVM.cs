using System.Collections.ObjectModel;
using WeatherAppMVVM.Model;
using WeatherAppMVVM.ViewModel.Commands;
using WeatherAppMVVM.ViewModel.Helpers;

namespace WeatherAppMVVM.ViewModel
{
    using System.ComponentModel;

    public class WeatherVM : INotifyPropertyChanged
    {
        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged("Query");
            }
        }
        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions _currentConditions;

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set
            {
                _currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {

                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // triggering the event 
        }

    }
}
