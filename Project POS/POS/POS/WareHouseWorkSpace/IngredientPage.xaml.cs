using System.Collections.Generic;
using System.Windows.Controls;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : Page
    {
        private AdminwsOfCloudPOS _unitofwork;
        

        public IngredientPage(AdminwsOfCloudPOS unitofwork, List<Ingredient> IngdList)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            lvItem.ItemsSource = IngdList;
        }
    }
}
