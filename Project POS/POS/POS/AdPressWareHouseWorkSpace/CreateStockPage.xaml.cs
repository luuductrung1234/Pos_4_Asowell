using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.Entities;
using POS.Repository.DAL;
using POS.AdPressWareHouseWorkSpace.Helper;
using POS.WareHouseWorkSpace;
using System.Linq;

namespace POS.AdPressWareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for CreateStockPage.xaml
    /// </summary>
    public partial class CreateStockPage : Page
    {
        private AdminwsOfCloudAPWH _unitofwork;
        private List<Stock> _stockList;

        Stock _currentNewStock = new Stock();

        public CreateStockPage(AdminwsOfCloudAPWH unitofwork, List<Stock> stockList)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            _stockList = stockList;
            lvStock.ItemsSource = _stockList;

            initComboBox();
        }

        private void initComboBox()
        {
            cboGroup.Items.Add(StockGroup.All);
            cboGroup.Items.Add(StockGroup.Cosmetics);
            cboGroup.Items.Add(StockGroup.SpaVoucher);
            cboGroup.Items.Add(StockGroup.GymVoucher);
            cboGroup.Items.Add(StockGroup.ResVoucher);
            cboGroup.Items.Add(StockGroup.TravVoucher);
            cboGroup.Items.Add(StockGroup.Food);
            cboGroup.Items.Add(StockGroup.Agricultural);
            cboGroup.Items.Add(StockGroup.Watch);
            cboGroup.Items.Add(StockGroup.TopTen);
            cboGroup.SelectedItem = StockGroup.All;

            cboStockGroup.Items.Add(StockGroup.Cosmetics);
            cboStockGroup.Items.Add(StockGroup.SpaVoucher);
            cboStockGroup.Items.Add(StockGroup.GymVoucher);
            cboStockGroup.Items.Add(StockGroup.ResVoucher);
            cboStockGroup.Items.Add(StockGroup.TravVoucher);
            cboStockGroup.Items.Add(StockGroup.Food);
            cboStockGroup.Items.Add(StockGroup.Agricultural);
            cboStockGroup.Items.Add(StockGroup.Watch);
            cboStockGroup.Items.Add(StockGroup.TopTen);
            cboStockGroup.SelectedItem = StockGroup.Cosmetics;

            cboUnitIn.Items.Add("pcs");
            cboUnitIn.SelectedItem = "pcs";

            cboUnitOut.Items.Add("pcs");
            cboUnitOut.SelectedItem = "pcs";
        }




        /*********************************
        * Controls
        *********************************/

        private void cboGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedI = (sender as ComboBox).SelectedIndex;

            if (filter.Length == 0)
            {
                if (selectedI < 0 || (sender as ComboBox).SelectedValue.Equals(StockGroup.All))
                {
                    lvStock.ItemsSource = _stockList;
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(x => x.Group.Equals((int)(sender as ComboBox).SelectedItem));
                }
            }
            else
            {
                if (selectedI < 0 || (sender as ComboBox).SelectedValue.Equals(StockGroup.All))
                {
                    lvStock.ItemsSource = _stockList.Where(x => x.Name.Contains(filter));
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(x => x.Group.Equals((int)(sender as ComboBox).SelectedItem) && x.Name.Contains(filter));
                }
            }
        }

        Stock _selectedStock;
        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            _selectedStock = lvStock.SelectedItem as Stock;

            if (lvStock.SelectedItem == null)
            {
                MessageBox.Show("Stock must be selected to update! Choose again!");
                return;
            }

            lvStock.UnselectAll();
            lvStock.Items.Refresh();
            btnUpdate.Visibility = Visibility.Visible;


            //put data to form
            txtName.Text = _selectedStock.Name;
            txtInfo.Text = _selectedStock.Info;

            switch (_selectedStock.Group)
            {
                case (int)StockGroup.Cosmetics:
                    cboStockGroup.SelectedItem = StockGroup.Cosmetics;
                    break;
                case (int)StockGroup.SpaVoucher:
                    cboStockGroup.SelectedItem = StockGroup.SpaVoucher;
                    break;
                case (int)StockGroup.GymVoucher:
                    cboStockGroup.SelectedItem = StockGroup.GymVoucher;
                    break;
                case (int)StockGroup.ResVoucher:
                    cboStockGroup.SelectedItem = StockGroup.ResVoucher;
                    break;
                case (int)StockGroup.TravVoucher:
                    cboStockGroup.SelectedItem = StockGroup.TravVoucher;
                    break;
                case (int)StockGroup.Food:
                    cboStockGroup.SelectedItem = StockGroup.Food;
                    break;
                case (int)StockGroup.Agricultural:
                    cboStockGroup.SelectedItem = StockGroup.Agricultural;
                    break;
                case (int)StockGroup.Watch:
                    cboStockGroup.SelectedItem = StockGroup.Watch;
                    break;
                case (int)StockGroup.TopTen:
                    cboStockGroup.SelectedItem = StockGroup.TopTen;
                    break;
            }
            txtBarterCode.Text = _selectedStock.BarterCode;
            txtBarterName.Text = _selectedStock.BarterName;
            cboUnitIn.SelectedItem = _selectedStock.UnitIn;
            cboUnitOut.SelectedItem = _selectedStock.UnitOut;
            txtSupplier.Text = _selectedStock.Supplier;
            txtPrice.Text = _selectedStock.StandardPrice.ToString();
        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {
            if (lvStock.SelectedItem == null)
            {
                MessageBox.Show("Stock must be selected to delete! Choose again!");
                return;
            }

            var delStock = lvStock.SelectedItem as Stock;
            if(delStock != null)
            {
                MessageBoxResult delMess = MessageBox.Show("This action will delete all following stock details! Do you want to delete " + delStock.Name + "(" + delStock.StoId + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
                if (delMess == MessageBoxResult.Yes)
                {
                    delStock.Deleted = 1;


                    _unitofwork.StockRepository.Update(delStock);
                    _unitofwork.Save();

                    // refesh data
                    ((APWareHouseWindow)Window.GetWindow(this)).Refresh_Tick(null, new EventArgs());
                    lvStock.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please choose stock you want to delete and try again!");
            }
        }


        /*********************************
        * Manipulate Search Box
        *********************************/

        private void SearchIBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedStock = cboGroup.SelectedIndex;

            if (selectedStock < 0 || cboGroup.SelectedValue.Equals(StockGroup.All))
            {
                if (filter.Length == 0)
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Deleted.Equals(0));
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
            else
            {
                if (filter.Length == 0)
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Group.Equals((int)cboGroup.SelectedItem) && p.Deleted.Equals(0));
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Group.Equals((int)cboGroup.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
        }

        private void SearchIBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedStock = cboGroup.SelectedIndex;

            if (selectedStock < 0 || cboGroup.SelectedValue.Equals(StockGroup.All))
            {
                if (filter.Length == 0)
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Deleted.Equals(0));
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
            else
            {
                if (filter.Length == 0)
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Group.Equals((int)cboGroup.SelectedItem) && p.Deleted.Equals(0));
                }
                else
                {
                    lvStock.ItemsSource = _stockList.Where(p => p.Group.Equals((int)cboGroup.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
        }

        private void SearchIBox_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }



        /*********************************
        * Manipulate Form
        *********************************/

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }

        private void cboStockGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_currentNewStock.Group = (int)cboStockGroup.SelectedItem;
        }

        private void cboUnitIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_currentNewStock.UnitIn = cboUnitIn.SelectedItem.ToString();
        }

        private void cboUnitOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_currentNewStock.UnitOut = cboUnitOut.SelectedItem.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check name
                string name = txtName.Text.Trim();
                if (name.Length == 0)
                {
                    MessageBox.Show("Name is not valid!");
                    txtName.Focus();
                    return;
                }

                //check info
                string info = txtInfo.Text.Trim();
                //check barter code
                string barterCode = txtBarterCode.Text;
                //check barter name
                string barterName = txtBarterName.Text;

                int group = (int)cboStockGroup.SelectedItem;
                string unitIn = cboUnitIn.SelectedItem.ToString();
                string unitOut = cboUnitOut.SelectedItem.ToString();

                //check supplier
                string supplier = txtSupplier.Text;

                //check price
                decimal price = decimal.Parse(txtPrice.Text.Trim());



                APWareHouse newWareHouse = new APWareHouse
                {
                    APWarehouseId = "",
                    Name = "",
                    Contain = 0,
                    StandardContain = 100
                };

                _unitofwork.APWareHouseRepository.Insert(newWareHouse);
                _unitofwork.Save();



                _currentNewStock.APWarehouseId = newWareHouse.APWarehouseId;
                _currentNewStock.Name = name;
                _currentNewStock.Info = info;
                _currentNewStock.Group = group;
                _currentNewStock.BarterCode = barterCode;
                _currentNewStock.BarterName = barterName;
                _currentNewStock.UnitIn = unitIn;
                _currentNewStock.UnitOut = unitOut;
                _currentNewStock.Supplier = supplier;
                _currentNewStock.StandardPrice = price;

                _unitofwork.StockRepository.Insert(_currentNewStock);
                _unitofwork.Save();


                MessageBox.Show("Add new stock " + _currentNewStock.Name + "(" + _currentNewStock.StoId + ") successful!");
                clearAllData();

                // refesh data
                ((APWareHouseWindow)Window.GetWindow(this)).Refresh_Tick(null, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. Can not add new stock. Please check your input again!");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearAllData();
        }

        private void clearAllData()
        {
            txtName.Text = "";
            txtInfo.Text = "";
            cboStockGroup.SelectedItem = StockGroup.Cosmetics;
            txtBarterCode.Text = "";
            txtBarterName.Text = "";
            cboUnitIn.SelectedItem = "pcs";
            cboUnitOut.SelectedItem = "pcs";
            txtSupplier.Text = "";
            txtPrice.Text = "";

            _currentNewStock = new Stock();
            lvStock.Items.Refresh();
            btnUpdate.Visibility = Visibility.Hidden;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //check name
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Name is not valid!");
                txtName.Focus();
                return;
            }

            //check info
            string info = txtInfo.Text.Trim();
            //check barter code
            string barterCode = txtBarterCode.Text;
            //check barter name
            string barterName = txtBarterName.Text;

            int group = (int)cboStockGroup.SelectedItem;
            string unitIn = cboUnitIn.SelectedItem.ToString();
            string unitOut = cboUnitOut.SelectedItem.ToString();

            //check supplier
            string supplier = txtSupplier.Text;

            //check price
            decimal price = decimal.Parse(txtPrice.Text.Trim());


            _selectedStock.Name = name;
            _selectedStock.Info = info;
            _selectedStock.Group = group;
            _selectedStock.BarterCode = barterCode;
            _selectedStock.BarterName = barterName;
            _selectedStock.UnitIn = unitIn;
            _selectedStock.UnitOut = unitOut;
            _selectedStock.Supplier = supplier;
            _selectedStock.StandardPrice = price;

            _unitofwork.StockRepository.Update(_selectedStock);
            _unitofwork.Save();


            MessageBox.Show("Update stock " + _selectedStock.Name + "(" + _selectedStock.StoId + ") successful!");
            clearAllData();

            btnUpdate.Visibility = Visibility.Hidden;
            _selectedStock = null;
            // refesh data
            ((APWareHouseWindow)Window.GetWindow(this)).Refresh_Tick(null, new EventArgs());
        }
    }
}
