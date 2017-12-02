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
using POS.EmployeeWorkSpace;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductDetailPage.xaml
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        private AdminwsOfCloudPOS _unitofwork;
        public ProductDetailPage(AdminwsOfCloudPOS unitofwork)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            InitializeComponent();
            initPageData();
        }

        private void initPageData()
        {
            lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(c=>c.Deleted.Equals(0));
            lvDetails.ItemsSource = _unitofwork.ProductDetailsRepository.Get(includeProperties: "Product");
            lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(c => c.Deleted.Equals(0));

            cboType.Items.Add(ProductType.All);
            cboType.Items.Add(ProductType.Beverage);
            cboType.Items.Add(ProductType.Food);
            cboType.Items.Add(ProductType.Beer);
            cboType.Items.Add(ProductType.Wine);
            cboType.Items.Add(ProductType.Snack);
            cboType.Items.Add(ProductType.Other);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Product pro = lvProduct.SelectedItem as Product;
            lvDetails.ItemsSource=_unitofwork.ProductDetailsRepository.Get(c=>c.ProductId.Equals(pro.ProductId));

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchIBox.Text = "";
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            try
            {
                if (filter.Length == 0)
                {
                    lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type.Equals((int)cboType.SelectedItem) && p.Deleted.Equals(0));
                    return;
                }

                lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type.Equals((int)cboType.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
            catch (Exception ex)
            {
                if (filter.Length == 0)
                {
                    lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Deleted.Equals(0));
                    return;
                }

                lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            try
            {
                if (filter.Length == 0)
                {
                    lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type.Equals((int)cboType.SelectedItem) && p.Deleted.Equals(0));
                    return;
                }

                lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Type.Equals((int)cboType.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
            catch(Exception ex)
            {
                if (filter.Length == 0)
                {
                    lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Deleted.Equals(0));
                    return;
                }

                lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var allProduct = _unitofwork.ProductRepository.Get(p => p.Deleted.Equals(0)).ToList();
            
            //if(SearchBox.Text.Trim().Equals(""))
            //{
            //    try
            //    {
            //        lvProduct.ItemsSource = allProduct.Where(p => p.Type == ((int)(sender as ComboBox).SelectedValue));
            //    }
            //    catch (Exception ex)
            //    {
            //        lvProduct.ItemsSource = allProduct;
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        lvProduct.ItemsSource = allProduct.Where(p => p.Type == ((int)(sender as ComboBox).SelectedValue) && p.Name.Contains(SearchBox.Text.Trim()));
            //    }
            //    catch (Exception ex)
            //    {
            //        lvProduct.ItemsSource = allProduct.Where(p => p.Name.Contains(SearchBox.Text.Trim()));
            //    }
            //}

            var allProduct = _unitofwork.ProductRepository.Get().ToList();
            int selectedVal = ((int) (sender as ComboBox).SelectedValue);
            if (selectedVal == -1)
            {
                lvProduct.ItemsSource = allProduct;
            }
            else
            {
                lvProduct.ItemsSource = allProduct.Where(p => p.Type == selectedVal);
            }

        }

        private void SearchIBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }

        private void SearchIBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();

            try
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.IgdType.Equals((int)cboTypeI.SelectedItem) && p.Deleted.Equals(0));
                }

                lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.IgdType.Equals((int)cboTypeI.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
            catch (Exception ex)
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.Deleted.Equals(0));
                }

                lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
        }

        private void SearchIBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();

            try
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.IgdType.Equals((int)cboTypeI.SelectedItem) && p.Deleted.Equals(0));
                }

                lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.IgdType.Equals((int)cboTypeI.SelectedItem) && p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
            catch (Exception ex)
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.Deleted.Equals(0));
                }

                lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
            }
        }

        private void cboTypeI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        
    }
}
