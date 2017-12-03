using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using POS.Entities;
using POS.Repository.DAL;
using POS.WareHouseWorkSpace.Helper;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for InputReceiptNote.xaml
    /// </summary>
    public partial class InputReceiptNote : Page
    {
        private AdminwsOfCloudPOS _unitofwork;
        private List<Ingredient> IngdList;
        internal ReceiptNote CurrentReceipt;
        internal List<ReceiptNoteDetail> ReceiptDetailsList;

        private static readonly string ORTHER_PERCHAGSE_ID = "IGD0000047";


        public InputReceiptNote(AdminwsOfCloudPOS unitofwork, List<Ingredient> ingdList)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            this.IngdList = ingdList;
            lvDataIngredient.ItemsSource = IngdList;

            ReceiptDetailsList = new List<ReceiptNoteDetail>();
            CurrentReceipt = new ReceiptNote()
            {
                EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId,
                ReceiptNoteDetails = ReceiptDetailsList
            };
            lvDataReceipt.ItemsSource = ReceiptDetailsList;

            LoadReceiptData();
        }


        private void BntDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int index;
            ReceiptNoteDetail r = new ReceiptNoteDetail();
            DependencyObject dep = (DependencyObject) e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataReceipt.ItemContainerGenerator.IndexFromContainer(dep);



            if (ReceiptDetailsList[index].Quan > 1 && !ErrorDetailsItem.Contains(index))
            {
                r.Quan = ReceiptDetailsList[index].Quan-1;
                r.IgdId = ReceiptDetailsList[index].IgdId;
                r.ItemPrice = ReceiptDetailsList[index].ItemPrice;
                ReceiptDetailsList[index] = r;
            }
            else
            {
                ReceiptDetailsList.RemoveAt(index);
                if (ErrorDetailsItem.Contains(index))
                    ErrorDetailsItem.Remove(index);
            }
            lvDataReceipt.Items.Refresh();
            LoadReceiptData();
        }
    
        private void lvDataIngredient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Ingredient ingredient=(Ingredient)lvDataIngredient.SelectedItem;
            if (ingredient == null)
                return;

            ReceiptNoteDetail r=new ReceiptNoteDetail();

            var foundIteminReceipt = ReceiptDetailsList.FirstOrDefault(c => c.IgdId.Equals(ingredient.IgdId));
            if (foundIteminReceipt==null)
            {
                r.IgdId = ingredient.IgdId;
                r.Quan = 1;
                r.ItemPrice = ingredient.StandardPrice;
                ReceiptDetailsList.Add(r);
            }
            else
            {
                if (ingredient.IgdId.Equals(ORTHER_PERCHAGSE_ID))   // only allow input the Orther Perchagse once per Receipt Note
                    return;
                foundIteminReceipt.Quan++;
            }
            lvDataReceipt.Items.Refresh();
            LoadReceiptData();
        }

        private List<int> ErrorDetailsItem = new List<int>();
        private void BntAdd_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ErrorDetailsItem.Count != 0)
                {
                    MessageBox.Show("Something is not correct. Please check all your input again!");
                    return;
                }

                if (CurrentReceipt.ReceiptNoteDetails.Count == 0)
                {
                    MessageBox.Show("You have to choose the ingredient you want to input before add");
                    return;
                }

                // check if the Receipt Note have input Other Perchagse, must require the Note
                foreach (var details in CurrentReceipt.ReceiptNoteDetails.ToList())
                {
                    if (details.IgdId.Equals(ORTHER_PERCHAGSE_ID) && string.IsNullOrEmpty(details.Note))
                    {
                        MessageBox.Show(
                            "You have inputed the \"Orther Purchase\" in your Receipt. Please input the detail description in note before save data!");
                        return;
                    }
                }

                CurrentReceipt.Inday = DateTime.Now;
                _unitofwork.ReceiptNoteRepository.Insert(CurrentReceipt);
                
                //ToDo: Update the contain value in Warehouse database
                UpdateWareHouseContain();

                _unitofwork.Save();


                ReceiptDetailsList = new List<ReceiptNoteDetail>();
                lvDataReceipt.ItemsSource = ReceiptDetailsList;
                lvDataReceipt.Items.Refresh();
                CurrentReceipt = new ReceiptNote()
                {
                    EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId,
                    ReceiptNoteDetails = ReceiptDetailsList
                };
                
                LoadReceiptData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when trying to input the new Receipt! May be you should reload this app or call for support!");
            }
        }

        private void UpdateWareHouseContain()
        {
            foreach (var details in CurrentReceipt.ReceiptNoteDetails)
            {
                if(details.IgdId.Equals(ORTHER_PERCHAGSE_ID))
                    continue;
                

                var ingd = IngdList.FirstOrDefault(x => x.IgdId.Equals(details.IgdId));
                if (ingd != null)
                {
                    WareHouse wareHouse = _unitofwork.WareHouseRepository.GetById(ingd.WarehouseId);
                    if (wareHouse != null)
                    {
                        wareHouse.Contain += details.Quan * UnitBuyTrans.ToUnitContain(ingd.UnitBuy);
                        _unitofwork.WareHouseRepository.Update(wareHouse);
                    }
                }
            }
        }

        private void BntDelAll_OnClick(object sender, RoutedEventArgs e)
        {
            ErrorDetailsItem.Clear();
            ReceiptDetailsList.Clear();
            lvDataReceipt.Items.Refresh();
            LoadReceiptData();
        }

        private void BntEdit_OnClick(object sender, RoutedEventArgs e)
        {
            int index;
            ReceiptNoteDetail r = new ReceiptNoteDetail();
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;
            index = lvDataReceipt.ItemContainerGenerator.IndexFromContainer(dep);
            InputNote inputNote=new InputNote(ReceiptDetailsList[index].Note);
            if ((ReceiptDetailsList[index].Note==null||ReceiptDetailsList[index].Note.Equals("") || ReceiptDetailsList[index].Note.Equals(inputNote.Note)))
            {
                if (inputNote.ShowDialog() == true)
                {
                    r.Note = inputNote.Note;
                    r.IgdId = ReceiptDetailsList[index].IgdId;
                    r.Quan = ReceiptDetailsList[index].Quan;
                    r.ItemPrice = ReceiptDetailsList[index].ItemPrice;
                    ReceiptDetailsList[index] = r;
                }
            }
            else
            {
                inputNote.ShowDialog();
            }
            lvDataReceipt.Items.Refresh();
        }

        private void LoadReceiptData()
        {
            CurrentReceipt.TotalAmount = 0;
            foreach (var details in ReceiptDetailsList)
            {
                CurrentReceipt.TotalAmount += details.ItemPrice * (decimal)details.Quan;
            }
            txtTotalPrice.Text = string.Format("{0:0.000}", CurrentReceipt.TotalAmount);
        }

        private void TxtItemPrice_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textboxItemPrice = sender as TextBox;


            int index;
            ReceiptNoteDetail r = new ReceiptNoteDetail();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataReceipt.ItemContainerGenerator.IndexFromContainer(dep);


            try
            {
                if (textboxItemPrice.Text == null || textboxItemPrice.Text.Length == 0)
                {
                    MessageBox.Show("The Price of Input Ingredients can not be blank!");
                    if (!ErrorDetailsItem.Contains(index))
                        ErrorDetailsItem.Add(index);
                    return;
                }
                ReceiptDetailsList[index].ItemPrice = decimal.Parse(textboxItemPrice.Text);

                LoadReceiptData();
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

        private void TxtQuan_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textboxQuan = sender as TextBox;


            int index;
            ReceiptNoteDetail r = new ReceiptNoteDetail();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            index = lvDataReceipt.ItemContainerGenerator.IndexFromContainer(dep);


            try
            {
                if (textboxQuan.Text == null || textboxQuan.Text.Length == 0)
                {
                    MessageBox.Show("The quantity of Input Ingredients can not be blank!");
                    if(!ErrorDetailsItem.Contains(index))
                        ErrorDetailsItem.Add(index);
                    return;
                }
                ReceiptDetailsList[index].Quan = float.Parse(textboxQuan.Text);

                LoadReceiptData();
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
    }
}
