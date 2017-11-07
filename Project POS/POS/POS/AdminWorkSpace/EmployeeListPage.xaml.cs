using POS.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    
    /// <summary>
    /// Interaction logic for EmployeeListPage.xaml
    /// </summary>
    public partial class EmployeeListPage : Page
    {
        private AdminwsOfAsowell _unitofork;
        public EmployeeListPage(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();
            lvDataEmployee.ItemsSource = unitofork.EmployeeRepository.Get();
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee emp = lvDataEmployee.SelectedItem as Employee;
            txtID.Text = emp.EmpId;
            txtName.Text = emp.Name;
            txtBirth.SelectedDate = emp.Birth;
            txtAddress.Text = emp.Addr;
            txtHour_wage.Text = emp.HourWage.ToString();
            txtMail.Text = emp.Email;
            txtPhone.Text = emp.Phone;
            txtStartDay.Text = emp.Startday.ToString("dd/MM/yyy");
            txtAcount.Text = emp.Username;
            txtPass.Text = emp.Pass;


        }
    }
}
