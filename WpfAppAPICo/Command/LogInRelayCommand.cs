using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppAPICo.Command
{
    public class LogInRelayCommand : ICommand
    {

        //private Task<Action<object>> _execute;

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;


        public LogInRelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {

            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
          this._execute(parameter);
        }

        //protected abstract Task ExecuteAsync(object parameter);


    }
}
