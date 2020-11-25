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
using WpfAppAPICo.Command;
using WpfAppAPICo.ViewModels;

namespace WpfAppAPICo.Views
{
    /// <summary>
    /// Interaction logic for EmployeeListUC.xaml
    /// </summary>
    public partial class EmployeeListUC : UserControl
    {


        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register("LoadCommand", typeof(ICommand), typeof(EmployeeListUC), new PropertyMetadata(null));




        public ICommand SelectedEmployeeChangedCommand
        {
            get { return (ICommand)GetValue(SelectedEmployeeChangedCommandProperty); }
            set { SetValue(SelectedEmployeeChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemChangeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEmployeeChangedCommandProperty =
            DependencyProperty.Register("SelectedEmployeeChangedCommand", typeof(ICommand), typeof(EmployeeListUC), new PropertyMetadata(null));



        public EmployeeListUC()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoadCommand != null)
                {
                    LoadCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // MessageBox.Show("Seletion Changed");
           if (SelectedEmployeeChangedCommand != null)
                SelectedEmployeeChangedCommand.Execute(lvEmployees.SelectedItem);

        }
    }
}
