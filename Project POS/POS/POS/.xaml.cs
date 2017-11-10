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
using POS.Repository.DAL;
using POS.Repository.Interfaces;

namespace POS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        internal EmployeewsOfAsowell _unitempofwork;

        public Login()
        {
            _unitempofwork = new EmployeewsOfAsowell();
            InitializeComponent();

            this.WindowState = WindowState.Normal;

            this.Closing += Closing_LoginWindos;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
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

        private async Task LoginAsync(string username, string pass)
        {
            try
            {
                await Task.Run(() =>
                {
                    bool isFound = false;
                    List<Employee> empList = _unitempofwork.EmployeeRepository.Get().ToList();
                    List<AdminRe> AdList = _unitempofwork.AdminreRepository.Get().ToList();
                    foreach (Employee emp in empList)
                    {
                        if (emp.Username.Equals(username) && emp.Pass.Equals(pass))
                        {
                            App.Current.Properties["EmpLogin"] = emp;

                            try
                            {
                                SalaryNote empSalaryNote = _unitempofwork.SalaryNoteRepository.Get(sle => sle.EmpId.Equals(emp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) && sle.ForYear.Equals(DateTime.Now.Year)).First();

                                App.Current.Properties["EmpSN"] = empSalaryNote;
                                WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalaryNote.SnId, EmpId = empSalaryNote.EmpId };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                            }
                            catch (Exception ex)
                            {
                                SalaryNote empSalary = new SalaryNote { EmpId = emp.EmpId, SalaryValue = 0, WorkHour = 0, ForMonth = DateTime.Now.Month, ForYear = DateTime.Now.Year, IsPaid = 0 };
                                _unitempofwork.SalaryNoteRepository.Insert(empSalary);
                                _unitempofwork.Save();
                                WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalary.SnId, EmpId = empSalary.EmpId };
                                App.Current.Properties["EmpWH"] = empWorkHistory;
                                App.Current.Properties["EmpSN"] = empSalary;
                            }
                            
                            Dispatcher.Invoke(() =>
                            {
                                EmployeeWorkSpace.MainWindow main = new EmployeeWorkSpace.MainWindow();
                                main.Show();
                            });
                            isFound = true;

                            //end create

                            break;
                        }

                    }
                    //Get Admin
                    bool isfoundad = false;
                    if (!isFound)
                    {
                        foreach (var item in AdList)
                        {
                            if (item.Username.Equals(username) && item.Pass.Equals(pass))
                            {
                                App.Current.Properties["AdLogin"] = item;
                                Dispatcher.Invoke(() =>
                                {
                                    AdminWorkSpace.AdminWindow adminwindow = new AdminWorkSpace.AdminWindow();
                                    adminwindow.Show();
                                });
                                
                                isfoundad = true;
                                break;

                            }

                        }
                        if (!isfoundad)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Closing_LoginWindos(object sender, EventArgs args)
        {
            _unitempofwork.Dispose();
        }
    }
}
