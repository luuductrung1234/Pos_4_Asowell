using POS.Entities;
using POS.Repository;
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
using POS.Context;
using POS.Repository.Interfaces;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SettingFood.xaml
    /// </summary>
    public partial class SettingFood : Page
    {
        private IProductRepository _productRepository;

        public SettingFood()
        {
            _productRepository = new ProductRepository(new AsowellContext());
            InitializeComponent();
            lvData.ItemsSource = _productRepository.GetAllProducts();
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product pro = lvData.SelectedItem as Product;

            txtID.Text = pro.ProductId;
            txtName.Text = pro.Name;
            txtPrice.Text = pro.Price.ToString();
        }
    }
}
