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
using System.Windows.Threading;
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
