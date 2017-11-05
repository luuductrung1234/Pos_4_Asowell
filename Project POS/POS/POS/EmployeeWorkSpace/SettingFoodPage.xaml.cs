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
using System.Text.RegularExpressions;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SettingFood.xaml
    /// </summary>
    public partial class SettingFoodPage : Page
    {
        private IProductRepository _productRepository;

        public SettingFoodPage(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            InitializeComponent();
            lvData.ItemsSource = _productRepository.GetAllProducts();
            for(int i = 0; i <= 100; i++)
            {
                cbopromotion.Items.Add(i.ToString());
            }

            
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bntUpdate.IsEnabled = true;
            Product pro = lvData.SelectedItem as Product;

            txtID.Text = pro.ProductId;
            txtName.Text = pro.Name;
            //txtPrice.Text = pro.Price.ToString();
            txtPrice.Text=string.Format("{0:0.000}", pro.Price);
            cbopromotion.SelectedItem = pro.Discount.ToString();
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (bntUpdate.Content.Equals("Update")){
                txtName.IsEnabled = true;
                txtPrice.IsEnabled = true;
                cbopromotion.IsEnabled = true;
                bntUpdate.Content = "Save";
            }else if (bntUpdate.Content.Equals("Save"))
            {
                Product p = _productRepository.GetProductById(txtID.Text);
                p.Discount= int.Parse(cbopromotion.SelectedValue.ToString());
               _productRepository.UpdateProduct(p);
                _productRepository.Save();


                txtName.IsEnabled = false;
                txtPrice.IsEnabled = false;
                cbopromotion.IsEnabled = false;
                bntUpdate.Content = "Update";
            }
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
