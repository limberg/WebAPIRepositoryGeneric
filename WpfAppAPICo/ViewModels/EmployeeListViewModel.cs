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

        private Employee _currentEmployee;

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value;
                OnPropertyChange("CurrentEmployee");
            }
        }

        #region Message
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

        #endregion

        #region Add
        private ICommand relayAddCommand;
        public ICommand RelayAddCommand { get => relayAddCommand; set => relayAddCommand = value; }
        private async void Add()
        {
            try
            {
                Employee employeeRes = await apiHelper.AddEmployee(CurrentEmployee);

                CurrentEmployee = new Employee();

                //this.Employees.Clear();
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

        #endregion

        #region Search

        private ICommand _relaySearchcommand;

        public ICommand RelaySearchCommand
        {
            get { return _relaySearchcommand; }
            set { _relaySearchcommand = value; }
        }

        private async void Search()
        {
            try
            {
                Employee employee = await apiHelper.GetEmployeeById(CurrentEmployee.EmployeeID);

                CurrentEmployee = employee;

            }
            catch (Exception ex)
            {
                Display = Visibility.Visible;
                MessageBrush = Brushes.Red;
                Message = $"{ex.Message}";

            }
        }

        #endregion

        #region Update

        private ICommand _relayUpdateCommand;

        public ICommand RelayUpdateCommand
        {
            get { return _relayUpdateCommand; }
            set { _relayUpdateCommand = value; }
        }

        private async void Update()
        {
            try
            {
                var IsUpdate  =await apiHelper.UpdateEmployee(CurrentEmployee.EmployeeID, CurrentEmployee);

                //_employees.Clear();
                if (IsUpdate)
                {
                    LoadEmployeesCommand.Execute(null);

                    Display = Visibility.Visible;
                    MessageBrush = Brushes.Blue;
                    Message = $"Der Angestellte {CurrentEmployee.EmployeeID} wurde aktualisiert!";
                }
            }
            catch (Exception ex)
            {
                Display = Visibility.Visible;
                MessageBrush = Brushes.Red;
                Message = $"{ex.Message}";

            }
        }


        #endregion

        #region DeleteEmployee

        private ICommand _relayDeleteCommand;

        public ICommand RelayDeleteCommand
        {
            get { return _relayDeleteCommand; }
            set { _relayDeleteCommand = value; }
        }

        private async void Delete()
        {
            try
            {

                int id = CurrentEmployee.EmployeeID;
                var IsDelete = await apiHelper.DeleteEmployee(CurrentEmployee.EmployeeID);

                //_employees.Clear();
                if (IsDelete)
                {

                    CurrentEmployee = new Employee();

                    LoadEmployeesCommand.Execute(null);
                   

                    Display = Visibility.Visible;
                    MessageBrush = Brushes.Blue;
                    Message = $"Der Angestellte {id} wurde gelöscht!";
                }
            }
            catch (Exception ex)
            {
                Display = Visibility.Visible;
                MessageBrush = Brushes.Red;
                Message = $"{ex.Message}";

            }
        }


        #endregion

        public EmployeeListViewModel()
        {
            this.apiHelper = new APIHelper();
            LoadEmployeesCommand = new LoadEmployeesCommand(this, apiHelper);
            //SelectedEmployeeCommand = new SelectedEmployeeCommand();
            RelayAddCommand = new RelayCommand(Add);
            RelaySearchCommand = new RelayCommand(Search);
            RelayUpdateCommand = new RelayCommand(Update);
            RelayDeleteCommand = new RelayCommand(Delete);

            Message = "";
            Display = Visibility.Collapsed;
            CurrentEmployee = new Employee();
        }
    }
}
