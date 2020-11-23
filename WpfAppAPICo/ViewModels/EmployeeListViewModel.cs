using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppAPICo.Command;
using WpfAppAPICo.Models;

namespace WpfAppAPICo.ViewModels
{
    public class EmployeeListViewModel: ObservableObject
    {
        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees { get => _employees;
            set { 
                _employees = value;
                OnPropertyChange(nameof(Employees));
            } 
        }

        public ICommand LoadEmployeesCommand { get; set; }

        public ICommand SelectedEmployeeCommand { get; set; }

        public EmployeeListViewModel()
        {
            LoadEmployeesCommand = new LoadEmployeesCommand(this, new APIServices.APIHelper());
            SelectedEmployeeCommand = new SelectedEmployeeCommand();
            //LoadEmployeesCommand.Execute(null);
        }
    }
}
