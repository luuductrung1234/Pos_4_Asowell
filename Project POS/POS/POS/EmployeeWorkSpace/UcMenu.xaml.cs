using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.Model;
using System.Windows.Controls.Primitives;

namespace POS.EmployeeWorkSpace
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
            this.Loaded += UcMenu_Loaded;
        }

        private void UcMenu_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
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
            if(((MainWindow)Window.GetWindow(this)).currentTable == null || ((MainWindow)Window.GetWindow(this)).currentChair == null)
            {
                MessageBox.Show("Chair must be choice!");
                return;
            }

            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                OrderNoteDetails o = new OrderNoteDetails();
                OrderNoteDetails oo = new OrderNoteDetails();
                Product it = (Product)lvCategory.SelectedItem;

                //tong order table
                var tableordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
                var foundtable = tableordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));
                int ii = tableordernotedetails.IndexOf(foundtable);
                if (foundtable == null)
                {
                    oo.Product_id = it.Product_id;
                    oo.Quan = 1;
                    tableordernotedetails.Add(oo);
                }
                else
                {
                    oo.Product_id = it.Product_id;
                    oo.Quan = tableordernotedetails[ii].Quan + 1;
                    oo.SelectedStats = tableordernotedetails[ii].SelectedStats;
                    
                    tableordernotedetails[ii] = oo;
                }

                //order tung ghe
                var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
                var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber) && x.TableOfChair.Equals(((MainWindow)Window.GetWindow(this)).currentChair.TableOfChair));
                var chairordernotedetails = foundchair.ChairOrderDetails;
                var found = chairordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));

                int i = chairordernotedetails.IndexOf(found);
                
                if (found == null)
                {
                    o.Product_id = it.Product_id;
                    o.Quan = 1;
                    chairordernotedetails.Add(o);
                }
                else
                {
                    o.Product_id = it.Product_id;
                    o.Quan = chairordernotedetails[i].Quan + 1;
                    o.SelectedStats = chairordernotedetails[i].SelectedStats;

                    chairordernotedetails[i] = o;
                }

                //
                lvCategory.UnselectAll();

                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControl();
                
            }

        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
