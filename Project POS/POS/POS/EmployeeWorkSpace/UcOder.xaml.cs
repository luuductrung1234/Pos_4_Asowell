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
using POS.Entities;
using POS.Repository.DAL;
using Chair = POS.BusinessModel.Chair;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for UcOder.xaml
    /// </summary>
    public partial class UcOder : UserControl
    {
        private EmployeewsOfAsowell _unitofwork;
        private Entities.Table currentTable;
        private Entities.Chair currentChair;
        private OrderTemp ordertemptable;
        private List<Entities.Chair> chairlistcurrenttable;
        private List<Entities.OrderDetailsTemp> orderdetailstempcurrenttablelist;

        public UcOder()
        {
            InitializeComponent();

            this.Loaded += UcOder_Loaded;
            this.Unloaded += UcOder_Unloaded;
        }

        private void UcOder_Loaded(object sender, RoutedEventArgs e)
        {
            _unitofwork = ((MainWindow)Window.GetWindow(this))._unitofwork;
            currentTable = ((MainWindow)Window.GetWindow(this)).currentTable;
            currentChair = ((MainWindow)Window.GetWindow(this)).currentChair;

            if (currentTable == null)
            {
                return;
            }

            ordertemptable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(currentTable.TableId)).First();
            orderdetailstempcurrenttablelist = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(ordertemptable.OrdertempId)).ToList();

            loadTableChairData();
            loadCustomerOwner();
            RefreshControlAllChair();
        }

        private void UcOder_Unloaded(object sender, RoutedEventArgs e)
        {
            //((MainWindow)Window.GetWindow(this)).currentChair = null;
        }


        /// <summary>
        /// show all orderdetails in the current checked chair.
        /// allow to modify these orderdetails
        /// </summary>
        public void RefreshControl(EmployeewsOfAsowell unitofwork, Entities.Table curTable)
        {
            try
            {
                initStatus_RaiseEvent = true;

                _unitofwork = unitofwork;
                currentTable = curTable;
                if (currentTable == null || currentChair == null)
                {
                    initStatus_RaiseEvent = false;
                    return;
                }

                ordertemptable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned == currentTable.TableId).First();
                orderdetailstempcurrenttablelist = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(ordertemptable.OrdertempId)).ToList();

                // lay ordernotedetails cua ban thu nhat
                var chairoftable = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(currentTable.TableId)).ToList();
                var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(currentChair.ChairNumber) && x.TableOwned.Equals(currentChair.TableOwned));
                var chairordernotedetails = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(foundchair.ChairId)).ToList();

                // chuyen product_id thanh product name
                var query = from orderdetails in chairordernotedetails
                            join product in _unitofwork.ProductRepository.Get()
                                    on orderdetails.ProductId equals product.ProductId

                            select new OrderDetails_Product_Joiner
                            {
                                OrderDetailsTemp = orderdetails,
                                Product = product
                            };

                // binding
                lvData.ItemsSource = query;
                loadTotalPrice();

                initStatus_RaiseEvent = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// show all orderdetails of all chairs in the table.
        /// but not allow to modify these orderdetails
        /// </summary>
        public void RefreshControlAllChair()
        {
            if (currentTable == null)
            {
                return;
            }

            // chuyen product_id thanh product name
            var query = from orderdetails in orderdetailstempcurrenttablelist
                        join product in _unitofwork.ProductRepository.Get()
                        on orderdetails.ProductId equals product.ProductId

                        select new OrderDetails_Product_Joiner
                        {
                            OrderDetailsTemp = orderdetails,
                            Product = product
                        };

            // binding
            lvData.ItemsSource = query;
            loadTotalPrice();
        }

        ToggleButton curChair;
        private void loadTableChairData()
        {
            if (currentTable == null)
            {
                return;
            }

            txtDay.Text = ordertemptable.Ordertime.ToString("dd/MM/yyyy H:mm:ss");
            txtTable.Text = currentTable.TableNumber.ToString();
            wp.Children.Clear();

            chairlistcurrenttable = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(currentTable.TableId)).ToList();

            foreach (Entities.Chair ch in chairlistcurrenttable)
            {
                var foundedchair = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(ch.ChairId)).ToList();
                if (foundedchair.Count != 0)
                {
                    currentTable.IsOrdered = 1;
                    _unitofwork.TableRepository.Update(currentTable);
                    _unitofwork.Save();
                }

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
            cboCustomers.ItemsSource = _unitofwork.CustomerRepository.Get();
            cboCustomers.SelectedValuePath = "CusId";
            cboCustomers.DisplayMemberPath = "Name";
            cboCustomers.MouseEnter += (sender, args) =>
            {
                cboCustomers.Background.Opacity = 100;
            };
            cboCustomers.MouseLeave += (sender, args) =>
            {
                cboCustomers.Background.Opacity = 0;
            };

            if (((MainWindow)Window.GetWindow(this)).currentTable != null
                && ordertemptable.CusId != null)
            {
                initCus_raiseEvent = true;
                cboCustomers.SelectedValue = ordertemptable.CusId;
            }
            initCus_raiseEvent = false;
        }

        private bool initCus_raiseEvent = false;
        private void cboCustomers_SeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!initCus_raiseEvent)
            {
                //((MainWindow) Window.GetWindow(this)).currentTable.TableOrder.CusId = (string) ((sender as ComboBox).SelectedItem as Customer).CusId;
                ordertemptable.CusId =
                    (string)(sender as ComboBox).SelectedValue;
                _unitofwork.OrderTempRepository.Update(ordertemptable);
                _unitofwork.Save();
            }
        }

        private void buttonChair_Checked(object sender, RoutedEventArgs e)
        {
            //int ii = 0;
            curChair = sender as ToggleButton;
            //if(int.Parse(curChair.Name.Substring(5)) != 1)
            //{
            //    if (int.Parse(curChair.Name.Substring(5)) - ((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber > 1)
            //    {
            //        for (int i = ((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber; i < int.Parse(curChair.Name.Substring(5)); i++)
            //        {
            //            foreach(var ch in ((MainWindow)Window.GetWindow(this)).currentTable.ChairData)
            //            {
            //                if(ch.ChairNumber == i)
            //                {
            //                    if(ch.ChairOrderDetails.Count == 0)
            //                    {
            //                        MessageBox.Show("You seem ignore chair number " + i + " of this table!");
            //                        curChair.IsChecked = false;
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            foreach (Entities.Chair chair in chairlistcurrenttable)
            {
                if (chair.ChairNumber == int.Parse(curChair.Content.ToString()) && chair.TableOwned.Equals(currentTable.TableId))
                {
                    ((MainWindow)Window.GetWindow(this)).currentChair = chair;
                    currentChair = chair;
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

            RefreshControl(_unitofwork, currentTable);
        }

        private void buttonChair_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshControlAllChair();
        }


        /// <summary>
        /// inner class that use to store the joined data from
        /// orderdetails entities and product entities
        /// </summary>
        public class OrderDetails_Product_Joiner : INotifyPropertyChanged
        {
            public Entities.Chair ChairOrder { get; set; }
            public OrderDetailsTemp OrderDetailsTemp { get; set; }
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
                    return OrderDetailsTemp.Quan;
                }
                set
                {
                    OrderDetailsTemp.Quan = value;
                    OnPropertyChanged("Quan");
                }
            }
            public ObservableCollection<string> StatusItems
            {
                get
                {
                    return OrderDetailsTemp.StatusItems;
                }
                set
                {
                    OrderDetailsTemp.StatusItems = value;
                    OnPropertyChanged("StatusItems");
                }
            }
            public string SelectedStats
            {
                get
                {
                    return OrderDetailsTemp.SelectedStats;
                }
                set
                {
                    OrderDetailsTemp.SelectedStats = value;
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
            var chairordernotedetails = new List<OrderDetailsTemp>();
            foreach (Entities.Chair ch in chairlistcurrenttable)
            {
                var chairorderdetailstemp = _unitofwork.OrderDetailsTempRepository.Get(x => x.ChairId.Equals(ch.ChairId)).ToList();
                if (ch != null && chairorderdetailstemp != null)
                {
                    chairordernotedetails = chairordernotedetails.Concat(chairorderdetailstemp).ToList();
                }
            }

            // chuyen product_id thanh product name
            var query_item_in_ordertails = from orderdetails in chairordernotedetails
                                           join product in _unitofwork.ProductRepository.Get()
                                           on orderdetails.ProductId equals product.ProductId
                                           select new
                                           {
                                               item_quan = orderdetails.Quan,
                                               item_price = product.Price,
                                               item_discount = product.Discount
                                           };

            decimal Total = 0;
            foreach (var item in query_item_in_ordertails)
            {
                Total = (decimal)((float)Total + (float)(item.item_quan * ((float)item.item_price * ((100 - item.item_discount) / 100.0))));
            }
            txtTotal.Text = string.Format("{0:0.000}", Total) + " VND";
            ordertemptable.TotalPrice = (decimal)Total;
        }


        bool initStatus_RaiseEvent = false;
        private void cboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!initStatus_RaiseEvent)
            {
                if (currentChair == null)
                {
                    return;
                }

                orderdetailstempcurrenttablelist = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(ordertemptable.OrdertempId)).ToList();
                var ordernotedetails = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(currentChair.ChairId)).ToList();
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);
                if (index < 0)
                {
                    return;
                }

                string olstat = ordernotedetails[index].OldStat;

                OrderDetailsTemp tempdata = new OrderDetailsTemp();
                tempdata.ChairId = ordernotedetails[index].ChairId;
                tempdata.OrdertempId = ordernotedetails[index].OrdertempId;
                tempdata.ProductId = ordernotedetails[index].ProductId;
                tempdata.StatusItems = ordernotedetails[index].StatusItems;
                tempdata.Quan = ordernotedetails[index].Quan;
                tempdata.Note = ordernotedetails[index].Note;

                _unitofwork.OrderDetailsTempRepository.Delete(ordernotedetails[index]);
                _unitofwork.Save();
                tempdata.SelectedStats = (e.OriginalSource as ComboBox).SelectedItem.ToString();
                _unitofwork.OrderDetailsTempRepository.Insert(tempdata);
                _unitofwork.Save();

                RefreshControl(_unitofwork, currentTable);
            }
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
            if (i == 0)
            {
                MessageBox.Show("Choose exactly which chair you want to decrease food's quantity!");
                return;
            }

            DependencyObject dep = (DependencyObject)e.OriginalSource;
            OrderDetailsTemp o = new OrderDetailsTemp();
            int index;

            foreach (ToggleButton btn in wp.Children)
            {
                if (btn.IsChecked == true)
                {
                    //delete chair order note
                    var chairoftable = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(currentTable.TableId)).ToList();
                    var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(currentChair.ChairNumber)
                                            && x.TableOwned.Equals(currentChair.TableOwned));
                    var chairordernotedetails = orderdetailstempcurrenttablelist.Where(x => x.ChairId == foundchair.ChairId).ToList();

                    while ((dep != null) && !(dep is ListViewItem))
                    {
                        dep = VisualTreeHelper.GetParent(dep);
                    }

                    if (dep == null)
                        return;

                    index = lvData.ItemContainerGenerator.IndexFromContainer(dep);

                    if (chairordernotedetails[index].Quan > 1)
                    {
                        chairordernotedetails[index].Quan--;
                        _unitofwork.OrderDetailsTempRepository.Update(chairordernotedetails[index]);
                        _unitofwork.Save();
                    }
                    else
                    {
                        var chairtemp = chairordernotedetails[index];

                        orderdetailstempcurrenttablelist.Remove(chairordernotedetails[index]);

                        chairordernotedetails.RemoveAt(index);
                        _unitofwork.OrderDetailsTempRepository.Delete(chairtemp);
                        _unitofwork.Save();
                    }

                    currentTable.IsOrdered = 0;
                    foreach (Entities.Chair chair in chairoftable)
                    {
                        var chairorderdetailstemp = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(chair.ChairId)).ToList();
                        if (chairorderdetailstemp.Count != 0)
                        {
                            currentTable.IsOrdered = 1;
                        }
                        break;
                    }

                    _unitofwork.TableRepository.Update(currentTable);
                    _unitofwork.Save();

                    ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
                    RefreshControl(_unitofwork, currentTable);
                    break;
                }
            }
        }

        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentTable == null || currentChair == null)
            {
                return;
            }

            orderdetailstempcurrenttablelist = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(ordertemptable.OrdertempId)).ToList();
            var ordernotedetails = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(currentChair.ChairId)).ToList();
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvData.ItemContainerGenerator.IndexFromContainer(dep);

            OrderDetailsTemp tempdata = new OrderDetailsTemp();
            tempdata.ChairId = ordernotedetails[index].ChairId;
            tempdata.OrdertempId = ordernotedetails[index].OrdertempId;
            tempdata.ProductId = ordernotedetails[index].ProductId;
            tempdata.SelectedStats = ordernotedetails[index].SelectedStats;
            tempdata.StatusItems = ordernotedetails[index].StatusItems;
            tempdata.Quan = ordernotedetails[index].Quan;
            tempdata.Note = "";

            InputNote inputnote = new InputNote(ordernotedetails[index].Note);
            if (ordernotedetails[index].Note.Equals("") || ordernotedetails[index].Note.Equals(inputnote.Note))
            {
                if (inputnote.ShowDialog() == true)
                {
                    tempdata.Note = inputnote.Note.ToLower();

                    if (ordernotedetails[index].Quan == 1)
                    {
                        foreach (var cho in ordernotedetails)
                        {
                            if (cho.OrdertempId.Equals(tempdata.OrdertempId) && cho.ChairId.Equals(tempdata.ChairId) && cho.ProductId.Equals(tempdata.ProductId) && cho.SelectedStats.Equals(tempdata.SelectedStats) && cho.Note.Equals(tempdata.Note))
                            {
                                cho.Quan++;
                                _unitofwork.OrderDetailsTempRepository.Delete(ordernotedetails[index]);
                                _unitofwork.Save();
                                _unitofwork.OrderDetailsTempRepository.Update(cho);
                                _unitofwork.Save();
                                RefreshControl(_unitofwork, currentTable);
                                return;
                            }
                        }

                        _unitofwork.OrderDetailsTempRepository.Delete(ordernotedetails[index]);
                        _unitofwork.Save();
                        _unitofwork.OrderDetailsTempRepository.Insert(tempdata);
                        _unitofwork.Save();
                        RefreshControl(_unitofwork, currentTable);
                        return;
                    }

                    if (ordernotedetails[index].Quan > 1)
                    {
                        foreach (var cho in ordernotedetails)
                        {
                            if (cho.OrdertempId.Equals(tempdata.OrdertempId) && cho.ChairId.Equals(tempdata.ChairId) && cho.ProductId.Equals(tempdata.ProductId) && cho.SelectedStats.Equals(tempdata.SelectedStats) && cho.Note.Equals(tempdata.Note))
                            {
                                tempdata.Note = ordernotedetails[index].Note;
                                tempdata.Quan--;
                                cho.Quan++;
                                _unitofwork.OrderDetailsTempRepository.Update(cho);
                                _unitofwork.Save();
                                _unitofwork.OrderDetailsTempRepository.Delete(ordernotedetails[index]);
                                _unitofwork.Save();
                                _unitofwork.OrderDetailsTempRepository.Insert(tempdata);
                                _unitofwork.Save();
                                RefreshControl(_unitofwork, currentTable);
                                return;
                            }
                        }

                        foreach (var cho in ordernotedetails)
                        {
                            if (cho.OrdertempId.Equals(tempdata.OrdertempId) && cho.ChairId.Equals(tempdata.ChairId) && cho.ProductId.Equals(tempdata.ProductId) && cho.SelectedStats.Equals(tempdata.SelectedStats) && !cho.Note.Equals(tempdata.Note))
                            {
                                ordernotedetails[index].Quan--;
                                _unitofwork.OrderDetailsTempRepository.Update(ordernotedetails[index]);
                                _unitofwork.Save();
                                tempdata.Quan = 1;
                                _unitofwork.OrderDetailsTempRepository.Insert(tempdata);
                                _unitofwork.Save();
                                RefreshControl(_unitofwork, currentTable);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                inputnote.ShowDialog();
            }
        }




        private void bntPay_Click(object sender, RoutedEventArgs e)
        {
            // printing

            // add data to database
            ordertemptable.CustomerPay = ordertemptable.TotalPrice;
            ordertemptable.PayBack = 0;
            _unitofwork.OrderTempRepository.Update(ordertemptable);
            _unitofwork.Save();

            // clean the old table data
            ClearTheTable();
        }

        private void BntPrint_OnClick(object sender, RoutedEventArgs e)
        {
            // printing (with business process in restaurant)
        }

        private void BntDelete_OnClick(object sender, RoutedEventArgs e)
        {
            ClearTheTable();
        }

        private void ClearTheTable()
        {
            Entities.Table curTable = currentTable;
            var chairoftable = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(curTable.TableId)).ToList();
            foreach (Entities.Chair chair in chairoftable)
            {
                var ordernotedetails = orderdetailstempcurrenttablelist.Where(x => x.ChairId.Equals(chair.ChairId)).ToList();
                if (ordernotedetails.Count != 0)
                {
                    foreach (var ch in ordernotedetails)
                    {
                        orderdetailstempcurrenttablelist.Remove(ch);
                        _unitofwork.OrderDetailsTempRepository.Delete(ch);
                        _unitofwork.Save();
                    }
                }
            }

            ordertemptable.EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId;
            ordertemptable.CusId = "CUS0000001";
            ordertemptable.TableOwned = curTable.TableId;
            ordertemptable.Ordertime = DateTime.Now;
            ordertemptable.TotalPrice = 0;
            ordertemptable.CustomerPay = 0;
            ordertemptable.PayBack = 0;

            curTable.IsOrdered = 0;

            ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
            loadCustomerOwner();
            RefreshControlAllChair();
            _unitofwork.OrderTempRepository.Update(ordertemptable);
            _unitofwork.Save();
        }

    }
}
