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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductCreatorPage.xaml
    /// </summary>
    public partial class ProductCreatorPage : Page
    {
        private AdminwsOfAsowell _unitofwork;
        public ProductCreatorPage(AdminwsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            
            InitializeComponent();
            lvAvaibleIngredient.ItemsSource = _unitofwork.IngredientRepository.Get();
        }


        private void LvAvaibleIngredient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        private void BntDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
