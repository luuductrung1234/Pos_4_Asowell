using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for InputReceiptNote.xaml
    /// </summary>
    public partial class InputReceiptNote : Page
    {
        private AdminwsOfAsowell _unitofork;
        private Ingredient ingredient;
        public InputReceiptNote(AdminwsOfAsowell unitofork)
        {
            _unitofork = unitofork;
            InitializeComponent();
            lvDataIngredient.ItemsSource = _unitofork.IngredientRepository.Get();
        }

        private void BntAddnew_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void LvDataIngredient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ingredient = lvDataIngredient.SelectedItem as Ingredient;
            if (ingredient == null)
            {
                
                txtName.Text = "";
                txtAmount.Text = "0";
                txtStandardPrice.Text = "";

                return;
            }
            txtName.Text = ingredient.Name;
            txtAmount.Text = "0";
            txtStandardPrice.Text = (ingredient.StandardPrice * decimal.Parse(txtAmount.Text)).ToString();
        }

        private void TxtAmount_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAmount.Text.Equals(""))
            {

                txtStandardPrice.Text = "0.000";

            }
            else
            {
                
                txtStandardPrice.Text = (ingredient.StandardPrice * decimal.Parse(txtAmount.Text)).ToString();
            }
            


        }

        private void TxtAmount_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }


        private void TxtAmount_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtAmount.Text.Equals(""))
            {

               txtStandardPrice.Text = "0.000";
            }
            else
            {
                 txtStandardPrice.Text = (ingredient.StandardPrice * decimal.Parse(txtAmount.Text)).ToString();

            }

        }
    }
}
