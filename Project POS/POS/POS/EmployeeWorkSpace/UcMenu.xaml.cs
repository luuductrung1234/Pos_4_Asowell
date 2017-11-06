using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Drink);
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Food);
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Beer);
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Wine);
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Snack);
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Other);

                if (((MainWindow) Window.GetWindow(this)).currentTable == null)
                {
                    return;
                }
                
                ((MainWindow) Window.GetWindow(this)).en.ucOrder.RefreshControlAllChair();
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
            ListBox lbSelected = sender as ListBox;

            if (((MainWindow)Window.GetWindow(this)).currentTable == null || ((MainWindow)Window.GetWindow(this)).currentChair == null)
            {
                MessageBox.Show("Chair must be choice!");
                return;
            }

            var item = lbSelected.SelectedItem;
            if (item != null)
            {
                OrderNoteDetail o = new OrderNoteDetail();
                OrderNoteDetail oo = new OrderNoteDetail();
                Product it = (Product)lbSelected.SelectedItem;

                //tong order table
                var tableordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
                var foundtable = tableordernotedetails.SingleOrDefault(x => x.ProductId.Equals(it.ProductId));
                int ii = tableordernotedetails.IndexOf(foundtable);
                if (foundtable == null)
                {
                    oo.ProductId = it.ProductId;
                    oo.Quan = 1;
                    tableordernotedetails.Add(oo);
                }
                else
                {
                    oo.ProductId = it.ProductId;
                    oo.Quan = tableordernotedetails[ii].Quan + 1;
                    oo.SelectedStats = tableordernotedetails[ii].SelectedStats;
                    
                    tableordernotedetails[ii] = oo;
                }

                //order tung ghe
                var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
                var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber) && x.TableOfChair.Equals(((MainWindow)Window.GetWindow(this)).currentChair.TableOfChair));
                var chairordernotedetails = foundchair.ChairOrderDetails;
                var found = chairordernotedetails.SingleOrDefault(x => x.ProductId.Equals(it.ProductId));

                int i = chairordernotedetails.IndexOf(found);
                
                if (found == null)
                {
                    o.ProductId = it.ProductId;
                    o.Quan = 1;
                    chairordernotedetails.Add(o);
                }
                else
                {
                    o.ProductId = it.ProductId;
                    o.Quan = chairordernotedetails[i].Quan + 1;
                    o.SelectedStats = chairordernotedetails[i].SelectedStats;

                    chairordernotedetails[i] = o;
                }

                //
                lbSelected.UnselectAll();

                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControl();
                
            }

        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        TabItem curItem = new TabItem();
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if(filter.Length == 0)
            {
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Drink);
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Food);
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Beer);
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Wine);
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Snack);
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Other);
                return;
            }

            if (ItemBeverages.IsSelected == true)
            {
                lvCategoryBeverages.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Drink && p.Name.Contains(filter));
                lvCategoryBeverages.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeverages;
            }

            if (ItemDishes.IsSelected == true)
            {
                lvCategoryDishes.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Food && p.Name.Contains(filter));
                lvCategoryDishes.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemDishes;
            }

            if (ItemBeer.IsSelected == true)
            {
                lvCategoryBeer.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Beer && p.Name.Contains(filter));
                lvCategoryBeer.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemBeer;
            }

            if (ItemWine.IsSelected == true)
            {
                lvCategoryWine.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Wine && p.Name.Contains(filter));
                lvCategoryWine.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemWine;
            }

            if (ItemSnack.IsSelected == true)
            {
                lvCategorySnack.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Snack && p.Name.Contains(filter));
                lvCategoryWine.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemSnack;
            }

            if (ItemOther.IsSelected == true)
            {
                lvCategoryOther.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type == ProductType.Other && p.Name.Contains(filter));
                lvCategoryOther.PreviewMouseLeftButtonUp += lvCategory_PreviewMouseLeftButtonUp;
                curItem = ItemOther;
            }

        }

        //private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
            
        //}

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            TabItem sen = sender as TabItem;

            if (curItem.Header == null)
            {
                return;
            }

            if (!sen.Header.Equals(curItem.Header))
            {
                SearchBox.Text = "";
            }
        }
    }
}
