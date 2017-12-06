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
        private List<Product> allProduct;
        private List<ProductDetail> allProductDetails;
        private List<Ingredient> allIngre;
        private Ingredient _ingre;
        private IngredientAddOrUpdateDialog _ingreAddOrUpdate;

        public ProductDetailPage(AdminwsOfCloudPOS unitofwork)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            InitializeComponent();
            this.Loaded += ProductDetailPage_Loaded;
        }

        private void ProductDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            initPageData();
        }

        private void initPageData()
        {
            allProduct = _unitofwork.ProductRepository.Get(c => c.Deleted.Equals(0)).ToList();
            lvProduct.ItemsSource = allProduct;
            allProductDetails = _unitofwork.ProductDetailsRepository.Get(includeProperties: "Product").ToList();
            lvDetails.ItemsSource = allProductDetails;
            allIngre = _unitofwork.IngredientRepository.Get(c => c.Deleted.Equals(0)).ToList();
            lvIngredient.ItemsSource = allIngre;

            cboType.Items.Add(ProductType.All);
            cboType.Items.Add(ProductType.Beverage);
            cboType.Items.Add(ProductType.Food);
            cboType.Items.Add(ProductType.Beer);
            cboType.Items.Add(ProductType.Wine);
            cboType.Items.Add(ProductType.Snack);
            cboType.Items.Add(ProductType.Other);
            cboType.Items.Add(ProductType.Coffee);
            cboType.Items.Add(ProductType.Cocktail);

            //init Ingredient Type
            cboTypeI.Items.Add("All");
            cboTypeI.Items.Add("Other");
            cboTypeI.Items.Add("Dry");
            cboTypeI.Items.Add("Dairy");
            cboTypeI.Items.Add("Vegetable");
            cboTypeI.Items.Add("Fee");
            cboTypeI.SelectedItem = "All";
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product pro = lvProduct.SelectedItem as Product;
            if(pro == null)
            {
                return;
            }

            lvDetails.ItemsSource = _unitofwork.ProductDetailsRepository.Get(c => c.ProductId.Equals(pro.ProductId));
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

            int selectedVal = ((int)(sender as ComboBox).SelectedValue);
            if (selectedVal == -1)
            {
                lvProduct.ItemsSource = allProduct;
            }
            else
            {
                lvProduct.ItemsSource = allProduct.Where(p => p.Type == selectedVal);
            }

        }

        private void bntEditPro_Click(object sender, RoutedEventArgs e)
        {
            Product curPro = lvProduct.SelectedItem as Product;
            if (curPro == null)
            {
                MessageBox.Show("Please choose exactly which product you want to update!");
                return;
            }

            ProductUpdatePage pup = new ProductUpdatePage(_unitofwork, curPro);
            ((AdminWindow)Window.GetWindow(this)).myframe.Navigate(pup);
        }

        private void bntDelPro_Click(object sender, RoutedEventArgs e)
        {
            Product delPro = lvProduct.SelectedItem as Product;
            if (delPro == null)
            {
                MessageBox.Show("Please choose exactly which product you want to delete!");
                return;
            }

            MessageBoxResult delMess = MessageBox.Show("This action will delete all following product details! Do you want to delete " + delPro.Name + "(" + delPro.ProductId + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
            if (delMess == MessageBoxResult.Yes)
            {
                delPro.Deleted = 1;
                var pdofingre = _unitofwork.ProductDetailsRepository.Get(x => x.ProductId.Equals(delPro.ProductId)).ToList();
                if (pdofingre.Count != 0)
                {
                    foreach (var pd in pdofingre)
                    {
                        _unitofwork.ProductDetailsRepository.Delete(pd);
                    }
                    _unitofwork.Save();
                }

                _unitofwork.ProductRepository.Update(delPro);
                _unitofwork.Save();
                lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(c => c.Deleted.Equals(0));
                lvDetails.ItemsSource = _unitofwork.ProductDetailsRepository.Get(includeProperties: "Product");
                lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(x => x.Deleted.Equals(0)).ToList();
                lvProduct.UnselectAll();
                lvProduct.Items.Refresh();
                lvDetails.UnselectAll();
                lvDetails.Items.Refresh();
                lvIngredient.UnselectAll();
                lvIngredient.Items.Refresh();
            }
        }

        private void SearchIBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }

        private void SearchIBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedI = cboTypeI.SelectedIndex;

            if (selectedI < 0 || cboTypeI.SelectedValue.Equals("All"))
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.Deleted.Equals(0));
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
            else
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.IgdType.Equals(cboTypeI.SelectedItem.ToString()) && p.Deleted.Equals(0));
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.IgdType.Equals(cboTypeI.SelectedItem.ToString()) && p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
        }

        private void SearchIBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedI = cboTypeI.SelectedIndex;

            if (selectedI < 0 || cboTypeI.SelectedValue.Equals("All"))
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.Deleted.Equals(0));
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
            else
            {
                if (filter.Length == 0)
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.IgdType.Equals(cboTypeI.SelectedItem.ToString()) && p.Deleted.Equals(0));
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(p => p.IgdType.Equals(cboTypeI.SelectedItem.ToString()) && p.Name.Contains(filter) && p.Deleted.Equals(0));
                }
            }
        }

        private void cboTypeI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchIBox.Text.Trim();
            int selectedI = (sender as ComboBox).SelectedIndex;

            if (filter.Length == 0)
            {
                if (selectedI < 0 || (sender as ComboBox).SelectedValue.Equals("All"))
                {
                    lvIngredient.ItemsSource = allIngre;
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(x => x.IgdType.Equals((sender as ComboBox).SelectedItem.ToString()));
                }
            }
            else
            {
                if (selectedI < 0 || (sender as ComboBox).SelectedValue.Equals("All"))
                {
                    lvIngredient.ItemsSource = allIngre.Where(x => x.Name.Contains(filter));
                }
                else
                {
                    lvIngredient.ItemsSource = allIngre.Where(x => x.IgdType.Equals((sender as ComboBox).SelectedItem.ToString()) && x.Name.Contains(filter));
                }
            }
        }

        private void bntAdd_Click(object sender, RoutedEventArgs e)
        {
            _ingreAddOrUpdate = new IngredientAddOrUpdateDialog(_unitofwork, null);
            _ingreAddOrUpdate.ShowDialog();

            lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(x => x.Deleted.Equals(0)).ToList();
            lvIngredient.UnselectAll();
            lvIngredient.Items.Refresh();
        }

        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            _ingre = lvIngredient.SelectedItem as Ingredient;

            if (lvIngredient.SelectedItem == null)
            {
                MessageBox.Show("Ingredient must be selected to update! Choose again!");
                return;
            }

            _ingreAddOrUpdate = new IngredientAddOrUpdateDialog(_unitofwork, _ingre);
            _ingreAddOrUpdate.ShowDialog();

            lvProduct.UnselectAll();
            lvProduct.Items.Refresh();
            lvDetails.UnselectAll();
            lvDetails.Items.Refresh();
            lvIngredient.UnselectAll();
            lvIngredient.Items.Refresh();
        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {
            if (lvIngredient.SelectedItem == null)
            {
                MessageBox.Show("Ingredient must be selected to delete! Choose again!");
                return;
            }

            var delIngre = lvIngredient.SelectedItem as Ingredient;
            if (delIngre != null)
            {
                MessageBoxResult delMess = MessageBox.Show("This action will delete all following product details! Do you want to delete " + delIngre.Name + "(" + delIngre.IgdId + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
                if (delMess == MessageBoxResult.Yes)
                {
                    delIngre.Deleted = 1;
                    var pdofingre = _unitofwork.ProductDetailsRepository.Get(x => x.IgdId.Equals(delIngre.IgdId)).ToList();
                    if (pdofingre.Count != 0)
                    {
                        foreach (var pd in pdofingre)
                        {
                            _unitofwork.ProductDetailsRepository.Delete(pd);
                        }
                        _unitofwork.Save();
                    }

                    _unitofwork.IngredientRepository.Update(delIngre);
                    _unitofwork.Save();
                    lvProduct.ItemsSource = _unitofwork.ProductRepository.Get(c => c.Deleted.Equals(0));
                    lvDetails.ItemsSource = _unitofwork.ProductDetailsRepository.Get(includeProperties: "Product");
                    lvIngredient.ItemsSource = _unitofwork.IngredientRepository.Get(x => x.Deleted.Equals(0)).ToList();
                    lvProduct.UnselectAll();
                    lvProduct.Items.Refresh();
                    lvDetails.UnselectAll();
                    lvDetails.Items.Refresh();
                    lvIngredient.UnselectAll();
                    lvIngredient.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please choose ingredient you want to delete and try again!");
            }
        }

    }
}
