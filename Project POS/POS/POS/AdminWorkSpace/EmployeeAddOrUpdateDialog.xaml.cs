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
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeAddOrUpdateDialog.xaml
    /// </summary>
    public partial class EmployeeAddOrUpdateDialog : Window
    {
        private AdminwsOfAsowell _unitofork;
        private Employee _emp;

        public EmployeeAddOrUpdateDialog(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        public EmployeeAddOrUpdateDialog(AdminwsOfAsowell unitofork, Employee emp)
        {
            _emp = emp;
            _unitofork = unitofork;
            InitializeComponent();
            initUptData();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initUptData()
        {
            if(_emp != null)
            {
                loadControl(false);
                txtUsername.Text = _emp.Username;
                txtPass.Password = _emp.Pass;
                txtCon.Password = _emp.Pass;
                txtName.Text = _emp.Name;
                txtBirth.SelectedDate = _emp.Birth;
                txtAddress.Text = _emp.Addr;
                txtPhone.Text = _emp.Phone;
                txtMail.Text = _emp.Email;
                txtStartDay.SelectedDate = _emp.Startday;
                txtHour_wage.Text = _emp.HourWage.ToString();
                txtCon.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loadControl(bool b)
        {
            //txtUsername.IsEnabled = b;
            //txtPass.IsEnabled = b;
            //txtCon.IsEnabled = b;
        }
    }
}
