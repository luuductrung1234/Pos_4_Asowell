using System;
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
using System.Windows.Shapes;
using LiveCharts;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for WareHouseWindow.xaml
    /// </summary>
    
    public partial class WareHouseWindow : Window
    {
        AdminwsOfAsowell _unitofwork;
        private LiveChartReceiptPage _lvChartReceiptPage;
        private IngredientPage _innIngredientPage;
        private InputReceiptNote inputReceipt;
        
        public WareHouseWindow()
        {
            InitializeComponent();
            _unitofwork = new AdminwsOfAsowell();
            _innIngredientPage=new IngredientPage(_unitofwork);
            _lvChartReceiptPage = new LiveChartReceiptPage(_unitofwork);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void InputReceipt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            inputReceipt=new InputReceiptNote(_unitofwork);
            myFrame.Navigate(inputReceipt);
        }

        private void ViewReceipt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(_lvChartReceiptPage);
        }

        private void ViewIngredient_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myFrame.Navigate(_innIngredientPage);
        }
    }
}
