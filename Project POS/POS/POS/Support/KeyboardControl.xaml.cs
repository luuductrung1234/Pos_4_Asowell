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
using POS.EmployeeWorkSpace;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.Support
{
    /// <summary>
    /// Interaction logic for KeyboardControl.xaml
    /// </summary>
    public partial class KeyboardControl : UserControl
    {
        private static EmployeewsOfAsowell _unitofwork;
        public Login parent { get; set; }

        public KeyboardControl()
        {
            InitializeComponent();

            _unitofwork = new EmployeewsOfAsowell();
            parent = null;
        }

        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "1";
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "2";
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "3";
        }

        private void BtnFour_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "4";
        }

        private void BtnFive_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "5";
        }

        private void BtnSix_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "6";
        }

        private void BtnSeven_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "7";
        }

        private void BtnEight_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "8";
        }

        private void BtnNine_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "9";
        }

        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "0";
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = TxtInputValue.Text.Remove(TxtInputValue.Text.Length - 1);
        }





        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (parent == null)
                return;

            int code = int.Parse(TxtInputValue.Text);
            try
            {
                BtnLogin.IsEnabled = false;
                await LoginAsync(code);

                BtnLogin.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoginAsync(int code)
        {
            try
            {
                await Task.Run(() =>
                {
                    bool isFound = false;
                    Employee loginEmp = _unitofwork.EmployeeRepository.Get(x => x.empCode == code).FirstOrDefault();
                    if (loginEmp != null)
                    {
                        App.Current.Properties["EmpLogin"] = loginEmp;

                        try
                        {
                            SalaryNote empSalaryNote = _unitofwork.SalaryNoteRepository.Get(sle => sle.EmpId.Equals(loginEmp.EmpId) && sle.ForMonth.Equals(DateTime.Now.Month) && sle.ForYear.Equals(DateTime.Now.Year)).First();

                            App.Current.Properties["EmpSN"] = empSalaryNote;
                            WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalaryNote.SnId, EmpId = empSalaryNote.EmpId };
                            App.Current.Properties["EmpWH"] = empWorkHistory;
                        }
                        catch (Exception ex)
                        {
                            SalaryNote empSalary = new SalaryNote { EmpId = loginEmp.EmpId, SalaryValue = 0, WorkHour = 0, ForMonth = DateTime.Now.Month, ForYear = DateTime.Now.Year, IsPaid = 0 };
                            _unitofwork.SalaryNoteRepository.Insert(empSalary);
                            _unitofwork.Save();
                            WorkingHistory empWorkHistory = new WorkingHistory { ResultSalary = empSalary.SnId, EmpId = empSalary.EmpId };
                            App.Current.Properties["EmpWH"] = empWorkHistory;
                            App.Current.Properties["EmpSN"] = empSalary;
                        }

                        Dispatcher.Invoke(() =>
                        {
                            EmpLoginListData.emploglist.Clear();
                            EmpLoginListData.emploglist.Add(new EmpLoginList { Emp = loginEmp, EmpSal = App.Current.Properties["EmpSN"] as SalaryNote, EmpWH = App.Current.Properties["EmpWH"] as WorkingHistory, TimePercent = 0 });

                            EmployeeWorkSpace.MainWindow main = new EmployeeWorkSpace.MainWindow();
                            main.Show();
                        });
                        isFound = true;

                    }

                    if (!isFound)
                    {
                        MessageBox.Show("incorrect username or password");
                        return;
                    }
                    Dispatcher.Invoke(() =>
                    {
                        parent.Close();
                    });

                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
