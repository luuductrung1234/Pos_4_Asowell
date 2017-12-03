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
using POS.Repository.DAL;
using POS.Entities;
using POS.Entities.CustomEntities;
using System.IO;
using Microsoft.Win32;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductCreatorPage.xaml
    /// </summary>
    public partial class ProductCreatorPage : Page
    {
        private AdminwsOfCloudAsowell _unitofwork;
        List<Ingredient> _igreList;
        Product _currentProduct;
        
        string browseImagePath = "";
        string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public ProductCreatorPage(AdminwsOfCloudAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            _igreList = _unitofwork.IngredientRepository.Get(x => x.Deleted == 0).ToList();
            lvAvaibleIngredient.ItemsSource = _igreList;

            _currentProduct = new Product();

            initComboBox();
        }

        private void initComboBox()
        {
            cboType.Items.Add(ProductType.Beverage);
            cboType.Items.Add(ProductType.Food);
            cboType.Items.Add(ProductType.Beer);
            cboType.Items.Add(ProductType.Wine);
            cboType.Items.Add(ProductType.Snack);
            cboType.Items.Add(ProductType.Other);
            cboType.Items.Add(ProductType.Coffee);
            cboType.Items.Add(ProductType.Cocktail);
            cboType.SelectedItem = ProductType.Beverage;

            cboStatus.Items.Add("Drink");
            cboStatus.Items.Add("Starter");
            cboStatus.Items.Add("Main");
            cboStatus.Items.Add("Dessert");
            cboStatus.SelectedItem = "Drink";
        }

        private void LvAvaibleIngredient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            var ingre = lv.SelectedItem as Ingredient;

            if (ingre == null)
            {
                return;
            }

            if(PDTempData.pdtList.Count != 0)
            {
                var igre = PDTempData.pdtList.Where(x => x.ProDe.IgdId.Equals(ingre.IgdId)).FirstOrDefault();
                if (igre != null)
                {
                    MessageBox.Show("This Ingredient is already exist in Product Details List! Please choose another!");
                    return;
                }
            }

            ProductDetail newPD = new ProductDetail
            {
                PdetailId = "",
                ProductId = "",
                IgdId = ingre.IgdId,
                Quan = 0,
                UnitUse = ""
            };

            isRaiseEvent = true;
            //_currentProduct.ProductDetails.Add(newPD);
            PDTempData.pdtList.Add(new PDTemp { ProDe = newPD, Ingre = ingre });
            lvDetails.ItemsSource = PDTempData.pdtList;
            lvDetails.Items.Refresh();
            isRaiseEvent = false;
        }

        private void BntDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvDetails.ItemContainerGenerator.IndexFromContainer(dep);

            if (index < 0)
                return;

            isRaiseEvent = true;
            //_currentProduct.ProductDetails.Remove(PDTempData.pdtList[index].ProDe);
            PDTempData.pdtList.RemoveAt(index);
            lvDetails.ItemsSource = PDTempData.pdtList;
            lvDetails.Items.Refresh();
            isRaiseEvent = false;
        }

        bool isRaiseEvent = false;
        private void cboUnitUse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isRaiseEvent)
            {
                ComboBox cbo = sender as ComboBox;
                if (cbo.SelectedItem.Equals(""))
                {
                    return;
                }

                DependencyObject dep = (DependencyObject)e.OriginalSource;
                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                int index = lvDetails.ItemContainerGenerator.IndexFromContainer(dep);
                if (index < 0)
                {
                    return;
                }

                isRaiseEvent = true;
                if(cboStatus.SelectedItem.Equals("Time"))
                {
                    _currentProduct.ProductDetails.ToList()[index].Quan = 1;
                    PDTempData.pdtList[index].ProDe.Quan = 1;
                }

                //_currentProduct.ProductDetails.ToList()[index].UnitUse = cbo.SelectedItem.ToString();
                PDTempData.pdtList[index].ProDe.UnitUse = cbo.SelectedItem.ToString();
                lvDetails.ItemsSource = PDTempData.pdtList;
                lvDetails.Items.Refresh();
                isRaiseEvent = false;
            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isRaiseEvent)
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;
                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                int index = lvDetails.ItemContainerGenerator.IndexFromContainer(dep);
                if (index < 0)
                {
                    return;
                }

                if((sender as TextBox).Text.Trim().Equals("") || (sender as TextBox).Text.Trim() == null)
                {
                    return;
                }

                isRaiseEvent = true;
                //_currentProduct.ProductDetails.ToList()[index].Quan = int.Parse((sender as TextBox).Text.Trim());
                PDTempData.pdtList[index].ProDe.Quan = int.Parse((sender as TextBox).Text.Trim());
                lvDetails.ItemsSource = PDTempData.pdtList;
                lvDetails.Items.Refresh();
                isRaiseEvent = false;
            }
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(PDTempData.pdtList.Count == 0)
            {
                MessageBox.Show("You must be choose at least one ingredient! Please try again!");
                return;
            }

            foreach(var pd in PDTempData.pdtList)
            {
                if(pd.ProDe.UnitUse.Equals("") || pd.ProDe.UnitUse == null || pd.ProDe.Quan < 1)
                {
                    MessageBox.Show("Please check information of " + pd.Ingre.Name + " again! Something is not valid!");
                    return;
                }
            }

            //check name
            string name = txtName.Text.Trim();
            if(name.Length == 0)
            {
                MessageBox.Show("Name is not valid!");
                txtName.Focus();
                return;
            }

            //check info
            string info = txtInfo.Text.Trim();
            //if (info.Length == 0)
            //{
            //    MessageBox.Show("Information is not valid!");
            //    txtInfo.Focus();
            //    return;
            //}

            //check type
            int type = (int)cboType.SelectedItem;

            //check imagename
            string imgname = txtImageName.Text.Trim();
            //if(imgname.Length == 0)
            //{
            //    MessageBox.Show("Please choose a image to continue!");
            //    btnLinkImg.Focus();
            //    return;
            //}

            //check discount
            //

            //check standard status
            string stdstt = cboStatus.SelectedItem.ToString();

            //check price
            decimal price = decimal.Parse(txtPrice.Text.Trim());

            _currentProduct.ProductId = "";
            _currentProduct.Name = name;
            _currentProduct.Info = info;
            _currentProduct.Type = type;
            _currentProduct.ImageLink = imgname;
            _currentProduct.Discount = 0;
            _currentProduct.StandardStats = stdstt;
            _currentProduct.Price = price;

            string destinationFile = startupProjectPath + "\\Images\\" + txtImageName.Text.Trim();
            try
            {
                File.Delete(destinationFile);
                File.Copy(browseImagePath, destinationFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _unitofwork.ProductRepository.Insert(_currentProduct);
            _unitofwork.Save();

            foreach(var pd in PDTempData.pdtList)
            {
                pd.ProDe.ProductId = _currentProduct.ProductId;
                _unitofwork.ProductDetailsRepository.Insert(pd.ProDe);
                _unitofwork.Save();
            }
        }

        private void btnLinkImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.DefaultExt = ".";
            browseFile.Filter = "All Image Files (*.png, *.jpg, *.jpeg)|*.png; *.jpg; *.jpeg"; // " | JPEG Files (*.jpeg)|*.jpeg | PNG Files (*.png)|*.png | JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = browseFile.ShowDialog();

            if (result == true)
            {
                txtImageName.Text = browseFile.SafeFileName;
                browseImagePath = browseFile.FileName;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtInfo.Text = "";
            cboType.SelectedItem = ProductType.Beverage;
            txtImageName.Text = "";
            txtDiscount.Text = "";
            cboStatus.SelectedItem = "Drink";
            txtPrice.Text = "";
        }
        
    }

    public class PDTemp
    {
        private ProductDetail _pd;
        private Ingredient _ingre;

        public ProductDetail ProDe
        {
            get { return _pd; }
            set
            {
                _pd = value;
            }
        }

        public Ingredient Ingre
        {
            get { return _ingre; }
            set
            {
                _ingre = value;
            }
        }

        public List<string> UnitUseT
        {
            get
            {
                return new List<string> { "", "g", "ml", "time" };
            }
        }
    }

    public class PDTempData
    {
        public static List<PDTemp> pdtList = new List<PDTemp>();
    }

}
