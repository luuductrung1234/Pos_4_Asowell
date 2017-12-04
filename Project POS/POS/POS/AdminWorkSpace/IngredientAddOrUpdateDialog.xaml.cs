using POS.Entities;
using POS.Repository.DAL;
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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for IngredientAddOrUpdateDialog.xaml
    /// </summary>
    public partial class IngredientAddOrUpdateDialog : Window
    {
        private readonly AdminwsOfCloudPOS _unitofwork;
        private Ingredient _ingre;
        private WareHouse _wah;

        public IngredientAddOrUpdateDialog(AdminwsOfCloudPOS unitofwork, Ingredient ingre)
        {
            _unitofwork = unitofwork;
            _ingre = ingre;
            InitializeComponent();

            initComboBox();
            if(_ingre != null)
            {
                initUpdateData();
            }

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initComboBox()
        {
            //init Use for
            cboUsefor.Items.Add("Drink");
            cboUsefor.Items.Add("Food");
            cboUsefor.Items.Add("Other");
            cboUsefor.SelectedItem = "Other";

            //init Ingredient Type
            cboIngreType.Items.Add("Other");
            cboIngreType.Items.Add("Dry");
            cboIngreType.Items.Add("Dairy");
            cboIngreType.Items.Add("Vegetable");
            cboIngreType.Items.Add("Fee");
            cboIngreType.SelectedItem = "Other";

            //init Unit Buy
            cboUnitBuy.Items.Add("g");
            cboUnitBuy.Items.Add("ml");
            cboUnitBuy.Items.Add("time");
            cboUnitBuy.SelectedItem = "g";
        }

        private void initUpdateData()
        {
            txtName.Text = _ingre.Name;
            txtInfo.Text = _ingre.Info;
            cboUsefor.SelectedIndex = _ingre.Usefor;
            cboIngreType.SelectedItem = _ingre.IgdType;
            cboUnitBuy.SelectedItem = _ingre.UnitBuy;
            txtStdPrice.Text = _ingre.StandardPrice.ToString();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //check name
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Trim().Length > 100)
            {
                IcNameValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcNameValid.Visibility = Visibility.Hidden;
            }
        }

        private void txtStdPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal price = decimal.Parse(txtStdPrice.Text.Trim());
            if(price < 0 || price > decimal.MaxValue)
            {
                IcPriceValid.Visibility = Visibility.Visible;
            }
            else
            {
                IcPriceValid.Visibility = Visibility.Hidden;
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check name
                string namee = txtName.Text.Trim();
                if (namee.Length == 0 || namee.Length > 50)
                {
                    MessageBox.Show("Name is not valid!");
                    txtName.Focus();
                    return;
                }

                //check info
                string info = txtInfo.Text.Trim();

                //check use for
                int usefor = cboUsefor.SelectedIndex;

                //check ingredient type
                string ingretype = cboIngreType.SelectedItem.ToString();

                //check unit buy
                string unitbuy = cboUnitBuy.SelectedItem.ToString();

                //check price
                decimal price = decimal.Parse(txtStdPrice.Text.Trim());
                if(price < 0 || price > decimal.MaxValue)
                {
                    MessageBox.Show("Standard price is not valid!");
                }

                if(_ingre == null) //Adding
                {
                    WareHouse newWare = new WareHouse
                    {

                    };

                    Ingredient newingre = new Ingredient
                    {
                        IgdId = "",
                        WarehouseId = "",
                        Name = namee,
                        Info = info,
                        Usefor = byte.Parse(usefor + ""),
                        IgdType = ingretype,
                        UnitBuy = unitbuy,
                        StandardPrice = price,
                        Deleted = 0
                    };


                }
                else //Updating
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong. Can not add or update ingredient. Please check the details again!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
