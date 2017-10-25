using POS.Model;
using System;
using System.Collections;
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
    /// Interaction logic for UcMenu.xaml
    /// </summary>
    public partial class UcMenu : UserControl
    {
        public UcMenu()
        {

            InitializeComponent();

            lvCategory.ItemsSource = ItemData.ilist;

        }


        private void lvCategory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                OrderNote o = new OrderNote();
                Item it = (Item)lvCategory.SelectedItem;
                var found = OrderData.Orderlist.FirstOrDefault(x => x.Name == it.Name);
                int i = OrderData.Orderlist.IndexOf(found);
                if (found == null)
                {
                    o.Name = it.Name;
                    o.Price = it.Price;
                    o.Count = 1;
                    OrderData.Orderlist.Add(o);
                }
                else
                {
                    o.Name = it.Name;
                    o.Price = it.Price;
                    o.Count = OrderData.Orderlist[i].Count + 1;
                    OrderData.Orderlist[i] = o;
                }
                lvCategory.UnselectAll();


            }


        }
    }
}
