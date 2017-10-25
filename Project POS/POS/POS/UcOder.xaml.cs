using POS.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS
{
    /// <summary>
    /// Interaction logic for UcOder.xaml
    /// </summary>
    public partial class UcOder : UserControl
    {
        public UcOder()
        {
            InitializeComponent();
            loadDataTotal();
            lvData.ItemsSource = OrderDetailsData.OrderDetailslist;
            txtDay.Text = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
        }

        public  void loadDataTotal()
        {

            int Total = 0;
            foreach (var item in OrderDetailsData.OrderDetailslist)
            {
                Total = Total + item.TotalPrice;
            }
            txtTotal.Text = Total.ToString()+" VND";

        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
            OrderNote o = new OrderNote();
            if(OrderDetailsData.OrderDetailslist[index].Count>1)
            {
                o.Name = OrderDetailsData.OrderDetailslist[index].Name;
                o.Price = OrderDetailsData.OrderDetailslist[index].Price;
                o.Count = OrderDetailsData.OrderDetailslist[index].Count - 1;
                OrderDetailsData.OrderDetailslist[index] = o;
            }
            else
            {
                OrderDetailsData.OrderDetailslist.RemoveAt(index);
            }
           
            loadDataTotal();

        }
        

        private void bntPay_Click(object sender, RoutedEventArgs e)
        {
            loadDataTotal();
            lvData.Items.Refresh();
        }

     

        private void txtCoutn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            if (currentTextBox.IsReadOnly)
            {
                currentTextBox.IsReadOnly = false;
            }
               
            else
                currentTextBox.IsReadOnly = true;

        }

        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
            OrderNote o = new OrderNote();
            InputNote inputnote = new InputNote(OrderDetailsData.OrderDetailslist[index].Note);
            if(OrderDetailsData.OrderDetailslist[index].Note==null|| OrderDetailsData.OrderDetailslist[index].Note.Equals(inputnote.Note))
            {
                if (inputnote.ShowDialog() == true)
                {
                    o.Name = OrderDetailsData.OrderDetailslist[index].Name;
                    o.Price = OrderDetailsData.OrderDetailslist[index].Price;
                    o.Count = OrderDetailsData.OrderDetailslist[index].Count;
                    o.Note = inputnote.Note;
                    OrderDetailsData.OrderDetailslist[index] = o;
                }

                
            }
            else
            {
                inputnote.ShowDialog();
            }
            



        }
    }
}
