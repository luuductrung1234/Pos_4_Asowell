using POS.Entities;
using POS.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using POS.Helper.PrintHelper;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminwsOfCloud _unitowork;
        EmployeeListPage empListPage;
        OrderNotePage ordernotepage;
        SalaryPage salarypage;
        ProductDetailPage productdetals;
        internal Login login;
        AdminRe curAdmin;
        CustomerPage ctmP;
        ReceiptNotePage receiptnotepage;
        private statisticsFoodPage FoodPage;
        private StatisticsWorkingHourPage statisticsWorkingHourPage;
        private HomePage homePage;
        private ProductCreatorPage productCreator;
        public AdminWindow()
        {
            InitializeComponent();
            _unitowork = new AdminwsOfCloud();

            AdminRe getAdmin = App.Current.Properties["AdLogin"] as AdminRe;
            curAdmin = _unitowork.AdminreRepository
                .Get(ad => ad.Username.Equals(getAdmin.Username) && ad.Pass.Equals(getAdmin.Pass)).First();
            cUser.Content = curAdmin.Name;
            
            empListPage = new EmployeeListPage(_unitowork, curAdmin);
            salarypage = new SalaryPage(_unitowork);
            productdetals = new ProductDetailPage( _unitowork);
            ctmP=new CustomerPage(_unitowork);
            ordernotepage = new OrderNotePage(_unitowork);
            receiptnotepage = new ReceiptNotePage(_unitowork);
            FoodPage=new statisticsFoodPage(_unitowork);
            statisticsWorkingHourPage=new StatisticsWorkingHourPage(_unitowork);
            homePage = new HomePage(_unitowork);
            productCreator = new ProductCreatorPage(_unitowork);
            myframe.Navigate(homePage);

            DispatcherTimer RefreshTimer = new DispatcherTimer();
            RefreshTimer.Tick += Refresh_Tick;
            RefreshTimer.Interval = new TimeSpan(0, 2, 0);
            RefreshTimer.Start();

            Closing += AdminWindow_Closing;
            
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            homePage.RefreshHome();
        }


        private void AdminWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _unitowork.Dispose();
        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {
            login = new Login();
            this.Close();
            login.Show();
        }

        private void EmployeeInfo_onClick(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(empListPage);
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {

        }

        private void SalaryInfo_onClick(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(salarypage);
        }

        private void ProductInfo_onclick(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(productdetals);
        }

        private void BtnDetails_OnClick(object sender, RoutedEventArgs e)
        {
            AdminDetailWindow adw = new AdminDetailWindow(_unitowork, curAdmin);
            adw.Show();
        }

        private void bntCustomer_Click(object sender, RoutedEventArgs e)
        {
            
            myframe.Navigate(ctmP);
        }

        private void bntOrder_Click(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(ordernotepage);
        }

        private void bntReceipt_Click(object sender, RoutedEventArgs e)
        {
            myframe.Navigate(receiptnotepage);
        }

        private void View_Statistics_Quantity_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myframe.Navigate(FoodPage);
        }

        private void ViewstaticWH_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myframe.Navigate(statisticsWorkingHourPage);
        }

        private void HomePage_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            myframe.Navigate(homePage);
        }

        private void EODReport_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ToDo: May be shoud close the repository after print
            var printer = new DoPrintHelper(new EmployeewsOfAsowell(), new EmployeewsOfCloud(), DoPrintHelper.Eod_Printing);
            printer.DoPrint();
        }

        private void BntCreateNewProduct_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Create new product feature is now not working. We will update later!");
            myframe.Navigate(productCreator);
        }
    }
}
