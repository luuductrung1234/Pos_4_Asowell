using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using log4net;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for WareHouseWindow.xaml
    /// </summary>

    public partial class WareHouseWindow : Window
    {
        AdminwsOfCloudPOS _unitofwork;
        private LiveChartReceiptPage _lvChartReceiptPage;
        private IngredientPage _innIngredientPage;
        private InputReceiptNote _inputReceipt;
        private Login login;
        private AdminRe curAdmin;
        private Employee curEmp;
        

        private List<Ingredient> IngdList;

        private static readonly ILog AppLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WareHouseWindow()
        {
            InitializeComponent();

            try
            {
                _unitofwork = new AdminwsOfCloudPOS();
                IngdList = _unitofwork.IngredientRepository
                    .Get(c => c.Deleted.Equals(0), includeProperties: "WareHouse").ToList();

                _innIngredientPage = new IngredientPage(_unitofwork, IngdList);
                _lvChartReceiptPage = new LiveChartReceiptPage(_unitofwork);



                if (App.Current.Properties["AdLogin"] != null)
                {
                    AdminRe getAdmin = App.Current.Properties["AdLogin"] as AdminRe;
                    List<AdminRe> adList = _unitofwork.AdminreRepository.Get().ToList();
                    curAdmin = adList.FirstOrDefault(x =>
                        x.Username.Equals(getAdmin.Username) && x.DecryptedPass.Equals(getAdmin.DecryptedPass));
                    CUserChip.Content = curAdmin.Name;
                }
                else
                {
                    Employee getEmp = App.Current.Properties["EmpLogin"] as Employee;
                    List<Employee> empList = _unitofwork.EmployeeRepository.Get().ToList();
                    curEmp = empList.FirstOrDefault(x =>
                        x.Username.Equals(getEmp.Username) && x.DecryptedPass.Equals(getEmp.DecryptedPass));
                    CUserChip.Content = curEmp.Name;
                    _inputReceipt = new InputReceiptNote(_unitofwork, IngdList);
                }


                DispatcherTimer RefreshTimer = new DispatcherTimer();
                RefreshTimer.Tick += Refresh_Tick;
                RefreshTimer.Interval = new TimeSpan(0, 1, 0);
                RefreshTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: \n" + ex.Message);
                AppLog.Error(ex);
            }
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
