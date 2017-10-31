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
            if (((MainWindow)Application.Current.MainWindow).currentTable == null)
            {
                return;
            }

            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                OrderNoteDetails o = new OrderNoteDetails();
                Product it = (Product)lvCategory.SelectedItem;
                //var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
                //var chairordernotedetails = ((MainWindow)Application.Current.MainWindow).currentChair.ChairOrderDetails;
                //var found = ordernotedetails.SingleOrDefault(x => x.Product_id.Equals(it.Product_id));
                //int i = ordernotedetails.IndexOf(found);
                //if (found == null)
                //{
                //    o.Product_id = it.Product_id;
                //    o.Quan = 1;
                //    ordernotedetails.Add(o);
                //}
                //else
                //{
                //    o.Product_id = it.Product_id;
                //    o.Quan = ordernotedetails[i].Quan + 1;
                //    o.SelectedStats = ordernotedetails[i].SelectedStats;

                //    ordernotedetails[i] = o;
                //}

                //var ordernotedetails = ((MainWindow)Application.Current.MainWindow).currentTable.TableOrderDetails;
                var chairordernotedetails = ((MainWindow)Application.Current.MainWindow).currentChair.ChairOrderDetails;
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

                lvCategory.UnselectAll();

                ((MainWindow)Application.Current.MainWindow).en.ucOrder.RefreshControl();
            }


        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
