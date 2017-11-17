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

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : Page
    {
        AdminwsOfAsowell _unitofwork;
        public IngredientPage(AdminwsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            lvItem.ItemsSource = unitofwork.IngredientRepository.Get(includeProperties: "WareHouse");
        }
    }
}
