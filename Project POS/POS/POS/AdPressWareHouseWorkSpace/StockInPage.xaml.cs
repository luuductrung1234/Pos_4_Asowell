using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using POS.Entities;
using POS.Repository.DAL;
using POS.AdPressWareHouseWorkSpace.Helper;
using POS.WareHouseWorkSpace;

namespace POS.AdPressWareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for StockInPage.xaml
    /// </summary>
    public partial class StockInPage : Page
    {
        private AdminwsOfCloudAPWH _unitofwork;
        private List<Stock> _stockList;
        internal StockIn _currentStockIn;
        internal List<StockInDetails> _stockInDetailsList;


        public StockInPage(AdminwsOfCloudAPWH unitofwork, List<Stock> stockList)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            _stockList = stockList;
            lvDataStock.ItemsSource = _stockList;

            _stockInDetailsList = new List<StockInDetails>();
            _currentStockIn = new StockIn()
            {
                AdId = (App.Current.Properties["AdLogin"] as AdminRe).AdId,
                StockInDetails = _stockInDetailsList
            };

            lvDataStockIn.ItemsSource = _stockInDetailsList;

            LoadStockInData();
        }

        private void LoadStockInData()
        {
            _currentStockIn.TotalAmount = 0;
            foreach (var details in _stockInDetailsList)
            {
                _currentStockIn.TotalAmount += details.ItemPrice * (decimal)details.Quan;
            }
            txtTotalPrice.Text = string.Format("{0:0.000}", _currentStockIn.TotalAmount);
        }




        /*********************************
         * Manipulate Each Stock
         *********************************/

        private void lvDataStock_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Stock stock = (Stock)lvDataStock.SelectedItem;
            if (stock == null)
                return;


            StockInDetails r = new StockInDetails();

            var foundIteminReceipt = _stockInDetailsList.FirstOrDefault(c => c.Sto_id.Equals(stock.StoId));
            if (foundIteminReceipt == null)
            {
                r.Sto_id = stock.StoId;
                r.Quan = 1;
                r.ItemPrice = stock.StandardPrice;
                _stockInDetailsList.Add(r);
            }
            else
            {
                foundIteminReceipt.Quan++;
            }
            lvDataStockIn.Items.Refresh();
            LoadStockInData();

        }


        /*********************************
         * Manipulate Each StockInDetails
         *********************************/

        private void txtQuan_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textboxQuan = sender as TextBox;


            int index;
            StockInDetails r = new StockInDetails();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataStockIn.ItemContainerGenerator.IndexFromContainer(dep);


            try
            {
                if (textboxQuan.Text == null || textboxQuan.Text.Length == 0)
                {
                    MessageBox.Show("The quantity of Input Stock can not be blank!");
                    if (!ErrorDetailsItem.Contains(index))
                        ErrorDetailsItem.Add(index);
                    return;
                }
                _stockInDetailsList[index].Quan = int.Parse(textboxQuan.Text);

                LoadStockInData();
                if (ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Remove(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when try to calculate the input data. Please check your input");
                if (!ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Add(index);
            }
        }

        private void txtItemPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textboxItemPrice = sender as TextBox;


            int index;
            StockInDetails r = new StockInDetails();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataStockIn.ItemContainerGenerator.IndexFromContainer(dep);


            try
            {
                if (textboxItemPrice.Text == null || textboxItemPrice.Text.Length == 0)
                {
                    MessageBox.Show("The Price of Input Stock can not be blank!");
                    if (!ErrorDetailsItem.Contains(index))
                        ErrorDetailsItem.Add(index);
                    return;
                }
                _stockInDetailsList[index].ItemPrice = decimal.Parse(textboxItemPrice.Text);

                LoadStockInData();
                if (ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Remove(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when try to calculate the input data. Please check your input");
                if (!ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Add(index);
            }
        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            int index;
            StockInDetails r = new StockInDetails();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataStockIn.ItemContainerGenerator.IndexFromContainer(dep);



            if (_stockInDetailsList[index].Quan > 1 && !ErrorDetailsItem.Contains(index))
            {
                r.Quan = _stockInDetailsList[index].Quan - 1;
                r.Sto_id = _stockInDetailsList[index].Sto_id;
                r.ItemPrice = _stockInDetailsList[index].ItemPrice;
                _stockInDetailsList[index] = r;
            }
            else
            {
                _stockInDetailsList.RemoveAt(index);
                if (ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Remove(index);
            }
            lvDataStockIn.Items.Refresh();
            LoadStockInData();
        }

        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            int index;
            StockInDetails r = new StockInDetails();
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;
            index = lvDataStockIn.ItemContainerGenerator.IndexFromContainer(dep);
            InputNote inputNote = new InputNote(_stockInDetailsList[index].Note);
            if ((_stockInDetailsList[index].Note == null || _stockInDetailsList[index].Note.Equals("") || _stockInDetailsList[index].Note.Equals(inputNote.Note)))
            {
                if (inputNote.ShowDialog() == true)
                {
                    r.Note = inputNote.Note;
                    r.Sto_id = _stockInDetailsList[index].Sto_id;
                    r.Quan = _stockInDetailsList[index].Quan;
                    r.ItemPrice = _stockInDetailsList[index].ItemPrice;
                    _stockInDetailsList[index] = r;
                }
            }
            else
            {
                inputNote.ShowDialog();
            }
            lvDataStockIn.Items.Refresh();
        }



        /*********************************
         * Form Manipulate
         *********************************/

        private void UpdateAPWareHouseContain()
        {
            foreach (var details in _currentStockIn.StockInDetails)
            {
                var stock = _stockList.FirstOrDefault(x => x.StoId.Equals(details.Sto_id));
                if (stock != null)
                {
                    APWareHouse wareHouse = _unitofwork.APWareHouseRepository.GetById(stock.APWarehouseId);
                    if (wareHouse != null)
                    {
                        wareHouse.Contain += details.Quan * UnitInTrans.ToUnitContain(stock.UnitOut);
                        _unitofwork.APWareHouseRepository.Update(wareHouse);
                    }
                }
            }
        }

        private List<int> ErrorDetailsItem = new List<int>();
        private void bntAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ErrorDetailsItem.Count != 0)
                {
                    MessageBox.Show("Something is not correct. Please check all your input again!");
                    return;
                }

                if (_currentStockIn.StockInDetails.Count == 0)
                {
                    MessageBox.Show("You have to choose the stock you want to put in");
                    return;
                }

                _currentStockIn.InTime = DateTime.Now;
                _unitofwork.StockInRepository.Insert(_currentStockIn);

                //ToDo: Update the contain value in Warehouse database
                UpdateAPWareHouseContain();

                _unitofwork.Save();


                _stockInDetailsList = new List<StockInDetails>();
                lvDataStockIn.ItemsSource = _stockInDetailsList;
                lvDataStockIn.Items.Refresh();

                _currentStockIn = new StockIn()
                {
                    AdId = (App.Current.Properties["AdLogin"] as AdminRe).AdId,
                    StockInDetails = _stockInDetailsList
                };


                LoadStockInData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when trying to input the new StockOut Receipt! May be you should reload this app or call for support!");
            }
        }

        private void bntDelAll_Click(object sender, RoutedEventArgs e)
        {
            ErrorDetailsItem.Clear();
            _stockInDetailsList.Clear();
            lvDataStockIn.Items.Refresh();
            LoadStockInData();
        }
    }
}
