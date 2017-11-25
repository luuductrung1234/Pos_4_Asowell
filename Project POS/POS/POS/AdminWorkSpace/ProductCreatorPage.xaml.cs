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
        private AdminwsOfAsowell _unitofork;
        public ProductCreatorPage(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            lvAvaibleIngredient.ItemsSource = _unitofork.IngredientRepository.Get();
            InitializeComponent();
        }


        private void LvAvaibleIngredient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void BntDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
