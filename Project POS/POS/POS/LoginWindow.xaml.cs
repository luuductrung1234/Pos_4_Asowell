using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.Interfaces;

namespace POS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IEmployeeRepository _employeeRepository;

        public Login()
        {
            _employeeRepository = new EmployeeRepository(new AsowellContext());
            InitializeComponent();

            this.WindowState = WindowState.Normal;

            this.Closing += Closing_LoginWindos;
        }

        public Login(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            InitializeComponent();

            this.WindowState = WindowState.Normal;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string pass = txtPass.Password;

            bool isFound = false;
            List<Employee> empList = _employeeRepository.GetAllEmployees().ToList();
            foreach (Employee emp in empList)
            {
                if (emp.Username.Equals(username) && emp.Pass.Equals(pass))
                {
                    App.Current.Properties["EmpLogin"] = emp;

                    EmployeeWorkSpace.MainWindow main = new EmployeeWorkSpace.MainWindow();
                    main.Show();

                    isFound = true;
                    break;
                }

            }

            if(!isFound)
            {
                MessageBox.Show("incorrect username or password");
                return;
            }
            this.Close();
        }

        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Closing_LoginWindos(object sender, EventArgs args)
        {
            _employeeRepository.Dispose();
        }
    }
}
