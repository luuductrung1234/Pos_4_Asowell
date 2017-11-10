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
        private Employee emp;
        internal EmployeeAddOrUpdateDialog empAddUptDialog;

        public EmployeeListPage(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();
            lvDataEmployee.ItemsSource = unitofork.EmployeeRepository.Get();
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            emp = lvDataEmployee.SelectedItem as Employee;
            txtID.Text = emp.EmpId;
            txtName.Text = emp.Name;
            txtBirth.SelectedDate = emp.Birth;
            txtAddress.Text = emp.Addr;
            txtHour_wage.Text = emp.HourWage.ToString();
            txtMail.Text = emp.Email;
            txtPhone.Text = emp.Phone;
            txtStartDay.Text = emp.Startday.ToString("yyyy-MM-dd");
            txtAcount.Text = emp.Username;
            txtPass.Text = emp.Pass;


        }

        private void bntAddnew_Click(object sender, RoutedEventArgs e)
        {
            empAddUptDialog = new EmployeeAddOrUpdateDialog(_unitofork);
            empAddUptDialog.ShowDialog();
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            empAddUptDialog = new EmployeeAddOrUpdateDialog(_unitofork, emp);
            empAddUptDialog.ShowDialog();
        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {
            var delEmp = _unitofork.EmployeeRepository.Get(em => em.EmpId.Equals(txtID.Text.Trim())).First();
            if(delEmp != null)
            {
                MessageBoxResult delMess = MessageBox.Show("Do you want to delete " + delEmp.Name + "(" + delEmp.Username + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
                if(delMess == MessageBoxResult.Yes)
                {
                    delEmp.Deleted = 1;
                    _unitofork.EmployeeRepository.Update(delEmp);
                    _unitofork.Save();
                }
            }
            else
            {
                MessageBox.Show("Please choose which employee you want to delete and try again!");
            }
        }
    }
}
