using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for InputReceiptNote.xaml
    /// </summary>
    public partial class InputReceiptNote : Page
    {
        private AdminwsOfCloudAsowell _unitofwork;
        private static ReceiptNote CurrentReceipt;
        private List<ReceiptNoteDetail> ReceiptDetailsList;

        private static readonly string ORTHER_PERCHAGSE_ID = "IGD0000047";


        public InputReceiptNote(AdminwsOfCloudAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            lvDataIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(c => c.Deleted.Equals(0));

            ReceiptDetailsList = new List<ReceiptNoteDetail>();
            lvDataReceipt.ItemsSource = ReceiptDetailsList;

            CurrentReceipt = new ReceiptNote()
            {
                
                EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId,
                ReceiptNoteDetails = ReceiptDetailsList
            };
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
            
        }
    
        private void lvDataIngredient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Ingredient ingredient=(Ingredient)lvDataIngredient.SelectedItem;
            if (ingredient == null)
                return;

            ReceiptNoteDetail r=new ReceiptNoteDetail();

            var foundIteminReceipt = ReceiptDetailsList.Where(c => c.IgdId.Equals(ingredient.IgdId)).FirstOrDefault();
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
            //lvDataReceipt.ItemsSource = ReceiptDetailsList;
            lvDataReceipt.Items.Refresh();
            LoadReceiptData();
        }

        private List<int> ErrorDetailsItem = new List<int>();
        private void BntAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (ErrorDetailsItem.Count != 0)
            {
                MessageBox.Show("Something is not correct. Please check all your input again!");
                return;
            }
               

            // check if the Receipt Note have input Other Perchagse, must require the Note
            foreach (var details in CurrentReceipt.ReceiptNoteDetails)
            {
                if (details.IgdId.Equals(ORTHER_PERCHAGSE_ID) && string.IsNullOrEmpty(details.Note))
                {
                    MessageBox.Show("You have inputed the \"Orther Purchase\" in your Receipt. Please input the detail description in note before save data!");
                    return;
                }
            }

            CurrentReceipt.Inday = DateTime.Now;
            _unitofwork.ReceiptNoteRepository.Insert(CurrentReceipt);
            _unitofwork.Save();


            //ToDo: Update the contain value in Warehouse database


            ReceiptDetailsList = new List<ReceiptNoteDetail>();
            lvDataReceipt.ItemsSource = ReceiptDetailsList;
            CurrentReceipt = new ReceiptNote()
            {

                EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId,
                ReceiptNoteDetails = ReceiptDetailsList
            };
            LoadReceiptData();
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
