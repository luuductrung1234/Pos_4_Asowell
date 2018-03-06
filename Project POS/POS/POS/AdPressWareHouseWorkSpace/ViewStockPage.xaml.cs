using System.Collections.Generic;
using System.Windows.Controls;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.AdPressWareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for ViewStockPage.xaml
    /// </summary>
    public partial class ViewStockPage : Page
    {
        private AdminwsOfCloudAPWH _unitofwork;

        public ViewStockPage(AdminwsOfCloudAPWH unitofwork, List<Stock> stockList)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            lvItem.ItemsSource = stockList;
        }
    }
}
