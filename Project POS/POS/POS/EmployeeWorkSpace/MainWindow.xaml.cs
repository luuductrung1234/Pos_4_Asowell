using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using POS.BusinessModel;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.DAL;
using POS.Repository.Interfaces;
using System;
using System.Windows.Threading;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// the object that store all repository you want to get data DBAsowell
        /// in Employee WorkSpace
        /// </summary>
        internal EmployeewsOfAsowell _unitofwork;

        Employee emp;
        SalaryNote empSln;
        public BusinessModel.Table currentTable { get; set; }
        public Chair currentChair { get; set; }
        internal Table b;
        internal Dash d;
        internal Entry en;
        internal Info info;
        internal Login login;
        internal SettingFoodPage st;
        internal SettingTableSize stts;

        public MainWindow()
        {
            InitializeComponent();
            currentTable = null;
            emp = App.Current.Properties["EmpLogin"] as Employee;
            empSln = App.Current.Properties["EmpSN"] as SalaryNote;

            cUser.Content = emp.Username;
            
            _unitofwork = new EmployeewsOfAsowell();
            b = new Table(_unitofwork);
            d = new Dash();
            en = new Entry();
            info = new Info();
            st = new SettingFoodPage(_unitofwork);
            stts = new SettingTableSize();

            DispatcherTimer WorkTime = new DispatcherTimer();
            WorkTime.Tick += WorkTime_Tick;
            WorkTime.Interval = new TimeSpan(0, 0, 1);
            WorkTime.Start();

            this.Closing += (sender, args) =>
            {
                WorkTime.Stop();
                _unitofwork.Dispose();
            };
        }

        private void WorkTime_Tick(object sender, EventArgs e)
        {
            DateTime nowWH = DateTime.Now;
            DateTime startWH = (App.Current.Properties["EmpWH"] as WorkingHistory).StartTime;
            var timer = nowWH - startWH;
            string fH = "", fm = "", fs = "";
            fH = timer.Hours.ToString();
            fm = timer.Minutes.ToString();
            fs = timer.Seconds.ToString();

            if(timer.Hours < 10)
            {
                fH = "0" + timer.Hours;
            }
            if(timer.Minutes < 10)
            {
                fm = "0" + timer.Minutes;
            }
            if(timer.Seconds < 10)
            {
                fs = "0" + timer.Seconds;
            }

            txtTimeWk.Text = fH + ":" + fm + ":" + fs;
        }

        private void bntDash_Click(object sender, RoutedEventArgs e)
        {
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = false;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
            myFrame.Navigate(d);

        }

        private void bntTable_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(b);
            bntTable.IsEnabled = false;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
        }

        private void bntEntry_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(en);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = false;
            bntInfo.IsEnabled = true;
        }

        private void bntInfo_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(info);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = false;
        }


        private void DemoItemsListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void lbiFoodList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(st);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
        }

        private void lbiTableSize_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(stts);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
        }

        private void btnEmpDetail_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetail emd = new EmployeeDetail(cUser.Content.ToString(), _unitofwork);
            emd.ShowDialog();
        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {
            WorkingHistory wh = App.Current.Properties["EmpWH"] as WorkingHistory;

            wh.EndTime = DateTime.Now;
            _unitofwork.WorkingHistoryRepository.Insert(wh);
            _unitofwork.Save();

            var workH = wh.EndTime - wh.StartTime;
            empSln.WorkHour += workH.Hours + workH.Minutes/60 + workH.Seconds/3600;
            _unitofwork.SalaryNoteRepository.Update(empSln);
            _unitofwork.Save();

            login = new Login();
            this.Close();
            login.Show();
        }
        
    }
}
