using POS.Repository.DAL;
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
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminwsOfAsowell _unitofork;
        EmployeeListPage empListPage;


        public AdminWindow()
        {
            InitializeComponent();

            _unitofork = new AdminwsOfAsowell();
            empListPage = new EmployeeListPage(_unitofork);

            Closing += AdminWindow_Closing;
        }

        private void AdminWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _unitofork.Dispose();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(empListPage);
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {

        }
    }
}
