using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfAppAPICo.APIServices;
using WpfAppAPICo.Command;
using WpfAppAPICo.Models;

namespace WpfAppAPICo.ViewModels
{
    public class EmployeeListViewModel : ObservableObject
    {
        private ObservableCollection<Employee> _employees;
        private ICommand relayAddCommand;

        private readonly IAPIHelper apiHelper;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChange(nameof(Employees));
            }
        }

        public ICommand LoadEmployeesCommand { get; set; }

        public ICommand SelectedEmployeeCommand { get; set; }

        public ICommand RelayAddCommand { get => relayAddCommand; set => relayAddCommand = value; }

        private Employee _currentEmployee;

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value;
                OnPropertyChange("CurrentEmployee");
            }
        }

        private string  _message;
        public string  Message
        {
            get { return _message; }
            set { _message = value;
                OnPropertyChange("Message");
            }
        }

        private Brush _messageBrush;
        public Brush MessageBrush
        {
            get { return _messageBrush; }
            set { _messageBrush = value;
                OnPropertyChange("MessageBrush");
            }
        }


        private Visibility _display;
        public Visibility Display
        {
            get { return _display; }
            set { _display = value;
                OnPropertyChange("Display");
            }
        }

        private async void Add()
        {
            try
            {
                Employee employeeRes = await apiHelper.AddEmployee(CurrentEmployee);

                CurrentEmployee = new Employee();

                this.Employees.Clear();
                LoadEmployeesCommand.Execute(null);

                MessageBrush = Brushes.Blue;
                Display = Visibility.Visible;

                Message = $"Der Angestellte Nro:{employeeRes.EmployeeID} wurde erfolgreich angelegt!";
            
            }
            catch(Exception ex)
            {
                Display = Visibility.Visible;
                MessageBrush = Brushes.Red;
                Message = $"Fehler: {ex.Message}";
            }
        }

        

        public EmployeeListViewModel()
        {
            this.apiHelper = new APIHelper();
            LoadEmployeesCommand = new LoadEmployeesCommand(this, apiHelper);
            //SelectedEmployeeCommand = new SelectedEmployeeCommand();
            RelayAddCommand = new RelayCommand(Add);

            Display = Visibility.Collapsed;
            CurrentEmployee = new Employee();
        }
    }
}
