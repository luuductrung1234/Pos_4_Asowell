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
        private List<ReceiptNoteDetail> ReceiptList;
        public InputReceiptNote(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();
            lvDataIngredient.ItemsSource = _unitofork.IngredientRepository.Get();
             ReceiptList = new List<ReceiptNoteDetail>();
            lvDataReceipt.ItemsSource = ReceiptList;
        }

        private void BntAddnew_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
            if (ReceiptList[index].Quan > 1)
            {
                r.Quan = ReceiptList[index].Quan-1;
                r.IgdId = ReceiptList[index].IgdId;
                r.ItemPrice = ReceiptList[index].ItemPrice;
                ReceiptList[index] = r;
            }
            else
            {
                ReceiptList.RemoveAt(index);
            }
            lvDataReceipt.Items.Refresh();
            
        }
    


        private void lvDataIngredient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Ingredient ingredient=(Ingredient)lvDataIngredient.SelectedItem;
            ReceiptNoteDetail r=new ReceiptNoteDetail();

            var foundIteminReceipt = ReceiptList.Where(c => c.IgdId.Equals(ingredient.IgdId)).FirstOrDefault();
            if (foundIteminReceipt==null)
            {
                r.IgdId = ingredient.IgdId;
                r.Quan = 1;
                r.ItemPrice = ingredient.StandardPrice;
                ReceiptList.Add(r);
            }
            else
            {
                foundIteminReceipt.Quan++;
            }
            lvDataReceipt.ItemsSource = ReceiptList;
            lvDataReceipt.Items.Refresh();
        }

        private void BntAdd_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BntDelAll_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BntEdit_OnClick(object sender, RoutedEventArgs e)
        {
            int index;
            ReceiptNoteDetail r = new ReceiptNoteDetail();
            DependencyObject dep = (DependencyObject)e.OriginalSource;
        }

       
    }
}
