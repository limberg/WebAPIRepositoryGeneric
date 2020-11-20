using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppAPICo.APIServices;
using WpfAppAPICo.ViewModels;
using WpfAppAPICo.Models;

namespace WpfAppAPICo.Command
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isExecuting;

        public bool IsExecuting {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
                
            }
        }

        public event EventHandler CanExecuteChanged;
        //{
        //    add
        //    {
        //        CommandManager.RequerySuggested += value;
        //    }

        //     remove
        //    {
        //        CommandManager.RequerySuggested -= value;
        //    }
        //}

        public bool CanExecute(object parameter)
        {
            return CanExecuteLogin(parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        protected abstract Task ExecuteAsync(object parameter);

        protected abstract bool CanExecuteLogin(object parameter);
    }

    public class LoginRelayCommandAsync : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly APIServices.APIHelper _apiHelper;

        public LoginRelayCommandAsync(LoginViewModel loginViewModel, APIHelper apiHelper)
        {
            _loginViewModel = loginViewModel;
            _apiHelper = apiHelper;
        }

        protected override bool CanExecuteLogin(object parameter)
        {
            //return _loginViewModel.Username?.Length > 0 && _loginViewModel.Password?.Length > 0;
            return !IsExecuting;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IsExecuting = true;

                _loginViewModel.ErrorMessage = "";
                Token token = await _apiHelper.Authentication(_loginViewModel.Username, _loginViewModel.Password);

                Console.WriteLine($"Token: {token.access_token}");

                
            }
            catch(Exception ex)
            {
                _loginViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                IsExecuting = false;
               
            }
        }
    }
}
