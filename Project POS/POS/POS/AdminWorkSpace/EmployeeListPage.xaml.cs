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
        private AdminwsOfCloudPOS _unitofork;
        private AdminRe admin;
        private Employee emp;
        private List<Employee> empwithad;
        internal EmployeeAddOrUpdateDialog empAddUptDialog;

        public EmployeeListPage(AdminwsOfCloudPOS unitofork, AdminRe ad)
        {
            _unitofork = unitofork;
            InitializeComponent();
            admin = ad;

            this.Loaded += EmployeeListPage_Loaded;
        }

        private void EmployeeListPage_Loaded(object sender, RoutedEventArgs e)
        {
            empwithad = _unitofork.EmployeeRepository.Get(x => x.Manager.Equals(admin.AdId) && x.Deleted.Equals(0)).ToList();
            lvDataEmployee.ItemsSource = empwithad;

            txtBirth.DisplayDateEnd = new DateTime((DateTime.Now.Year - 16), 12, 31);
            txtStart.DisplayDateStart = DateTime.Now;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            emp = lvDataEmployee.SelectedItem as Employee;
            if (emp == null)
            {
                txtID.Text = "";
                txtName.Text = "";
                txtBirth.SelectedDate = new DateTime(1990, 1, 1);
                txtAddress.Text = "";
                txtHour_wage.Text = "";
                txtMail.Text = "";
                txtPhone.Text = "";
                txtStart.SelectedDate = DateTime.Now;
                txtAcount.Text = "";
                txtPass.Password = "";
                return;
            }
            txtID.Text = emp.EmpId;
            txtName.Text = emp.Name;
            txtBirth.SelectedDate = emp.Birth;
            txtAddress.Text = emp.Addr;
            txtHour_wage.Text = emp.HourWage.ToString();
            txtMail.Text = emp.Email;
            txtPhone.Text = emp.Phone;
            txtStart.SelectedDate = emp.Startday;
            txtAcount.Text = emp.Username;
            txtPass.Password = emp.DecryptedPass;
            switch (emp.EmpRole)
            {
                case (int) EmployeeRole.Ministering:
                {
                    txtRole.Text = EmployeeRole.Ministering.ToString();
                    break;
                }
                case (int)EmployeeRole.Bar:
                {
                    txtRole.Text = EmployeeRole.Bar.ToString();
                    break;
                }
                case (int)EmployeeRole.Kitchen:
                {
                    txtRole.Text = EmployeeRole.Kitchen.ToString();
                    break;
                }
                case (int)EmployeeRole.Stock:
                {
                    txtRole.Text = EmployeeRole.Stock.ToString();
                    break;
                }
            }
        }

        private void bntAddnew_Click(object sender, RoutedEventArgs e)
        {
            empAddUptDialog = new EmployeeAddOrUpdateDialog(_unitofork);
            empAddUptDialog.ShowDialog();

            empwithad = _unitofork.EmployeeRepository.Get(x => x.Manager.Equals(admin.AdId) && x.Deleted.Equals(0)).ToList();
            lvDataEmployee.ItemsSource = empwithad;
            lvDataEmployee.UnselectAll();
            lvDataEmployee.Items.Refresh();
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(lvDataEmployee.SelectedItem == null)
            {
                MessageBox.Show("Employee must be selected to update! Choose again!");
                return;
            }

            empAddUptDialog = new EmployeeAddOrUpdateDialog(_unitofork, emp);
            empAddUptDialog.ShowDialog();
            lvDataEmployee.UnselectAll();
            lvDataEmployee.Items.Refresh();
        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {
            if(lvDataEmployee.SelectedItem == null)
            {
                MessageBox.Show("Employee must be selected to delete! Choose again!");
                return;
            }

            var delEmp = lvDataEmployee.SelectedItem as Employee;
            if (delEmp != null)
            {
                MessageBoxResult delMess = MessageBox.Show("Do you want to delete " + delEmp.Name + "(" + delEmp.Username + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
                if(delMess == MessageBoxResult.Yes)
                {
                    delEmp.Deleted = 1;
                    _unitofork.EmployeeRepository.Update(delEmp);
                    _unitofork.Save();
                    empwithad = _unitofork.EmployeeRepository.Get(x => x.Manager.Equals(admin.AdId) && x.Deleted.Equals(0)).ToList();
                    lvDataEmployee.ItemsSource = empwithad;
                    lvDataEmployee.UnselectAll();
                    lvDataEmployee.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please choose employee you want to delete and try again!");
            }
        }
    }
}
