﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfAppAPICo.APIServices;
using WpfAppAPICo.Models;
using WpfAppAPICo.ViewModels;

namespace WpfAppAPICo.Command
{
    public class LoadEmployeesCommand : ICommand
    {
        private readonly EmployeeListViewModel _employeeListViewModel;
        private IAPIHelper _apiHelper;

        public LoadEmployeesCommand(EmployeeListViewModel employeeListViewModel, IAPIHelper apiHelper)
        {
            _employeeListViewModel = employeeListViewModel;
            _apiHelper = apiHelper;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                IEnumerable<Employee> employees = await _apiHelper.GetAllEmployees();

                _employeeListViewModel.Employees = new System.Collections.ObjectModel.ObservableCollection<Employee>(employees);
            }
            catch (Exception ex)
            {
                _employeeListViewModel.Display = System.Windows.Visibility.Visible;
                _employeeListViewModel.MessageBrush = Brushes.Red;
                _employeeListViewModel.Message = ex.Message;
            }
            
        }

        //public void Execute(object parameter)
        //{
        //    IEnumerable<Employee> employees = null;

        //    _apiHelper.GetAllEmployees().ContinueWith(task =>
        //    {
        //        if (task.Exception == null)
        //            employees = task.Result;
        //    });

        //    _employeeListViewModel.Employees = new System.Collections.ObjectModel.ObservableCollection<Employee>(employees);
        //}
    }
}
