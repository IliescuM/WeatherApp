using System;
using System.Windows.Input;

namespace WeatherAppMVVM.ViewModel.Commands
{


    public class SearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            var query = parameter as string;
            return !string.IsNullOrWhiteSpace(query);
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }

    }
}
