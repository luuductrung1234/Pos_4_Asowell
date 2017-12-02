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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductCreatorPage.xaml
    /// </summary>
    public partial class ProductCreatorPage : Page
    {
        private AdminwsOfCloudPOS _unitofwork;
        List<Ingredient> _igreList;
        Product _currentProduct = new Product();
        public ProductCreatorPage(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            _igreList = _unitofwork.IngredientRepository.Get(x => x.Deleted == 0).ToList();
            lvAvaibleIngredient.ItemsSource = _igreList;
        }

        private void LvAvaibleIngredient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isRaiseEvent = true;
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

            _currentProduct.ProductDetails.Add(newPD);
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

            int index = lvAvaibleIngredient.ItemContainerGenerator.IndexFromContainer(dep);

            if (index < 0)
                return;

            _currentProduct.ProductDetails.Remove(PDTempData.pdtList[index].ProDe);
            PDTempData.pdtList.RemoveAt(index);
            lvDetails.ItemsSource = PDTempData.pdtList;
            lvDetails.Items.Refresh();
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

                _currentProduct.ProductDetails.ToList()[index].UnitUse = cbo.SelectedItem.ToString();
                PDTempData.pdtList[index].ProDe.UnitUse = cbo.SelectedItem.ToString();
                lvDetails.ItemsSource = PDTempData.pdtList;
                lvDetails.Items.Refresh();
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

                _currentProduct.ProductDetails.ToList()[index].Quan = int.Parse((sender as TextBox).Text.Trim());
                PDTempData.pdtList[index].ProDe.Quan = int.Parse((sender as TextBox).Text.Trim());
                lvDetails.ItemsSource = PDTempData.pdtList;
                lvDetails.Items.Refresh();

            }
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
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
