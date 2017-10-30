using POS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        }

        public void RefreshControl()
        {
            try
            {
                // lay ordernotedetails cua ban thu nhat
                var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
                var ordertabledetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrder;
                // chuyen product_id thanh product name
                var query = from orderdetails in ordernotedetails
                            join product in ProductData.PList
                            on orderdetails.Product_id equals product.Product_id
                            select new OrderDetails_Product_Joiner
                            {
                                OrderDetails = orderdetails,
                                Product = product
                            };


                // binding
                lvData.ItemsSource = query;
                txtDay.Text = ordertabledetails.ordertime.ToString("dd/MM/yyyy H:mm:ss");
                txtTable.Text = ordertabledetails.ordertable.ToString();
                loadTotalPrice();
                //loadCustomerOwner();
                loadChair();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        private void loadChair()
        {
            for(int i = 0; i < ((MainWindow)Application.Current.MainWindow).currentTable.ChairAmount; i++)
            {
                Button button = new Button();
                button.Name = "chair" + (i + 1);
                button.Content = (i + 1).ToString();
                button.Width = 24;
                button.Height = 24;
                Thickness m = button.Margin;
                m.Left = 5;
                m.Top = 5;
                button.Margin = m;
                button.SetValue(StyleProperty, FindResource("MaterialDesignFloatingActionMiniButton"));
                wp.Children.Add(button);
            }
        }

        public class OrderDetails_Product_Joiner : INotifyPropertyChanged
        {
            public OrderNoteDetails OrderDetails { get; set; }
            public Product Product { get; set; }

            public string ProductName
            {
                get
                {
                    return Product.Name;
                }
            }
            public float Price
            {
                get
                {
                    return Product.Price;
                }
            }
            public int Quan
            {
                get
                {
                    return OrderDetails.Quan;
                }
                set
                {
                    OrderDetails.Quan = value;
                    OnPropertyChanged("Quan");
                }
            }
            public ObservableCollection<string> StatusItems
            {
                get
                {
                    return OrderDetails.StatusItems;
                }
                set
                {
                    OrderDetails.StatusItems = value;
                    OnPropertyChanged("StatusItems");
                }
            }
            public string SelectedStats
            {
                get
                {
                    return OrderDetails.SelectedStats;
                }
                set
                {
                    OrderDetails.SelectedStats = value;
                    OnPropertyChanged("SelectedStats");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public void loadTotalPrice()
        {
            var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
            // chuyen product_id thanh product name
            var query_item_in_ordertails = from orderdetails in ordernotedetails
                                           join product in ProductData.PList
                                           on orderdetails.Product_id equals product.Product_id
                                           select new
                                           {
                                               item_quan = orderdetails.Quan,
                                               item_price = product.Price
                                           };

            float Total = 0;
            foreach (var item in query_item_in_ordertails)
            {
                Total = Total + item.item_quan * item.item_price;
            }
            txtTotal.Text = Total.ToString() + " VND";

        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
            OrderNoteDetails o = new OrderNoteDetails();
            if (ordernotedetails[index].Quan > 1)
            {
                o.Product_id = ordernotedetails[index].Product_id;
                o.Quan = ordernotedetails[index].Quan - 1;
                ordernotedetails[index] = o;
            }
            else
            {
                ordernotedetails.RemoveAt(index);
            }
            RefreshControl();
            loadTotalPrice();

        }

        private void bntPay_Click(object sender, RoutedEventArgs e)
        {
            loadTotalPrice();
            lvData.Items.Refresh();
            RefreshControl();
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
            var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
            OrderNoteDetails o = new OrderNoteDetails();
            InputNote inputnote = new InputNote(ordernotedetails[index].Note);
            if (ordernotedetails[index].Note == null || ordernotedetails[index].Note.Equals(inputnote.Note))
            {
                if (inputnote.ShowDialog() == true)
                {
                    o.Product_id = ordernotedetails[index].Product_id;
                    o.Quan = ordernotedetails[index].Quan;
                    o.SelectedStats = ordernotedetails[index].SelectedStats;
                    o.Note = inputnote.Note;
                    ordernotedetails[index] = o;
                }


            }
            else
            {
                inputnote.ShowDialog();
            }




        }
    }
}
