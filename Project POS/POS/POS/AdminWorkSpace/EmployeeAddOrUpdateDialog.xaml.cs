using POS.Entities;
using POS.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private AdminwsOfCloudAsowell _unitofork;
        private Employee _emp;

        public EmployeeAddOrUpdateDialog(AdminwsOfCloudAsowell unitofork)
        {
            _unitofork = unitofork;
            _emp = new Employee();
            InitializeComponent();
            initControlAdd();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        public EmployeeAddOrUpdateDialog(AdminwsOfCloudAsowell unitofork, Employee emp)
        {
            _unitofork = unitofork;
            _emp = emp;
            InitializeComponent();
            txbCon.Visibility = Visibility.Collapsed;
            txtCon.Visibility = Visibility.Collapsed;
            initUptData();
            initControlAdd();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initControlAdd()
        {
            txtBirth.DisplayDateEnd = new DateTime((DateTime.Now.Year - 16), 12, 31);
            txtStartDay.DisplayDateStart = DateTime.Now;

            List<dynamic> roleList = new List<dynamic>
            {
                new { role = 1, roleDisplay = "Bar"},
                new { role = 2, roleDisplay = "Kitchen"}
            };
            cboRole.ItemsSource = roleList;
            cboRole.SelectedValuePath = "role";
            cboRole.DisplayMemberPath = "roleDisplay";
        }

        private void initUptData()
        {
            if (_emp != null)
            {
                loadControl(false);
                txtUsername.Text = _emp.Username;
                txtPass.Password = _emp.Pass;
                txtCon.Password = _emp.Pass;
                txtName.Text = _emp.Name;
                cboRole.SelectedValue = _emp.EmpRole;
                txtBirth.SelectedDate = _emp.Birth;
                txtAddress.Text = _emp.Addr;
                txtPhone.Text = _emp.Phone;
                txtMail.Text = _emp.Email;
                txtStartDay.SelectedDate = _emp.Startday;
                txtHour_wage.Text = _emp.HourWage.ToString();
                return;
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Password.Trim();
            if (_emp.EmpId == null)
            {
                //check username
                if (username.Length == 0 || username.Length > 50)
                {
                    MessageBox.Show("Username is not valid!");
                    txtUsername.Focus();
                    return;
                }

                var newemp = _unitofork.EmployeeRepository.Get(x => x.Username.Equals(username)).ToList();

                if (newemp.Count != 0)
                {
                    MessageBox.Show("Username is already exist! Please try again!");
                    txtUsername.Focus();
                    return;
                }

                //check pass
                if (pass.Length == 0 || pass.Length > 50)
                {
                    MessageBox.Show("Password is not valid!");
                    txtPass.Focus();
                    return;
                }

                string passcon = txtCon.Password.Trim();
                if (!passcon.Equals(pass))
                {
                    MessageBox.Show("Confirm password is not match!");
                    txtCon.Focus();
                    return;
                }
            }

            //check name
            string namee = txtName.Text.Trim();
            if (namee.Length == 0 || namee.Length > 50)
            {
                MessageBox.Show("Name is not valid!");
                txtName.Focus();
                return;
            }

            //check role
            int role = 0;

            if(cboRole.SelectedValue == null)
            {
                MessageBox.Show("Role must be selected!");
                return;
            }

            role = (int)cboRole.SelectedValue;

            if (role ==  0)
            {
                MessageBox.Show("Role must be selected!");
                return;
            }

            //check birth
            if(txtBirth.SelectedDate == null)
            {
                MessageBox.Show("Birth must be selected!");
                return;
            }

            DateTime birth = txtBirth.SelectedDate.Value;

            //check address
            string addr = txtAddress.Text.Trim();
            if (addr.Length == 0 || addr.Length > 200)
            {
                MessageBox.Show("Address is not valid!");
                txtAddress.Focus();
                return;
            }

            //check phone
            string phone = txtPhone.Text.Trim();
            if (phone.Length == 0 || phone.Length > 20)
            {
                MessageBox.Show("Phone is not valid!");
                txtPhone.Focus();
                return;
            }

            //check email
            string email = txtMail.Text.Trim();
            if(!Regex.IsMatch(email, "[\\w\\d]+[@][\\w]+[.][\\w]+"))
            {
                MessageBox.Show("Email is not valid!");
                txtMail.Focus();
                return;
            }

            //check start day
            if (txtStartDay.SelectedDate == null)
            {
                MessageBox.Show("Start Day must be selected!");
                return;
            }

            DateTime start = txtStartDay.SelectedDate.Value;

            //check hour wage
            int hourwage = int.Parse(txtHour_wage.Text.Trim());
            if(hourwage <= 0 || hourwage >= int.MaxValue)
            {
                MessageBox.Show("Hour wage is not valid! Hour wage must be greater than 0 and lesser than " + int.MaxValue);
                txtHour_wage.Focus();
                return;
            }

            if(_emp.EmpId == null)
            {
                Employee checkemp = new Employee()
                {
                    EmpId = "",
                    Username = username,
                    Pass = pass,
                    Name = namee,
                    EmpRole = role,
                    Birth = birth,
                    Addr = addr,
                    Phone = phone,
                    Email = email,
                    Startday = start,
                    HourWage = hourwage,
                    Manager = (App.Current.Properties["AdLogin"] as AdminRe).AdId
                };

                _unitofork.EmployeeRepository.Insert(checkemp);
                _unitofork.Save();

                MessageBox.Show("Insert " + checkemp.Name + "(" + checkemp.EmpId + ") successful!");
                this.Close();
            }
            else
            {
                _emp.Name = namee;
                _emp.EmpRole = role;
                _emp.Birth = birth;
                _emp.Addr = addr;
                _emp.Phone = phone;
                _emp.Email = email;
                _emp.Startday = start;
                _emp.HourWage = hourwage;

                _unitofork.EmployeeRepository.Update(_emp);
                _unitofork.Save();

                MessageBox.Show("Update " + _emp.Name + "(" + _emp.EmpId + ") successful!");
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loadControl(bool b)
        {
            txtUsername.IsEnabled = b;
            txtPass.IsEnabled = b;
            txtCon.IsEnabled = b;
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }
    }
}
