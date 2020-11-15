using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppAPICo.APIServices;
using WpfAppAPICo.Models;

namespace WpfAppAPICo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        APIHelper apiHelper;
        public MainWindow()
        {
            InitializeComponent();
            apiHelper = new APIHelper();
        }

        private async Task LoadEmployees()
        {
            List<Employee> employees = await apiHelper.GetAllEmployees();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadEmployees();
        
        }
    }
}
