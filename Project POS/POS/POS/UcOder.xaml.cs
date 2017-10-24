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
            lvData.ItemsSource = OrderData.Orderlist;

        }

        public  void loadDataTotal()
        {

            int Total = 0;
            foreach (var item in OrderData.Orderlist)
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
            
            OrderData.Orderlist.RemoveAt(index);
            loadDataTotal();

        }

        private void bntView_Click(object sender, RoutedEventArgs e)
        {
            ViewBill v = new ViewBill();
            v.Show();
        }

        private void bntPay_Click(object sender, RoutedEventArgs e)
        {
            loadDataTotal();
        }
    }
}
