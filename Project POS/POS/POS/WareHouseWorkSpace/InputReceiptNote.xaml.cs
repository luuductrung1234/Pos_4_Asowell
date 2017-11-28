using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for InputReceiptNote.xaml
    /// </summary>
    public partial class InputReceiptNote : Page
    {
        private AdminwsOfAsowell _unitofork;
        private static ReceiptNote CurrentReceipt;
        private List<ReceiptNoteDetail> ReceiptDetailsList;


        public InputReceiptNote(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();

            lvDataIngredient.ItemsSource = _unitofork.IngredientRepository.Get(c => c.Deleted.Equals(0));

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
            if (ReceiptDetailsList[index].Quan > 1)
            {
                r.Quan = ReceiptDetailsList[index].Quan-1;
                r.IgdId = ReceiptDetailsList[index].IgdId;
                r.ItemPrice = ReceiptDetailsList[index].ItemPrice;
                ReceiptDetailsList[index] = r;
            }
            else
            {
                ReceiptDetailsList.RemoveAt(index);
            }
            lvDataReceipt.Items.Refresh();
            
        }
    
        private void lvDataIngredient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Ingredient ingredient=(Ingredient)lvDataIngredient.SelectedItem;
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
                foundIteminReceipt.Quan++;
            }
            //lvDataReceipt.ItemsSource = ReceiptDetailsList;
            lvDataReceipt.Items.Refresh();
            LoadReceiptData();
        }

        private void BntAdd_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentReceipt.Inday = DateTime.Now;
            _unitofork.ReceiptNoteRepository.Insert(CurrentReceipt);
            _unitofork.Save();


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
                CurrentReceipt.TotalAmount += details.ItemPrice * details.Quan;
            }
            txtTotalPrice.Text = string.Format("{0:0.000}", CurrentReceipt.TotalAmount);
        }

        private void TxtItemPrice_OnTextChanged(object sender, TextChangedEventArgs e)
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
            ReceiptDetailsList[index].ItemPrice = decimal.Parse((sender as TextBox).Text);

            LoadReceiptData();
        }
    }
}
