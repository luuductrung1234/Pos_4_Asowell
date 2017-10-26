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
        public Table currentTable { get; set; }

        public UcMenu()
        {

            InitializeComponent();

            lvCategory.ItemsSource = ProductData.PList;

        }


        private void lvCategory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).currentTable != null)
            {
                var item = (sender as ListView).SelectedItem;
                if (item != null)
                {
                    OrderNoteDetails o = new OrderNoteDetails();
                    Product it = (Product)lvCategory.SelectedItem;
                    var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
                    var found = ordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));
                    int i = ordernotedetails.IndexOf(found);
                    if (found == null)
                    {
                        o.Product_id = it.Product_id;
                        o.Quan = 1;
                        ordernotedetails.Add(o);
                    }
                    else
                    {
                        o.Product_id = it.Product_id;
                        o.Quan = ordernotedetails[i].Quan + 1;
                        o.SelectedStats = ordernotedetails[i].SelectedStats;


                        ordernotedetails[i] = o;
                    }
                    lvCategory.UnselectAll();

                    ((MainWindow)Application.Current.MainWindow).en.ucOrder.RefreshControl();
                }
            }

        }
    }
}
