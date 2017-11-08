using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductDetailPage.xaml
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        private AdminwsOfAsowell _unitofork;
        public ProductDetailPage(AdminwsOfAsowell unitofork)
        {
            InitializeComponent();
            _unitofork = unitofork;
            InitializeComponent();
            lvData.ItemsSource = unitofork.ProductRepository.Get();
            lvDetails.ItemsSource = unitofork.ProductDetailsRepository.Get(includeProperties: "Product");
          

            
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Product pro = lvData.SelectedItem as Product;
            lvDetails.ItemsSource=_unitofork.ProductDetailsRepository.Get(c=>c.ProductId.Equals(pro.ProductId));

        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
