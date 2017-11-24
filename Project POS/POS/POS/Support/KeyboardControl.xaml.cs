using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using POS.EmployeeWorkSpace;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;

namespace POS.Support
{
    /// <summary>
    /// Interaction logic for KeyboardControl.xaml
    /// </summary>
    public partial class KeyboardControl : UserControl
    {
        public static int LOG_IN_TYPE = 1;
        public static int ORDER_PAYMENT_TYPE = 2;
        private static EmployeewsOfAsowell _unitofwork;
        public Window parent { get; set; }
        public int Type { get; set; }

        public Entities.OrderNote currentOrder { get; set; }
        public string payMethod { get; set; }

        public KeyboardControl()
        {
            InitializeComponent();

            _unitofwork = new EmployeewsOfAsowell();
            parent = null;
            Type = LOG_IN_TYPE;
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
            if (TxtInputValue.Text.Length == 0)
                return;

            TxtInputValue.Text = TxtInputValue.Text.Remove(TxtInputValue.Text.Length - 1);
        }



        private async void BtnGo_OnClick(object sender, RoutedEventArgs e)
        {
            if (Type == LOG_IN_TYPE)
            {
                if (parent == null)
                    return;

                int code;
                try
                {
                    code = int.Parse(TxtInputValue.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Incorrect input!");
                    return;
                }

                try
                {
                    BtnGo.IsEnabled = false;
                    await LoginAsync(code);

                    BtnGo.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Type == ORDER_PAYMENT_TYPE)
            {
                if (payMethod == "" || payMethod == null)
                {
                    MessageBox.Show("please choose Payment Method!");
                    return;
                }


                if (payMethod == PaymentMethod.Cash.ToString())
                {
                    currentOrder.paymentMethod = (int) PaymentMethod.Cash;
                }
                else if (payMethod == PaymentMethod.Cheque.ToString())
                {
                    currentOrder.paymentMethod = (int)PaymentMethod.Cheque;
                }
                else if (payMethod == PaymentMethod.Credit.ToString())
                {
                    currentOrder.paymentMethod = (int)PaymentMethod.Credit;
                }
                else if (payMethod == PaymentMethod.Deferred.ToString())
                {
                    currentOrder.paymentMethod = (int)PaymentMethod.Deferred;
                }
                else if (payMethod == PaymentMethod.International.ToString())
                {
                    currentOrder.paymentMethod = (int)PaymentMethod.International;
                }
                else if (payMethod == PaymentMethod.OnAcount.ToString())
                {
                    currentOrder.paymentMethod = (int)PaymentMethod.OnAcount;
                }


                try
                {
                    int cusPay = int.Parse(TxtInputValue.Text);

                    if (cusPay < currentOrder.TotalPrice)
                    {
                        MessageBox.Show("All payment ground up to higher number!");
                        return;
                    }

                    currentOrder.CustomerPay = cusPay;
                    currentOrder.PayBack = currentOrder.CustomerPay - currentOrder.TotalPrice;
                    parent.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Incorrect input!");
                    return;
                }
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
