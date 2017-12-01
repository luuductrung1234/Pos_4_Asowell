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
        private InputReceiptNote _inputReceipt;
        private Login login;
        private AdminRe curAdmin;
        private Employee curEmp;
        

        private List<Ingredient> IngdList;

        public WareHouseWindow()
        {
            InitializeComponent();

            _unitofwork = new AdminwsOfCloudAsowell();
            IngdList = _unitofwork.IngredientRepository.Get(c => c.Deleted.Equals(0), includeProperties: "WareHouse").ToList();

            _innIngredientPage =new IngredientPage(_unitofwork, IngdList);
            _lvChartReceiptPage = new LiveChartReceiptPage(_unitofwork);

            
            _inputReceipt = new InputReceiptNote(_unitofwork, IngdList);


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
            

            DispatcherTimer RefreshTimer = new DispatcherTimer();
            RefreshTimer.Tick += Refresh_Tick;
            RefreshTimer.Interval = new TimeSpan(0, 0, 20);
            RefreshTimer.Start();
        }



        private void Refresh_Tick(object sender, EventArgs e)
        {
            foreach (var ingd in _unitofwork.IngredientRepository.Get(includeProperties: "WareHouse"))
            {
                if (ingd.Deleted == 1)
                {
                    var deletedIngd = IngdList.FirstOrDefault(x => x.IgdId.Equals(ingd.IgdId));
                    if (deletedIngd != null)
                    {
                        IngdList.Remove(deletedIngd);
                    }
                    continue;
                }

                var curIngd = IngdList.FirstOrDefault(x => x.IgdId.Equals(ingd.IgdId));
                if (curIngd == null)
                {
                    IngdList.Add(ingd);
                }
                else
                {
                    curIngd.Name = ingd.Name;
                    curIngd.Info = ingd.Info;
                    curIngd.Usefor = ingd.Usefor;
                    curIngd.IgdType = ingd.IgdType;
                    curIngd.UnitBuy = ingd.UnitBuy;
                    curIngd.StandardPrice = ingd.StandardPrice;

                    curIngd.WareHouse.Contain = ingd.WareHouse.Contain;
                    curIngd.WareHouse.StandardContain = ingd.WareHouse.StandardContain;
                }
            }

            _innIngredientPage.lvItem.Items.Refresh();
            _inputReceipt.lvDataIngredient.Items.Refresh();
            //_innIngredientPage = new IngredientPage(_unitofwork, IngdList);
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
            
            myFrame.Navigate(_inputReceipt);
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
