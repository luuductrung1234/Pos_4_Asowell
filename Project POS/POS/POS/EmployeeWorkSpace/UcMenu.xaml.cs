using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.BusinessModel;
using POS.Entities;
using POS.Repository.DAL;
using POS.Entities.CustomEntities;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for UcMenu.xaml
    /// </summary>
    public partial class UcMenu : UserControl
    {
        private EmployeewsOfAsowell _unitofwork;
        private Entities.Table orderingTable;
        private Entities.Chair orderingChair;
        private OrderTemp orderTempCurrentTable;
        private OrderDetailsTemp orderDetailsTempTable;

        public UcMenu()
        {
            InitializeComponent();

            this.Loaded += UcMenu_Loaded;
        }

        private void UcMenu_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _unitofwork = ((MainWindow)Window.GetWindow(this))._unitofwork;
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage);
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Food);
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer);
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine);
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Snack);
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other);

                orderingTable = ((MainWindow)Window.GetWindow(this)).currentTable;
                orderingChair = ((MainWindow)Window.GetWindow(this)).currentChair;

                if (orderingTable == null)
                {
                    return;
                }

                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControlAllChair();
            }
            catch (Exception ex)
            {

            }
        }

        public override void OnApplyTemplate()
        {
            DependencyObject ButtonControlInTemplate = GetTemplateChild("searchbutton");// set the name as the x:Name for the controls in your xaml.
            Button SearchButton = (Button)ButtonControlInTemplate;
            DependencyObject TextBoxInTemplate = GetTemplateChild("searchinputfield"); // set the name as the x:Name for the controls in your xaml.
            TextBox InputTextBox = (TextBox)TextBoxInTemplate;
            base.OnApplyTemplate();
        }

        private void lvCategory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            orderingTable = ((MainWindow)Window.GetWindow(this)).currentTable;
            orderingChair = ((MainWindow)Window.GetWindow(this)).currentChair;
            ListBox lbSelected = sender as ListBox;

            if (orderingTable == null || orderingChair == null)
            {
                MessageBox.Show("Chair must be choice!");
                return;
            }

            orderTempCurrentTable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(orderingTable.TableId)).First();
            if(orderTempCurrentTable == null)
            {
                return;
            }

            var item = lbSelected.SelectedItem;
            if (item != null)
            {
                if (orderingTable.IsOrdered == 0)
                {
                    orderTempCurrentTable.Ordertime = DateTime.Now;
                    orderingTable.IsOrdered = 1;
                    _unitofwork.TableRepository.Update(orderingTable);
                    _unitofwork.Save();
                }

                OrderDetailsTemp o = new OrderDetailsTemp();
                Product it = (Product)lbSelected.SelectedItem;

                //order tung ghe
                var chairoftable = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(orderingTable.TableId));

                if (orderingChair != null)
                {
                    var chairorderdetailstemp = _unitofwork.OrderDetailsTempRepository.Get(x => x.ChairId.Equals(orderingChair.ChairId)).ToList();
                    var foundinchairorderdetailstemp = chairorderdetailstemp.Where(x => x.ProductId.Equals(it.ProductId)).ToList();

                    if (foundinchairorderdetailstemp.Count == 0)
                    {
                        o.ChairId = orderingChair.ChairId;
                        o.OrdertempId = orderTempCurrentTable.OrdertempId;
                        o.ProductId = it.ProductId;
                        o.SelectedStats = it.StandardStats;
                        o.Note = "";
                        o.Quan = 1;
                        _unitofwork.OrderDetailsTempRepository.Insert(o);
                        _unitofwork.Save();
                    }
                    else
                    {
                        foreach (var order in foundinchairorderdetailstemp)
                        {
                            if (!order.SelectedStats.Equals(it.StandardStats) || !order.Note.Equals(""))
                            {
                                o.ChairId = orderingChair.ChairId;
                                o.OrdertempId = orderTempCurrentTable.OrdertempId;
                                o.ProductId = it.ProductId;
                                o.SelectedStats = it.StandardStats;
                                o.Note = "";
                                o.Quan = 1;

                                _unitofwork.OrderDetailsTempRepository.Insert(o);
                                _unitofwork.Save();

                                break;
                            }

                            if (order.SelectedStats.Equals(it.StandardStats) && order.Note.Equals(""))
                            {
                                order.ProductId = it.ProductId;
                                order.Quan++;

                                _unitofwork.OrderDetailsTempRepository.Update(order);
                                _unitofwork.Save();

                                break;
                            }
                        }
                    }
                }

                //
                lbSelected.UnselectAll();

                ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControl(_unitofwork, orderingTable);
                ((MainWindow)Window.GetWindow(this)).en.ucOrder.txtDay.Text = orderTempCurrentTable.Ordertime.ToString("dd/MM/yyyy H:mm:ss");
            }

        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string filter = SearchBox.Text.Trim();
                checkSearch(filter);
            }
        }

        TabItem curItem = new TabItem();
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if (filter.Length == 0)
            {
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage);
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Food);
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer);
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine);
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Snack);
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other);
                return;
            }

            checkSearch(filter);

        }

        //check khi Search
        private void checkSearch(string filter)
        {
            if (ItemBeverages.IsSelected == true)
            {
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage && p.Name.Contains(filter));
                lvCategoryBeverages.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeverages;
            }

            if (ItemDishes.IsSelected == true)
            {
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Food && p.Name.Contains(filter));
                lvCategoryDishes.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemDishes;
            }

            if (ItemBeer.IsSelected == true)
            {
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer && p.Name.Contains(filter));
                lvCategoryBeer.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeer;
            }

            if (ItemWine.IsSelected == true)
            {
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine && p.Name.Contains(filter));
                lvCategoryWine.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemWine;
            }

            if (ItemSnack.IsSelected == true)
            {
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Snack && p.Name.Contains(filter));
                lvCategoryWine.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemSnack;
            }

            if (ItemOther.IsSelected == true)
            {
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other && p.Name.Contains(filter));
                lvCategoryOther.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemOther;
            }
        }

        //private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            //TabItem sen = sender as TabItem;

            //if (curItem == null)
            //{
            //    return;
            //}

            //if (!sen.Name.Equals(curItem.Name))
            //{
            //    SearchBox.Text = "";
            //}
        }

    }
}
