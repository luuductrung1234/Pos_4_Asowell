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
        private EmployeewsOfAsowell _unitofwork;
        EmployeeListPage empListPage;
        SalaryPage salarypage;
        ProductDetailPage productdetals;
        internal Login login;


        public AdminWindow()
        {
            InitializeComponent();

            _unitofork = new AdminwsOfAsowell();
            _unitofwork = new EmployeewsOfAsowell();
            empListPage = new EmployeeListPage(_unitofork);
            salarypage = new SalaryPage(_unitofork);
            productdetals = new ProductDetailPage( _unitofork);

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
            login = new Login();
            this.Close();
            login.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(empListPage);
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(salarypage);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(productdetals);
        }
    }
}
