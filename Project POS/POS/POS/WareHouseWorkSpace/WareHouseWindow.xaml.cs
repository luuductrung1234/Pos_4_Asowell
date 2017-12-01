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
using LiveCharts;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for WareHouseWindow.xaml
    /// </summary>
    
    public partial class WareHouseWindow : Window
    {
        AdminwsOfCloudAsowell _unitofwork;
        private LiveChartReceiptPage _lvChartReceiptPage;
        private IngredientPage _innIngredientPage;
        private Login login;
        private AdminRe curAdmin;
        private Employee curEmp;
        private InputReceiptNote inputReceipt;
        
        public WareHouseWindow()
        {
            InitializeComponent();
            _unitofwork = new AdminwsOfCloudAsowell();
            _innIngredientPage=new IngredientPage(_unitofwork);
            _lvChartReceiptPage = new LiveChartReceiptPage(_unitofwork);

            if (App.Current.Properties["AdLogin"] != null)
            {
                AdminRe getAdmin = App.Current.Properties["AdLogin"] as AdminRe;
                curAdmin = _unitofwork.AdminreRepository
                    .Get(ad => ad.Username.Equals(getAdmin.Username) && ad.Pass.Equals(getAdmin.Pass)).First();
                CUserChip.Content = curAdmin.Name;
            }
            else
            {
                Employee getEmp = App.Current.Properties["EmpLogin"] as Employee;
                curEmp = _unitofwork.EmployeeRepository
                    .Get(emp => emp.Username.Equals(getEmp.Username) && emp.Pass.Equals(getEmp.Pass)).First();
                CUserChip.Content = curEmp.Name;
            }
            
        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["AdLogin"] = null;
            App.Current.Properties["EmpLogin"] = null;
            login = new Login();
            this.Close();
            login.Show();
        }


        private void InputReceipt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (App.Current.Properties["AdLogin"] != null)
            {
                MessageBox.Show("Your role is not allowed to do this!");
                return;
            }
            inputReceipt=new InputReceiptNote(_unitofwork);
            myFrame.Navigate(inputReceipt);
        }

        private void ViewReceipt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(_lvChartReceiptPage);
        }

        private void ViewIngredient_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(_innIngredientPage);
        }
    }
}
