using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppAPICo.Command;

namespace WpfAppAPICo.ViewModels
{
    public class LoginViewModel : NotifyPropertyChange
    {
        private string _username;
        private string _password;
        private ICommand _logInCommand;
        private string errorMesage;

        private APIServices.APIHelper _apiHelper;

        public string ErrorMessage { get => errorMesage;
            set
            {
                errorMesage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                //_logInCommand = new LogInRelayCommand(LogInExecute, CanLogInExecute);

                _logInCommand = new LoginRelayCommandAsync(this, _apiHelper);
               
                return _logInCommand;

            }
            //set => _logInCommand = value; 
        }

        //private bool CanLogInExecute(object parameter)
        //{
        //    return _username?.Length > 0 && _password?.Length > 0;
        //}

        //private async Task LogInExecute(object parameter)
        //{
           
        //    try
        //    {
        //        ErrorMessage = "";
        //        var token = await _apiHelper.Authentication(Username, Password);

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //    }
            
        //    //if (Username == "Ok")
        //    //    ErrorMessage = "";
        //    //else
        //    //    ErrorMessage = $"Username: {Username}, Password: {Password}";


        //}

        public LoginViewModel()
        {
            _apiHelper = new APIServices.APIHelper();
        }

    }

    public class NotifyPropertyChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged (string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
