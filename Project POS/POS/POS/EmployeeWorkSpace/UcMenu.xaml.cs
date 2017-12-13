using System;
using System.Collections.Generic;
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
        private EmployeewsOfLocalPOS _unitofwork;
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        private Entities.Table orderingTable;
        private Entities.Chair orderingChair;
        private OrderTemp orderTempCurrentTable;
        private OrderDetailsTemp orderDetailsTempTable;

        public UcMenu()
        {
            InitializeComponent();

            this.Loaded += UcMenu_Loaded;
        }

        internal bool IsRefreshMenu = true;
        public void UcMenu_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsRefreshMenu)
            {
                try
                {
                    _unitofwork = ((MainWindow)Window.GetWindow(this))._unitofwork;
                    _cloudPosUnitofwork = ((MainWindow)Window.GetWindow(this)).CloudPosUnitofwork;
                    lvCategoryBreakFast.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("BreakFast"));
                    lvCategoryStarter.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Starter"));
                    lvCategoryMain.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Main"));
                    lvCategoryDessert.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Dessert"));
                    lvCategoryBeverages.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage);
                    lvCategoryBeer.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer);
                    lvCategoryWine.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine);
                    lvCategoryOther.ItemsSource =
                        _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other);


                    IsRefreshMenu = false;


                    //orderingTable = ((MainWindow) Window.GetWindow(this)).currentTable;
                    //orderingChair = ((MainWindow) Window.GetWindow(this)).currentChair;

                    //if (orderingTable == null)
                    //{
                    //    return;
                    //}

                    //((MainWindow) Window.GetWindow(this)).en.ucOrder.RefreshControlAllChair();
                }
                catch (Exception ex)
                {

                }
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



        //ToDo: Need to update the contain in Warehouse database when new order occur
        private void lvCategory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (App.Current.Properties["CurrentEmpWorking"] == null)
            {
                return;
            }

            orderingTable = ((MainWindow)Window.GetWindow(this)).currentTable;
            orderingChair = ((MainWindow)Window.GetWindow(this)).currentChair;
            ListBox lbSelected = sender as ListBox;

            if (orderingTable == null || orderingChair == null)
            {
                MessageBox.Show("Chair must be choice!");
                return;
            }

            orderTempCurrentTable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(orderingTable.TableId)).First();
            if (orderTempCurrentTable == null)
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
                }

                OrderDetailsTemp o = new OrderDetailsTemp();
                Product it = (Product)lbSelected.SelectedItem;

                //order for each chair

                if (orderingChair != null)
                {
                    var chairorderdetailstemp = _unitofwork.OrderDetailsTempRepository.Get(x => x.ChairId.Equals(orderingChair.ChairId)).ToList();
                    var foundinchairorderdetailstemp = chairorderdetailstemp.Where(x => x.ProductId.Equals(it.ProductId)).ToList();

                    // go to warehouse, check and get the ingredient to make product
                    if (!TakeFromWareHouseData(o, it))       
                    {
                        return;
                    }

                    // add a product to order
                    if (foundinchairorderdetailstemp.Count == 0)
                    {
                        o.ChairId = orderingChair.ChairId;
                        o.OrdertempId = orderTempCurrentTable.OrdertempId;
                        o.ProductId = it.ProductId;
                        o.SelectedStats = it.StandardStats;
                        o.Note = "";
                        o.Quan = 1;
                        o.IsPrinted = 0;
                        o.Discount = it.Discount;

                        
                        _unitofwork.OrderDetailsTempRepository.Insert(o);
                        _unitofwork.Save();
                    }
                    else
                    {
                        foreach (var order in foundinchairorderdetailstemp)
                        {
                            if (!order.SelectedStats.Equals(it.StandardStats) || !order.Note.Equals("") || order.IsPrinted != 0)
                            {
                                o.ChairId = orderingChair.ChairId;
                                o.OrdertempId = orderTempCurrentTable.OrdertempId;
                                o.ProductId = it.ProductId;
                                o.SelectedStats = it.StandardStats;
                                o.Note = "";
                                o.Quan = 1;
                                o.IsPrinted = 0;
                                o.Discount = it.Discount;

                                _unitofwork.OrderDetailsTempRepository.Insert(o);
                                _unitofwork.Save();

                                break;
                            }

                            if (order.SelectedStats.Equals(it.StandardStats) && order.Note.Equals("") && order.IsPrinted == 0)
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


                lbSelected.UnselectAll();

                checkWorkingAction(App.Current.Properties["CurrentEmpWorking"] as EmpLoginList, orderTempCurrentTable);
                ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControl(_unitofwork, orderingTable);
                ((MainWindow)Window.GetWindow(this)).en.ucOrder.txtDay.Text = orderTempCurrentTable.Ordertime.ToString("dd/MM/yyyy H:mm:ss");
            }

        }

        private bool TakeFromWareHouseData(OrderDetailsTemp orderDetails, Product orderingProduct)
        {
            var prodOfOrderDetails =
                _cloudPosUnitofwork.ProductRepository.Get(x => x.ProductId.Equals(orderingProduct.ProductId), includeProperties: "ProductDetails").FirstOrDefault();
            if (prodOfOrderDetails != null)
            {
                // if product have no product details
                if (prodOfOrderDetails.ProductDetails.Count == 0)
                {
                    // still allow to order but no ingredient relate to this product for tracking
                    return true;
                }

                var wareHouseDict = new Dictionary<WareHouse, double?>();

                // going to warehouse and take the contain of each ingredient
                foreach (var prodDetails in prodOfOrderDetails.ProductDetails)
                {
                    var quan = prodDetails.Quan;
                    var ingd =
                        _cloudPosUnitofwork.IngredientRepository.Get(x => x.IgdId.Equals(prodDetails.IgdId))
                            .FirstOrDefault();
                    if (ingd == null)
                    {
                        MessageBox.Show("Something went wrong cause of the Ingredient's information");
                        return false;
                    }
                    var wareHouse =
                        _cloudPosUnitofwork.WareHouseRepository.Get(x => x.WarehouseId.Equals(ingd.WarehouseId))
                            .FirstOrDefault();
                    if (wareHouse == null)
                    {
                        MessageBox.Show("Something went wrong cause of the WareHouse's information");
                        return false;
                    }

                    var temple_Contain = wareHouse.Contain;

                    if (temple_Contain < quan)
                    {
                        MessageBox.Show("This Product can not order now. Please check to WareHouse for Ingredient's stock!");
                        return false;
                    }
                    else
                    {
                        temple_Contain -= quan;
                    }

                    wareHouseDict.Add(wareHouse, temple_Contain);
                }

                // when all ingredient are enough to make product
                foreach (var item in wareHouseDict)
                {
                    item.Key.Contain = item.Value;
                }
                _cloudPosUnitofwork.Save();
            }
            else
            {
                MessageBox.Show("This Product is not existed in database! Please check the Product's information");
                return false;
            }

            return true;
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
                lvCategoryBreakFast.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("BreakFast"));
                lvCategoryStarter.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Starter"));
                lvCategoryMain.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Main"));
                lvCategoryDessert.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Dessert"));
                lvCategoryBeverages.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage);
                lvCategoryBeer.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer);
                lvCategoryWine.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine);
                lvCategoryOther.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other);
                return;
            }

            checkSearch(filter);
        }

        //check khi Search
        private void checkSearch(string filter)
        {
            if (ItemBreakFast.IsSelected == true)
            {
                lvCategoryBreakFast.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("BreakFast") && p.Name.Contains(filter));
                lvCategoryBreakFast.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBreakFast;
            }

            if (ItemStarter.IsSelected == true)
            {
                lvCategoryStarter.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Starter") && p.Name.Contains(filter));
                lvCategoryStarter.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemStarter;
            }

            if (ItemMain.IsSelected == true)
            {
                lvCategoryMain.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Main") && p.Name.Contains(filter));
                lvCategoryMain.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemMain;
            }

            if (ItemDessert.IsSelected == true)
            {
                lvCategoryDessert.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.StandardStats.Equals("Dessert") && p.Name.Contains(filter));
                lvCategoryDessert.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemDessert;
            }

            if (ItemBeverages.IsSelected == true)
            {
                lvCategoryBeverages.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beverage && p.Name.Contains(filter));
                lvCategoryBeverages.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeverages;
            }

            if (ItemBeer.IsSelected == true)
            {
                lvCategoryBeer.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Beer && p.Name.Contains(filter));
                lvCategoryBeer.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeer;
            }

            if (ItemWine.IsSelected == true)
            {
                lvCategoryWine.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Wine && p.Name.Contains(filter));
                lvCategoryWine.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemWine;
            }

            if (ItemOther.IsSelected == true)
            {
                lvCategoryOther.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(p => p.Type == (int)ProductType.Other && p.Name.Contains(filter));
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

        private void checkWorkingAction(EmpLoginList currentEmp, OrderTemp ordertempcurrenttable)
        {
            ((MainWindow)Window.GetWindow(this)).b.isTablesDataChange = true;
            if (currentEmp.Emp.EmpId.Equals(ordertempcurrenttable.EmpId))
            {
                return;
            }

            if (ordertempcurrenttable.SubEmpId != null)
            {
                string[] subemplist = ordertempcurrenttable.SubEmpId.Split(',');

                for (int i = 0; i < subemplist.Count(); i++)
                {
                    if (subemplist[i].Equals(""))
                    {
                        continue;
                    }

                    if (currentEmp.Emp.EmpId.Equals(subemplist[i]))
                    {
                        return;
                    }
                }

                ordertempcurrenttable.SubEmpId += currentEmp.Emp.EmpId + ",";
                _unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
                _unitofwork.Save();
                return;
            }

            ordertempcurrenttable.SubEmpId += currentEmp.Emp.EmpId + ",";
            _unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
            _unitofwork.Save();

        }

    }
}
