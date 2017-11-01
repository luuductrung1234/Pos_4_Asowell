using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using POS.Model;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for UcOder.xaml
    /// </summary>
    public partial class UcOder : UserControl
    {
        public UcOder()
        {
            InitializeComponent();

            this.Loaded += UcOder_Loaded;
        }

        private void UcOder_Loaded(object sender, RoutedEventArgs e)
        {
            loadTableChairData();
            loadCustomerOwner();
        }

        public void RefreshControl()
        {
            try
            {
                // lay ordernotedetails cua ban thu nhat
                var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
                var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber) && x.TableOfChair.Equals(((MainWindow)Window.GetWindow(this)).currentChair.TableOfChair));
                var chairordernotedetails = foundchair.ChairOrderDetails;

                // chuyen product_id thanh product name
                var query = from orderdetails in chairordernotedetails
                            join product in ProductData.PList
                            on orderdetails.Product_id equals product.Product_id

                            select new OrderDetails_Product_Joiner
                            {
                                OrderDetails = orderdetails,
                                Product = product
                            };

                // binding
                lvData.ItemsSource = query;
                loadTotalPrice();
                ReadWriteData.writeToBinFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        public void RefreshControlAllChair()
        {
            // lay ordernotedetails cua ban thu nhat
            var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
            List<OrderNoteDetails> ordernotedetailstemp = new List<OrderNoteDetails>();
            var chairordernotedetails = ordernotedetailstemp;
            foreach (Chair ch in chairoftable)
            {
                if (ch != null)
                {
                    chairordernotedetails = chairordernotedetails.Concat(ch.ChairOrderDetails).ToList();
                }
            }

            // chuyen product_id thanh product name
            var query = from orderdetails in chairordernotedetails
                        join product in ProductData.PList
                        on orderdetails.Product_id equals product.Product_id

                        select new OrderDetails_Product_Joiner
                        {
                            OrderDetails = orderdetails,
                            Product = product
                        };

            // binding
            lvData.ItemsSource = query;
            loadTotalPrice();
        }

        ToggleButton curChair;
        private void loadTableChairData()
        {
            if (((MainWindow)Window.GetWindow(this)).currentTable == null)
            {
                return;
            }

            var ordertabledetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrder;
            txtDay.Text = ordertabledetails.ordertime.ToString("dd/MM/yyyy H:mm:ss");
            txtTable.Text = ordertabledetails.ordertable.ToString();
            wp.Children.Clear();
            foreach (Chair ch in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
            {
                ToggleButton button = new ToggleButton();
                button.Name = "chair" + (ch.ChairNumber);
                button.Content = (ch.ChairNumber).ToString();
                button.Width = 24;
                button.Height = 24;
                Thickness m = button.Margin;
                m.Left = 5;
                m.Top = 5;
                button.Margin = m;
                button.SetValue(StyleProperty, FindResource("MaterialDesignActionToggleButton"));
                button.Checked += buttonChair_Checked;
                button.Unchecked += buttonChair_Unchecked;

                wp.Children.Add(button);
            }
        }

        private void loadCustomerOwner()
        {
            cboCustomers.ItemsSource = CustomerData.CusList;
            cboCustomers.SelectedValuePath = "Cus_id";
            cboCustomers.DisplayMemberPath = "Name";
            cboCustomers.SelectionChanged += cboCustomers_SeSelectionChanged;
            
            if(((MainWindow)Window.GetWindow(this)).currentTable != null && ((MainWindow)Window.GetWindow(this)).currentTable.TableOrder.cus_id != null)
            {
                cboCustomers.SelectedValue = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrder.cus_id;
            }
        }

        private void cboCustomers_SeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (Model.Table t in TableTempData.TbList)
                {
                    if (((MainWindow)Window.GetWindow(this)).currentTable.TableNumber == t.TableNumber)
                    {
                        t.TableOrder.cus_id = cboCustomers.SelectedValue.ToString();
                    }
                }
                ReadWriteData.writeToBinFile();
            }
            catch(Exception ex)
            {

            }
        }

        private void buttonChair_Checked(object sender, RoutedEventArgs e)
        {
            curChair = sender as ToggleButton;

            foreach (Model.Chair chair in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
            {
                if (chair.ChairNumber == int.Parse(curChair.Name.Substring(5)) && chair.TableOfChair == ((MainWindow)Window.GetWindow(this)).currentTable.TableNumber)
                {
                    ((MainWindow)Window.GetWindow(this)).currentChair = chair;
                    break;
                }
            }

            foreach (ToggleButton btn in wp.Children)
            {
                if (!btn.Name.Equals(curChair.Name))
                {
                    btn.IsChecked = false;
                }
            }

            RefreshControl();
        }

        private void buttonChair_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshControlAllChair();
        }

        public class OrderDetails_Product_Joiner : INotifyPropertyChanged
        {
            public Chair ChairOrder { get; set; }
            public OrderNoteDetails OrderDetails { get; set; }
            public Product Product { get; set; }

            public int ChairOrderNumber
            {
                get
                {
                    return ChairOrder.ChairNumber;
                }
            }
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
            //var ordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
            var chairordernotedetails = new List<OrderNoteDetails>();
            var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
            foreach (Chair ch in chairoftable)
            {
                if (ch != null)
                {
                    chairordernotedetails = chairordernotedetails.Concat(ch.ChairOrderDetails).ToList();
                }
            }

            // chuyen product_id thanh product name
            var query_item_in_ordertails = from orderdetails in chairordernotedetails
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
            var ordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
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
            var ordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
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
