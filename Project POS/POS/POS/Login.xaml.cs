using POS.Model;
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

namespace POS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            this.WindowState = WindowState.Normal;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string pass = txtPass.Password;

            List<Employee> empList = EmployeeData.EmpList;
            foreach (Employee emp in empList)
            {
                if (emp.Username.Equals(username) && emp.Pass.Equals(pass))
                {
                    App.Current.Properties["EmpLogin"] = emp;

                    MainWindow main = new MainWindow();
                    main.Show();

                    break;
                }
            }

            this.Close();
        }

        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
