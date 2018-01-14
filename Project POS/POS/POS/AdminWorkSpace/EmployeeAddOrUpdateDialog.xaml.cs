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
        private readonly AdminwsOfCloudPOS _unitofwork;
        private Employee _emp;

        public EmployeeAddOrUpdateDialog(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            _emp = new Employee();
            InitializeComponent();
            initControlAdd();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        public EmployeeAddOrUpdateDialog(AdminwsOfCloudPOS unitofwork, Employee emp)
        {
            _unitofwork = unitofwork;
            _emp = emp;
            InitializeComponent();
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
                new { role = EmployeeRole.Ministering, roleDisplay = "Ministering"},
                new { role = EmployeeRole.Cashier, roleDisplay = "Cashier"},
                new { role = EmployeeRole.Bar, roleDisplay = "Bar"},
                new { role = EmployeeRole.Kitchen, roleDisplay = "Kitchen"},
                new { role = EmployeeRole.Stock, roleDisplay = "Stock"},
            };
            cboRole.ItemsSource = roleList;
            cboRole.SelectedValuePath = "role";
            cboRole.DisplayMemberPath = "roleDisplay";
        }

        private void initUptData()
        {
            if (_emp != null)
            {
                txtUsername.Text = _emp.Username;
                txtPass.Password = _emp.DecryptedPass;
                txtCon.Password = _emp.DecryptedPass;
                txtName.Text = _emp.Name;
                cboRole.SelectedValue = _emp.EmpRole;
                txtBirth.SelectedDate = _emp.Birth;
                txtAddress.Text = _emp.Addr;
                txtPhone.Text = _emp.Phone;
                txtMail.Text = _emp.Email;
                txtStartDay.SelectedDate = _emp.Startday;
                txtHour_wage.Text = _emp.HourWage.ToString();
                txtCode.Password = _emp.DecryptedCode;
                return;
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string pass = txtPass.Password.Trim();
                //check username
                if (username.Length == 0 || username.Length > 50)
                {
                    MessageBox.Show("Username is not valid!");
                    txtUsername.Focus();
                    return;
                }

                if (_emp == null)
                {
                    var newemp = _unitofwork.EmployeeRepository.Get(x => x.Username.Equals(username)).ToList();

                    if (newemp.Count != 0)
                    {
                        MessageBox.Show("Username is already exist! Please try again!");
                        txtUsername.Focus();
                        return;
                    }
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

                if (cboRole.SelectedValue == null)
                {
                    MessageBox.Show("Role must be selected!");
                    return;
                }

                role = (int)cboRole.SelectedValue;



                //check birth
                if (txtBirth.SelectedDate == null)
                {
                    MessageBox.Show("Birth must be selected!");
                    return;
                }
                DateTime birth = txtBirth.SelectedDate.Value;
                if (DateTime.Now.Year - birth.Year < 17)
                {
                    MessageBox.Show("Employee's age must higher than 17!");
                    return;
                }


                //check address
                string addr = txtAddress.Text;
                if (addr.Length > 200)
                {
                    MessageBox.Show("Address is not valid!");
                    txtAddress.Focus();
                    return;
                }

                //check phone
                string phone = txtPhone.Text;
                if (phone.Length > 20)
                {
                    MessageBox.Show("Phone is not valid!");
                    txtPhone.Focus();
                    return;
                }

                //check email
                string email = txtMail.Text;
                if (!Regex.IsMatch(email, "[\\w\\d]+[@][\\w]+[.][\\w]+"))
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
                if (hourwage <= 0 || hourwage >= int.MaxValue)
                {
                    MessageBox.Show("Hour wage is not valid! Hour wage must be greater than 0 and lesser than " +
                                    int.MaxValue);
                    txtHour_wage.Focus();
                    return;
                }

                //check code
                string code = txtCode.Password.Trim();
                if (code.Length < 4)
                {
                    MessageBox.Show("Employee code should be stronger!");
                    txtCode.Focus();
                    return;
                }

                // Adding
                if (_emp.EmpId == null)
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
                        EmpCode = code,
                        Deleted = 0,
                        Manager = (App.Current.Properties["AdLogin"] as AdminRe).AdId
                    };

                    _unitofwork.EmployeeRepository.Insert(checkemp);
                    _unitofwork.Save();

                    MessageBox.Show("Insert " + checkemp.Name + "(" + checkemp.EmpId + ") successful!");
                    this.Close();
                }
                else //Updating
                {
                    _emp.Username = username;
                    _emp.Pass = pass;
                    _emp.Name = namee;
                    _emp.EmpRole = role;
                    _emp.Birth = birth;
                    _emp.Addr = addr;
                    _emp.Phone = phone;
                    _emp.Email = email;
                    _emp.Startday = start;
                    _emp.HourWage = hourwage;
                    _emp.EmpCode = code;

                    _unitofwork.EmployeeRepository.Update(_emp);
                    _unitofwork.Save();

                    MessageBox.Show("Update " + _emp.Name + "(" + _emp.EmpId + ") successful!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. Can not add or update employee. Please check the details again!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }


        private void TxtUsername_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //check username
            if (string.IsNullOrEmpty(txtUsername.Text) || txtUsername.Text.Trim().Length > 50)
            {
                IcUserNameValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcUserNameValid.Visibility = Visibility.Hidden;
            }

        }

        private void TxtPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            //check pass
            if (string.IsNullOrEmpty(txtPass.Password) || txtPass.Password.Trim().Length > 50)
            {
                IcPassValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcPassValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtCon_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCon.Password))
            {
                IcConfirmValid.Visibility = Visibility.Visible;
                return;
            }

            string passcon = txtCon.Password.Trim();
            if (!passcon.Equals(txtPass.Password.Trim()))
            {
                IcConfirmValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcConfirmValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //check name
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Length > 50)
            {
                IcNameValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcNameValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtBirth_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var birth = ((DatePicker)sender).SelectedDate.Value.Date;
            if (DateTime.Now.Year - birth.Year < 17)
            {
                IcBirthValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcBirthValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtAddress_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //check address
            if (txtAddress.Text.Length > 200)
            {
                IcAddrValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcAddrValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtPhone_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //check phone
            if (txtPhone.Text.Length > 20)
            {
                IcPhoneValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcPhoneValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtMail_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //check email
            if (!Regex.IsMatch(txtMail.Text, "[\\w\\d]+[@][\\w]+[.][\\w]+"))
            {
                IcMailValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcMailValid.Visibility = Visibility.Hidden;
            }
        }

        private void TxtCode_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Password))
            {
                IcCodeValid.Visibility = Visibility.Visible;
                return;
            }

            //check code
            string code = txtCode.Password.Trim();
            if (code.Length < 4)
            {
                IcCodeValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcCodeValid.Visibility = Visibility.Hidden;
            }
        }
        
    }
}
