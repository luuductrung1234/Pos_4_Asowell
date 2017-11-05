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
using POS.BusinessModel;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.Interfaces;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for UcOder.xaml
    /// </summary>
    public partial class UcOder : UserControl
    {
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;

        public UcOder()
        {
            InitializeComponent();

            this.Loaded += UcOder_Loaded;
        }

        private void UcOder_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _productRepository = ((MainWindow)Window.GetWindow(this))._productRepository;
                _customerRepository = ((MainWindow)Window.GetWindow(this))._customerRepository;

                if (((MainWindow)Window.GetWindow(this)).currentTable == null)
                {
                    return;
                }

                
                loadTableChairData();
                loadCustomerOwner();
            }
            catch (Exception ex)
            {
                
            }
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
                    join product in _productRepository.GetAllProducts()
                            on orderdetails.ProductId equals product.ProductId

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
            if (((MainWindow)Window.GetWindow(this)).currentTable == null)
            {
                return;
            }

            // lay ordernotedetails cua ban thu nhat
            var tableordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
            //var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
            //List<OrderNoteDetails> ordernotedetailstemp = new List<OrderNoteDetails>();
            //var chairordernotedetails = ordernotedetailstemp;
            //foreach (Chair ch in chairoftable)
            //{
            //    if (ch != null)
            //    {
            //        chairordernotedetails = chairordernotedetails.Concat(ch.ChairOrderDetails).ToList();
            //    }
            //}

            // chuyen product_id thanh product name
            var query = from orderdetails in tableordernotedetails
                        join product in _productRepository.GetAllProducts()
                        on orderdetails.ProductId equals product.ProductId

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
            txtDay.Text = ordertabledetails.Ordertime.ToString("dd/MM/yyyy H:mm:ss");
            txtTable.Text = ordertabledetails.Ordertable.ToString();
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
            cboCustomers.ItemsSource = _customerRepository.GetAllCustomers();
            cboCustomers.SelectedValuePath = "Cus_id";
            cboCustomers.DisplayMemberPath = "Name";
            cboCustomers.MouseEnter += (sender, args) =>
            {
                cboCustomers.Background.Opacity = 100;
            };
            cboCustomers.MouseLeave += (sender, args) =>
            {
                cboCustomers.Background.Opacity = 0;
            };
            cboCustomers.SelectionChanged += cboCustomers_SeSelectionChanged;
            
            if(((MainWindow)Window.GetWindow(this)).currentTable != null && ((MainWindow)Window.GetWindow(this)).currentTable.TableOrder.CusId != null)
            {
                cboCustomers.SelectedValue = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrder.CusId;
            }
        }

        private void cboCustomers_SeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (BusinessModel.Table t in TableTempData.TbList)
                {
                    if (((MainWindow)Window.GetWindow(this)).currentTable.TableNumber == t.TableNumber)
                    {
                        t.TableOrder.CusId = cboCustomers.SelectedValue.ToString();
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
            int ii = 0;
            curChair = sender as ToggleButton;
            if(int.Parse(curChair.Name.Substring(5)) != 1)
            {
                if (int.Parse(curChair.Name.Substring(5)) - ((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber > 1)
                {
                    
                    for (int i = ((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber; i < int.Parse(curChair.Name.Substring(5)); i++)
                    {
                        foreach(var ch in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
                        {
                            if(ch.ChairNumber == i)
                            {
                                if(ch.ChairOrderDetails.Count == 0)
                                {
                                    MessageBox.Show("You seem ignore chair number " + i + " of this table!");
                                    curChair.IsChecked = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            foreach (Chair chair in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
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
            public OrderNoteDetail OrderDetails { get; set; }
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
            public decimal Price
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
            var chairordernotedetails = new List<OrderNoteDetail>();
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
                                           join product in _productRepository.GetAllProducts()
                                           on orderdetails.ProductId equals product.ProductId
                                           select new
                                           {
                                               item_quan = orderdetails.Quan,
                                               item_price = product.Price,
                                               item_discount = product.Discount
                                           };

            float Total = 0;
            foreach (var item in query_item_in_ordertails)
            {
                Total = Total + (float) (item.item_quan * ((float)item.item_price * ((100-item.item_discount)/100.0)));
            }
            txtTotal.Text = Total.ToString() + " VND";
            ((MainWindow) Window.GetWindow(this)).currentTable.TableOrder.TotalPrice = (decimal) Total;
        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (ToggleButton btn in wp.Children)
            {
                if (btn.IsChecked == false)
                {
                    i++;
                }
            }
            if(i == 0)
            {
                MessageBox.Show("Choose exactly which chair you want to decrease food's quantity!");
                return;
            }

            DependencyObject dep = (DependencyObject)e.OriginalSource;
            OrderNoteDetail o = new OrderNoteDetail();
            int index;
            int indext = 0;

            foreach (ToggleButton btn in wp.Children)
            {
                if (btn.IsChecked == true)
                {
                    //delete chair order note
                    var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
                    var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber) && x.TableOfChair.Equals(((MainWindow)Window.GetWindow(this)).currentChair.TableOfChair));
                    var chairordernotedetails = foundchair.ChairOrderDetails;
                    
                    while ((dep != null) && !(dep is ListViewItem))
                    {
                        dep = VisualTreeHelper.GetParent(dep);
                    }

                    if (dep == null)
                        return;

                    index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
                    if (chairordernotedetails[index].Quan > 1)
                    {
                        o.ProductId = chairordernotedetails[index].ProductId;
                        o.Quan = chairordernotedetails[index].Quan - 1;
                        chairordernotedetails[index] = o;

                        foreach(var od in ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails)
                        {
                            if(od.ProductId.Equals(o.ProductId))
                            {
                                od.Quan--;
                            }
                        }
                    }
                    else
                    {
                        o.ProductId = chairordernotedetails[index].ProductId;
                        chairordernotedetails.RemoveAt(index);

                        foreach (var od in ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails)
                        {
                            if (od.ProductId.Equals(o.ProductId))
                            {
                                o = od;
                            }
                        }

                        foreach(var chList in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
                        {
                            foreach(var chOrder in chList.ChairOrderDetails)
                            {
                                if(chOrder.ProductId.Equals(o.ProductId))
                                {
                                    indext++;
                                    foreach (var od in ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails)
                                    {
                                        if (od.ProductId.Equals(o.ProductId))
                                        {
                                            od.Quan--;
                                        }
                                    }
                                }
                            }
                        }

                        if(indext == 0)
                        {
                            ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails.Remove(o);
                        }
                    }

                    RefreshControl();
                    break;
                }
            }
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
            OrderNoteDetail o = new OrderNoteDetail();
            InputNote inputnote = new InputNote(ordernotedetails[index].Note);
            if (ordernotedetails[index].Note == null || ordernotedetails[index].Note.Equals(inputnote.Note))
            {
                if (inputnote.ShowDialog() == true)
                {
                    o.ProductId = ordernotedetails[index].ProductId;
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
