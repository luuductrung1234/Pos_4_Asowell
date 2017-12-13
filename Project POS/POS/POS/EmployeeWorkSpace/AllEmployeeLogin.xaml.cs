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
using System.Windows.Threading;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for AllEmployeeLogin.xaml
    /// </summary>
    public partial class AllEmployeeLogin : Window
    {
        private EmployeewsOfLocalPOS _unitofwork;
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        private List<Employee> _employee;
        private EmpLoginList _emplog;
        MaterialDesignThemes.Wpf.Chip _cUser;
        private DispatcherTimer LoadForm;
        private bool IsShow = false;
        private int _typeshow = 0; //1: login, 2: details, 3: logout, 4: start working
        private Window _main;

        public AllEmployeeLogin(Window main, EmployeewsOfLocalPOS unitofwork, EmployeewsOfCloudPOS cloudPosUnitofwork, MaterialDesignThemes.Wpf.Chip cUser, int typeshow)
        {
            _unitofwork = unitofwork;
            _cloudPosUnitofwork = cloudPosUnitofwork;
            _employee = _cloudPosUnitofwork.EmployeeRepository.Get(x => x.Deleted == 0).ToList();
            _main = main;

            _cUser = cUser;
            _typeshow = typeshow;
            InitializeComponent();

            initData();

            this.Loaded += AllEmployeeLogin_Loaded;

            LoadForm = new DispatcherTimer();
            LoadForm.Tick += LoadForm_Tick;
            LoadForm.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void AllEmployeeLogin_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = 500;
            spLoginAnother.Visibility = Visibility.Collapsed;

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void LoadForm_Tick(object sender, EventArgs e)
        {
            if(IsShow)
            {
                this.Width -= 10;
                if(this.Width == 500)
                {
                    LoadForm.Stop();
                }
            }
            else
            {
                this.Width += 10;
                if (this.Width == 900)
                {
                    LoadForm.Stop();
                }
            }
        }

        private void initData()
        {
            //main control
            btnLoginNew.Visibility = Visibility.Collapsed;
            BtnCodeLogin.Visibility = Visibility.Collapsed;
            btnLogout.Visibility = Visibility.Collapsed;
            btnView.Visibility = Visibility.Collapsed;
            btnStart.Visibility = Visibility.Collapsed;
            //left control
            btnAcceptLogin.Visibility = Visibility.Collapsed;
            btnAcceptLogout.Visibility = Visibility.Collapsed;
            btnAcceptView.Visibility = Visibility.Collapsed;
            btnAcceptStart.Visibility = Visibility.Collapsed;
            btnAcceptCancel.Visibility = Visibility.Collapsed;

            foreach (var e in EmpLoginListData.emploglist)
            {
                e.EmpWH.EndTime = DateTime.Now;
                int h = (e.EmpWH.EndTime - e.EmpWH.StartTime).Hours;
                int m = (e.EmpWH.EndTime - e.EmpWH.StartTime).Minutes;
                int s = (e.EmpWH.EndTime - e.EmpWH.StartTime).Seconds;

                e.TimePercent = (int)((((double)h) + (double)m / 60.0 + (double)s / 3600.0) / 24.0 * 100);
            }

            lvLoginList.ItemsSource = EmpLoginListData.emploglist;

            if(_typeshow == 1)//login
            {
                btnLoginNew.Visibility = Visibility.Visible;
                BtnCodeLogin.Visibility = Visibility.Visible;

                btnAcceptLogin.Visibility = Visibility.Visible;
                btnAcceptCancel.Visibility = Visibility.Visible;
            }
            else if(_typeshow == 2)//details
            {
                btnView.Visibility = Visibility.Visible;
                
                btnAcceptView.Visibility = Visibility.Visible;
                btnAcceptCancel.Visibility = Visibility.Visible;
            }
            else if(_typeshow == 3)//logout
            {
                btnLogout.Visibility = Visibility.Visible;

                btnAcceptLogout.Visibility = Visibility.Visible;
                btnAcceptCancel.Visibility = Visibility.Visible;
            }
            else if(_typeshow == 4)//start
            {
                btnStart.Visibility = Visibility.Visible;

                btnAcceptStart.Visibility = Visibility.Visible;
                btnAcceptCancel.Visibility = Visibility.Visible;
            }
        }

        private void BtnCodeLogin_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
            {
                if (this.Width == 500)
                {
                    IsShow = false;
                    LoadForm.Start();
                }

                spLoginAnother.Visibility = Visibility.Visible;
                loginNormal.Visibility = Visibility.Collapsed;
                loginCode.Visibility = Visibility.Visible;
                lvLoginList.UnselectAll();
                txbLabel.Text = "Login Another";
                setControl(true);
            }
            else
            {
                if(_typeshow == 1)
                {
                    return;
                }

                int index = lvLoginList.ItemContainerGenerator.IndexFromContainer(dep);

                EmpLoginList emp = EmpLoginListData.emploglist[index];
                if (emp == null)
                {
                    MessageBox.Show("Please choose employee to continue!");
                    return;
                }

                if (this.Width == 500)
                {
                    IsShow = false;
                    LoadForm.Start();
                }

                _emplog = emp;

                spLoginAnother.Visibility = Visibility.Visible;
                loginNormal.Visibility = Visibility.Collapsed;
                loginCode.Visibility = Visibility.Visible;
                lvLoginList.UnselectAll();
                txbLabel.Text = "Login Another";
                setControl(true);
            }
        }

        private async void loginCode_GoClick(object sender, RoutedEventArgs e)
        {
            string code;
            try
            {
                code = loginCode.InputValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect input!");
                return;
            }

            try
            {
                loginCode.ButtonGoAbleState(false);
                if(_typeshow == 1)//login
                {
                    await Async("", "", code, null);
                    setControl(true);
                }
                else if (_typeshow == 2)//view
                {
                    EmployeeDetail ed = new EmployeeDetail(_emplog.Emp.Username, _cloudPosUnitofwork);
                    ed.ShowDialog();
                    setControl(true);
                }
                else if(_typeshow == 3)//logout
                {
                    await Async("", "", code, _emplog);
                    setControl(true);
                }
                else if(_typeshow == 4)//start
                {
                    if (_emplog.Emp.DecryptedCode.Equals(code))
                    {
                        App.Current.Properties["CurrentEmpWorking"] = _emplog;
                        _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                        setControl(true);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login fail Employee's code is not correct!");
                    }
                }

                loginCode.ButtonGoAbleState(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnLoginNew_Click(object sender, RoutedEventArgs e)
        {
            if(this.Width == 500)
            {
                IsShow = false;
                LoadForm.Start();
            }

            spLoginAnother.Visibility = Visibility.Visible;
            loginNormal.Visibility = Visibility.Visible;
            loginCode.Visibility = Visibility.Collapsed;
            lvLoginList.UnselectAll();
            txbLabel.Text = "Login Another";
            setControl(true);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _emplog = lvLoginList.SelectedItem as EmpLoginList;
            if (_emplog == null)
            {
                MessageBox.Show("Please choose one employee want to start working!");
                return;
            }

            if (this.Width == 500)
            {
                IsShow = false;
                LoadForm.Start();
            }

            spLoginAnother.Visibility = Visibility.Visible;
            loginNormal.Visibility = Visibility.Visible;
            loginCode.Visibility = Visibility.Collapsed;
            lvLoginList.UnselectAll();
            txbLabel.Text = "Start Working";
            setControl(false);
            txtUsername.Text = _emplog.Emp.Username;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _emplog = lvLoginList.SelectedItem as EmpLoginList;
            if (_emplog == null)
            {
                MessageBox.Show("Please choose one employee want to logout!");
                return;
            }

            if (this.Width == 500)
            {
                IsShow = false;
                LoadForm.Start();
            }

            spLoginAnother.Visibility = Visibility.Visible;
            loginNormal.Visibility = Visibility.Visible;
            loginCode.Visibility = Visibility.Collapsed;
            lvLoginList.UnselectAll();
            txbLabel.Text = "Logout";
            setControl(false);
            txtUsername.Text = _emplog.Emp.Username;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            _emplog = lvLoginList.SelectedItem as EmpLoginList;
            if (_emplog == null)
            {
                MessageBox.Show("Please choose one employee want to view details!");
                return;
            }

            if (this.Width == 500)
            {
                IsShow = false;
                LoadForm.Start();
            }

            spLoginAnother.Visibility = Visibility.Visible;
            loginNormal.Visibility = Visibility.Visible;
            loginCode.Visibility = Visibility.Collapsed;
            lvLoginList.UnselectAll();
            txbLabel.Text = "View Details";
            setControl(false);
            txtUsername.Text = _emplog.Emp.Username;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                txtPass.Focus();
            }
        }

        private async void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(_typeshow == 1)
                {
                    string username = txtUsername.Text.Trim();
                    string pass = txtPass.Password.Trim();
                    try
                    {
                        btnAcceptLogin.IsEnabled = false;
                        PgbLoginProcess.Visibility = Visibility.Visible;
                        await Async(username, pass, "", null);

                        btnAcceptLogin.IsEnabled = true;
                        PgbLoginProcess.Visibility = Visibility.Collapsed;

                        lvLoginList.ItemsSource = EmpLoginListData.emploglist;
                        lvLoginList.Items.Refresh();

                        setControl(true);

                        if (App.Current.Properties["CurrentEmpWorking"] != null)
                        {
                            _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if(_typeshow == 2)
                {
                    if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
                    {
                        MessageBox.Show("Login fail! Please try again!");
                        return;
                    }

                    EmployeeDetail ed = new EmployeeDetail(_emplog.Emp.Username, _cloudPosUnitofwork);
                    ed.ShowDialog();

                    setControl(true);
                }
                else if(_typeshow == 3)
                {
                    if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
                    {
                        MessageBox.Show("Fail! Please try again!");
                        return;
                    }

                    try
                    {
                        btnAcceptLogout.IsEnabled = false;
                        PgbLoginProcess.Visibility = Visibility.Visible;
                        await Async("", "", "", _emplog);

                        btnAcceptLogout.IsEnabled = true;
                        PgbLoginProcess.Visibility = Visibility.Collapsed;

                        lvLoginList.ItemsSource = EmpLoginListData.emploglist;
                        lvLoginList.Items.Refresh();

                        setControl(true);

                        if (App.Current.Properties["CurrentEmpWorking"] != null)
                        {
                            _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if(_typeshow == 4)
                {
                    if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
                    {
                        MessageBox.Show("Login fail! Please try again!");
                        return;
                    }

                    App.Current.Properties["CurrentEmpWorking"] = _emplog;
                    _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;

                    this.Close();
                }
            }
        }

        private async void btnAcceptLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Password.Trim();

            try
            {
                btnAcceptLogin.IsEnabled = false;
                PgbLoginProcess.Visibility = Visibility.Visible;
                await Async(username, pass, "", null);
                
                btnAcceptLogin.IsEnabled = true;
                PgbLoginProcess.Visibility = Visibility.Collapsed;
                
                lvLoginList.ItemsSource = EmpLoginListData.emploglist;
                lvLoginList.Items.Refresh();

                setControl(true);

                if(App.Current.Properties["CurrentEmpWorking"] != null)
                {
                    _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnAcceptLogout_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Password.Trim();

            if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
            {
                MessageBox.Show("Fail! Please try again!");
                return;
            }

            try
            {
                btnAcceptLogout.IsEnabled = false;
                PgbLoginProcess.Visibility = Visibility.Visible;
                await Async(username, pass, "", _emplog);

                btnAcceptLogout.IsEnabled = true;
                PgbLoginProcess.Visibility = Visibility.Collapsed;
                
                lvLoginList.ItemsSource = EmpLoginListData.emploglist;
                lvLoginList.Items.Refresh();

                setControl(true);

                if (App.Current.Properties["CurrentEmpWorking"] != null)
                {
                    _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnAcceptStart_Click(object sender, RoutedEventArgs e)
        {
            if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
            {
                MessageBox.Show("Login fail! Please try again!");
                return;
            }

            App.Current.Properties["CurrentEmpWorking"] = _emplog;
            _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;

            this.Close();
        }

        private void btnAcceptView_Click(object sender, RoutedEventArgs e)
        {
            if (!_emplog.Emp.DecryptedPass.Equals(txtPass.Password.Trim()))
            {
                MessageBox.Show("Login fail! Please try again!");
                return;
            }

            EmployeeDetail ed = new EmployeeDetail(_emplog.Emp.Username, _cloudPosUnitofwork);
            ed.ShowDialog();

            setControl(true);
        }

        private void btnAcceptCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Width == 900)
            {
                IsShow = true;
                LoadForm.Start();
            }

            spLoginAnother.Visibility = Visibility.Collapsed;
            loginNormal.Visibility = Visibility.Collapsed;
            loginCode.Visibility = Visibility.Collapsed;
            lvLoginList.UnselectAll();
            setControl(true);
        }

        private async Task Async(string username, string pass, string code, EmpLoginList empout)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (empout != null)
                    {
                        if (EmpLoginListData.emploglist.Count == 1)
                        {
                            var orderedTable = _unitofwork.TableRepository.Get(x => x.IsOrdered == 1).ToList();
                            if (orderedTable.Count != 0)
                            {
                                MessageBox.Show("You can not logout because still have Tables that in the ordering state out there. Please check again!");
                                return;
                            }
                        }

                        if ((empout.Emp.Username.Equals(username) && (empout.Emp.DecryptedPass.Equals(pass)) || empout.Emp.DecryptedCode.Equals(code)))
                        {
                            empout.EmpWH.EndTime = DateTime.Now;
                            _cloudPosUnitofwork.WorkingHistoryRepository.Insert(empout.EmpWH);
                            _cloudPosUnitofwork.Save();

                            var workH = empout.EmpWH.EndTime - empout.EmpWH.StartTime;
                            empout.EmpSal = _cloudPosUnitofwork.SalaryNoteRepository.Get(sle => sle.EmpId.Equals(empout.Emp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) && sle.ForYear.Equals(DateTime.Now.Year)).First();
                            empout.EmpSal.WorkHour += workH.Hours + (workH.Minutes / 60.0) + (workH.Seconds / 3600.0);
                            empout.EmpSal.SalaryValue = (decimal) (empout.EmpSal.WorkHour * empout.Emp.HourWage);
                            _cloudPosUnitofwork.SalaryNoteRepository.Update(empout.EmpSal);
                            _cloudPosUnitofwork.Save();

                            EmpLoginListData.emploglist.Remove(empout);

                            Dispatcher.Invoke(() =>
                            {
                                checkEmployeeCount();
                            });

                            return;
                        }
                        else
                        {
                            MessageBox.Show("Fail! Please try again!");
                            return;
                        }
                    }

                    bool isFound = false;
                    foreach (Employee emp in _employee)
                    {
                        if ((emp.Username.Equals(username) && (emp.DecryptedPass.Equals(pass)) || emp.DecryptedCode.Equals(code)))
                        {
                            var chemp = EmpLoginListData.emploglist.Where(x => x.Emp.EmpId.Equals(emp.EmpId)).ToList();
                            if(chemp.Count != 0)
                            {
                                MessageBox.Show("This employee is already login!");
                                return;
                            }

                            try
                            {
                                SalaryNote empSalaryNote = _cloudPosUnitofwork.SalaryNoteRepository.Get(sle => sle.EmpId.Equals(emp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) && sle.ForYear.Equals(DateTime.Now.Year)).First();

                                App.Current.Properties["EmpSN"] = empSalaryNote;
                                WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalaryNote.SnId, EmpId = empSalaryNote.EmpId };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                            }
                            catch (Exception ex)
                            {
                                SalaryNote empSalary = new SalaryNote { EmpId = emp.EmpId, SalaryValue = 0, WorkHour = 0, ForMonth = DateTime.Now.Month, ForYear = DateTime.Now.Year, IsPaid = 0 };
                                _cloudPosUnitofwork.SalaryNoteRepository.Insert(empSalary);
                                _cloudPosUnitofwork.Save();
                                WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalary.SnId, EmpId = empSalary.EmpId };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                                App.Current.Properties["EmpSN"] = empSalary;
                            }

                            Dispatcher.Invoke(() =>
                            {
                                EmpLoginListData.emploglist.Add(new EmpLoginList { Emp = emp, EmpSal = App.Current.Properties["EmpSN"] as SalaryNote, EmpWH = App.Current.Properties["EmpWH"] as WorkingHistory, TimePercent = 0 });
                                checkEmployeeCount();
                                setControl(true);
                            });
                            isFound = true;

                            //end create

                            break;
                        }

                    }

                    if (!isFound)
                    {
                        MessageBox.Show("incorrect username or password");
                        return;
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void checkEmployeeCount()
        {
            if (EmpLoginListData.emploglist.Count == 0)
            {
                foreach (var table in _unitofwork.TableRepository.Get())
                {
                    var orderTemp = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(table.TableId)).First();
                    orderTemp.EmpId = "";
                    orderTemp.CusId = "CUS0000001";
                    orderTemp.Ordertime = DateTime.Now;
                    orderTemp.TotalPriceNonDisc = 0;
                    orderTemp.TotalPrice = 0;
                    orderTemp.CustomerPay = 0;
                    orderTemp.PayBack = 0;
                    orderTemp.SubEmpId = "";
                    orderTemp.Pax = 0;

                    table.IsOrdered = 0;
                    table.IsPrinted = 0;

                    var orderDetails = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(orderTemp.OrdertempId));
                    if (orderDetails.Count() != 0)
                    {
                        foreach (var od in orderDetails)
                        {
                            _unitofwork.OrderDetailsTempRepository.Delete(od);
                            _unitofwork.Save();
                        }
                    }
                }

                App.Current.Properties["CurrentEmpWorking"] = null;
                _main.Close();
                Login login = new Login();
                this.Close();
                login.Show();
                return;
            }
            else
            {
                _cUser.Content = EmpLoginListData.emploglist.Count + " employee(s) available";
                if(App.Current.Properties["CurrentEmpWorking"] != null)
                {
                    _cUser.Content = (App.Current.Properties["CurrentEmpWorking"] as EmpLoginList).Emp.Username;
                }
            }
            
            lvLoginList.ItemsSource = EmpLoginListData.emploglist;
            lvLoginList.Items.Refresh();
        }

        private void setControl(bool b)
        {
            if(b)
            {
                txtUsername.IsEnabled = b;
                txtPass.IsEnabled = true;
                txtUsername.Text = "";
                txtPass.Password = "";
            }
            else
            {
                txtUsername.IsEnabled = b;
                txtPass.IsEnabled = true;
                txtUsername.Text = "";
                txtPass.Password = "";
            }
        }

        private void LoginCode_OnTurnOffKeyboard(object sender, RoutedEventArgs e)
        {
            //do nothing
        }
    }

    public class EmpLoginList
    {
        private Employee _emp;
        private SalaryNote _empsal;
        private WorkingHistory _empwh;
        private int _timepercent;

        public Employee Emp
        {
            get { return _emp; }
            set { _emp = value; }
        }

        public SalaryNote EmpSal
        {
            get { return _empsal; }
            set { _empsal = value; }
        }

        public WorkingHistory EmpWH
        {
            get { return _empwh; }
            set { _empwh = value; }
        }

        public int TimePercent
        {
            get { return _timepercent; }
            set { _timepercent = value; }
        }
    }

    public class EmpLoginListData
    {
        public static List<EmpLoginList> emploglist = new List<EmpLoginList>();
    }

}
