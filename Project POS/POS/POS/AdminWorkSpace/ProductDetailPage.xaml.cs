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
            lvIngredient.ItemsSource = unitofork.IngredientRepository.Get();
            cboType.Items.Add("ALL");
            
            cboType.Items.Add(ProductType.Drink);
            cboType.Items.Add(ProductType.Food);
            cboType.Items.Add(ProductType.Beer);
            cboType.Items.Add(ProductType.Wine);
            cboType.Items.Add(ProductType.Snack);
            cboType.Items.Add(ProductType.Other);


        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Product pro = lvData.SelectedItem as Product;
            lvDetails.ItemsSource=_unitofork.ProductDetailsRepository.Get(c=>c.ProductId.Equals(pro.ProductId));

        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lvData.ItemsSource = _unitofork.ProductRepository.Get(c=>c.Type.Equals(cboType.SelectedValue.ToString() as enum));
        }
    }
}
