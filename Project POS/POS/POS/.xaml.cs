using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using POS.Entities;
using POS.Repository.DAL;
using POS.EmployeeWorkSpace;
using System.Windows.Input;
using System.Windows.Threading;
using log4net;
using POS.BusinessModel;
using POS.WareHouseWorkSpace;

namespace POS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        internal EmployeewsOfCloudPOS _unitofwork;
        private DispatcherTimer LoadCodeLogin;

        private static readonly ILog AppLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Login()
        {
            //string[] config = ReadWriteData.ReadDBConfig();
            //if (config != null)
            //{
            //    _unitofwork = new EmployeewsOfLocalPOS(config[0], config[1], config[2], config[3]);
            //}
            //else
            //{
            //    _unitofwork = new EmployeewsOfLocalPOS();
            //}

            _unitofwork = new EmployeewsOfCloudPOS();
            InitializeComponent();

            txtUsername.Focus();

            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;

            LoadCodeLogin = new DispatcherTimer();
            LoadCodeLogin.Tick += LoadCodeLogin_Tick; ;
            LoadCodeLogin.Interval = new TimeSpan(0, 0, 0, 0, 1);

            this.Closing += Closing_LoginWindos;
        }

        private bool isCodeLoginTurnOn = false;
        private void LoadCodeLogin_Tick(object sender, EventArgs e)
        {
            if (isCodeLoginTurnOn)
            {
                gNormalLoginForm.Width -= 10;
                if (gNormalLoginForm.Width == 0)
                {
                    LoadCodeLogin.Stop();
                }
            }
            else
            {
                gNormalLoginForm.Width += 10;
                if (gNormalLoginForm.Width == 400)
                {
                    LoadCodeLogin.Stop();
                }
            }
        }

        private void txtUsername_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPass.Focus();
            }
        }

        private async void txtPass_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string username = txtUsername.Text;
                string pass = txtPass.Password;
                try
                {
                    btnLogin.IsEnabled = false;
                    PgbLoginProcess.Visibility = Visibility.Visible;
                    await LoginAsync(username, pass);

                    btnLogin.IsEnabled = true;
                    PgbLoginProcess.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Password.Trim();
            try
            {
                btnLogin.IsEnabled = false;
                PgbLoginProcess.Visibility = Visibility.Visible;
                await LoginAsync(username, pass);

                btnLogin.IsEnabled = true;
                PgbLoginProcess.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoginAsync(string username, string pass)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<Employee> empList = _unitofwork.EmployeeRepository.Get().ToList();

                    var emp = empList.FirstOrDefault(x => x.Username.Equals(username) && x.DecryptedPass.Equals(pass));
                    if (emp != null)
                    {
                        App.Current.Properties["EmpLogin"] = emp;
                        if (emp.EmpRole == (int)EmployeeRole.Stock)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                WareHouseWindow wareHouse = new WareHouseWindow();
                                wareHouse.Show();
                            });
                        }
                        else
                        {
                            try
                            {
                                SalaryNote empSalaryNote = _unitofwork.SalaryNoteRepository.Get(sle =>
                                    sle.EmpId.Equals(emp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) &&
                                    sle.ForYear.Equals(DateTime.Now.Year)).First();

                                App.Current.Properties["EmpSN"] = empSalaryNote;
                                WorkingHistory empWorkHistory = new WorkingHistory
                                {
                                    ResultSalary = empSalaryNote.SnId,
                                    EmpId = empSalaryNote.EmpId
                                };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                            }
                            catch (Exception ex)
                            {
                                SalaryNote empSalary = new SalaryNote
                                {
                                    EmpId = emp.EmpId,
                                    SalaryValue = 0,
                                    WorkHour = 0,
                                    ForMonth = DateTime.Now.Month,
                                    ForYear = DateTime.Now.Year,
                                    IsPaid = 0
                                };
                                _unitofwork.SalaryNoteRepository.Insert(empSalary);
                                _unitofwork.Save();
                                WorkingHistory empWorkHistory = new WorkingHistory
                                {
                                    ResultSalary = empSalary.SnId,
                                    EmpId = empSalary.EmpId
                                };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                                App.Current.Properties["EmpSN"] = empSalary;
                            }

                            Dispatcher.Invoke(() =>
                            {
                                EmpLoginListData.emploglist.Clear();
                                EmpLoginListData.emploglist.Add(new EmpLoginList
                                {
                                    Emp = emp,
                                    EmpSal = App.Current.Properties["EmpSN"] as SalaryNote,
                                    EmpWH = App.Current.Properties["EmpWH"] as WorkingHistory,
                                    TimePercent = 0
                                });

                                EmployeeWorkSpace.MainWindow main = new EmployeeWorkSpace.MainWindow();
                                main.Show();
                            });
                        }
                    }
                    else    
                    {
                        //Get Admin
                        List<AdminRe> adList = _unitofwork.AdminreRepository.Get().ToList();

                        var ad = adList.FirstOrDefault(x => x.Username.Equals(username) && x.DecryptedPass.Equals(pass));
                        if (ad != null)
                        {
                            App.Current.Properties["AdLogin"] = ad;

                            Dispatcher.Invoke(() =>
                            {
                                AdminNavWindow navwindow = new AdminNavWindow();
                                navwindow.Show();
                            });
                        }

                        if (ad == null && emp == null)
                        {
                            MessageBox.Show("incorrect username or password");
                            return;
                        }
                    }
                    

                    Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: \n" + ex.Message);
                AppLog.Error(ex);
            }
        }

        private async void btnLoginCode_Click(object sender, RoutedEventArgs e)
        {
            string code;
            try
            {
                code = KbEmpCodeLoginForm.InputValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect input!");
                return;
            }

            try
            {
                KbEmpCodeLoginForm.ButtonGoAbleState(false);
                await LoginByCodeAsync(code);
                KbEmpCodeLoginForm.ButtonGoAbleState(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoginByCodeAsync(string code)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<Employee> empList = _unitofwork.EmployeeRepository.Get().ToList();
                    Employee loginEmp = empList.FirstOrDefault(x => x.DecryptedCode.Equals(code));
                    if (loginEmp != null)
                    {
                        App.Current.Properties["EmpLogin"] = loginEmp;

                        if (loginEmp.EmpRole == (int)EmployeeRole.Stock)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                WareHouseWindow wareHouse = new WareHouseWindow();
                                wareHouse.Show();
                            });
                        }
                        else
                        {
                            try
                            {
                                SalaryNote empSalaryNote = _unitofwork.SalaryNoteRepository.Get(sle =>
                                    sle.EmpId.Equals(loginEmp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) &&
                                    sle.ForYear.Equals(DateTime.Now.Year)).First();

                                App.Current.Properties["EmpSN"] = empSalaryNote;
                                WorkingHistory empWorkHistory = new WorkingHistory
                                {
                                    ResultSalary = empSalaryNote.SnId,
                                    EmpId = empSalaryNote.EmpId
                                };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                            }
                            catch (Exception ex)
                            {
                                SalaryNote empSalary = new SalaryNote
                                {
                                    EmpId = loginEmp.EmpId,
                                    SalaryValue = 0,
                                    WorkHour = 0,
                                    ForMonth = DateTime.Now.Month,
                                    ForYear = DateTime.Now.Year,
                                    IsPaid = 0
                                };
                                _unitofwork.SalaryNoteRepository.Insert(empSalary);
                                _unitofwork.Save();
                                WorkingHistory empWorkHistory = new WorkingHistory
                                {
                                    ResultSalary = empSalary.SnId,
                                    EmpId = empSalary.EmpId
                                };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                                App.Current.Properties["EmpSN"] = empSalary;
                            }

                            Dispatcher.Invoke(() =>
                            {
                                EmpLoginListData.emploglist.Clear();
                                EmpLoginListData.emploglist.Add(new EmpLoginList
                                {
                                    Emp = loginEmp,
                                    EmpSal = App.Current.Properties["EmpSN"] as SalaryNote,
                                    EmpWH = App.Current.Properties["EmpWH"] as WorkingHistory,
                                    TimePercent = 0
                                });

                                EmployeeWorkSpace.MainWindow main = new EmployeeWorkSpace.MainWindow();
                                main.Show();
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("incorrect username or password");
                        return;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: \n" + ex.Message);
                AppLog.Error(ex);
            }
        }


        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConfigWindow dbConfig = new DatabaseConfigWindow();
            dbConfig.ShowDialog();
        }

        private void Closing_LoginWindos(object sender, EventArgs args)
        {
            _unitofwork.Dispose();
        }

        private void ButtonChangeLoginType_Click(object sender, RoutedEventArgs e)
        {
            isCodeLoginTurnOn = true;
            LoadCodeLogin.Start();
            //gNormalLoginForm.Visibility = Visibility.Collapsed;
        }

        private void KbEmpCodeLoginForm_OnTurnOffKeyboard(object sender, RoutedEventArgs e)
        {
            isCodeLoginTurnOn = false;
            LoadCodeLogin.Start();
        }
    }
}
