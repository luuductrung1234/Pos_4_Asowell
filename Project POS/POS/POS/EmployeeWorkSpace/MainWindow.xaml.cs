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
            login = new Login();
            st = new SettingFoodPage(_unitofwork);
            stts = new SettingTableSize();

            this.Closing += (sender, args) =>
            {
                _unitofwork.Dispose();
            };
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

        private void btnWH_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkingHistoryDialog empWH = new EmployeeWorkingHistoryDialog(App.Current.Properties["EmpWH"] as WorkingHistory);
            empWH.ShowDialog();
        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {
            WorkingHistory wh = App.Current.Properties["EmpWH"] as WorkingHistory;

            wh.Endhour = DateTime.Now.Hour;
            wh.Endminute = DateTime.Now.Minute;
            _unitofwork.WorkingHistoryRepository.Insert(wh);
            _unitofwork.Save();

            this.Close();
            login.Show();

        }
        
    }
}
