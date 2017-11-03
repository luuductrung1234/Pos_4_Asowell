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

                var tableordernotedetails = ((MainWindow)Window.GetWindow(this)).currentTable.TableOrderDetails;
                var foundtable = tableordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));
                var chairoftable = ((MainWindow)Window.GetWindow(this)).currentTable.ChairData;
                var foundchair = chairoftable.SingleOrDefault(x => x.ChairNumber.Equals(((MainWindow)Window.GetWindow(this)).currentChair.ChairNumber) && x.TableOfChair.Equals(((MainWindow)Window.GetWindow(this)).currentChair.TableOfChair));
                var chairordernotedetails = foundchair.ChairOrderDetails;
                var found = chairordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));
                int i = chairordernotedetails.IndexOf(found);
                int ii = tableordernotedetails.IndexOf(foundtable);
                if (found == null)
                {
                    o.Product_id = it.Product_id;
                    oo.Product_id = it.Product_id;
                    o.Quan = 1;
                    oo.Quan = 1;
                    chairordernotedetails.Add(o);
                    tableordernotedetails.Add(oo);
                }
                else
                {
                    o.Product_id = it.Product_id;
                    oo.Product_id = it.Product_id;
                    o.Quan = chairordernotedetails[i].Quan + 1;
                    oo.Quan = tableordernotedetails[ii].Quan + 1;
                    o.SelectedStats = chairordernotedetails[i].SelectedStats;
                    oo.SelectedStats = tableordernotedetails[ii].SelectedStats;

                    chairordernotedetails[i] = o;
                    tableordernotedetails[ii] = oo;
                }

                lvCategory.UnselectAll();

                ((MainWindow)Window.GetWindow(this)).en.ucOrder.RefreshControl();
                
            }

        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
