using POS.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using POS.Repository.DAL;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SettingFood.xaml
    /// </summary>
    public partial class SettingFoodPage : Page
    {
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;

        public SettingFoodPage(EmployeewsOfCloudPOS cloudPosUnitofwork)
        {
            _cloudPosUnitofwork = cloudPosUnitofwork;
            InitializeComponent();
            lvData.ItemsSource = _cloudPosUnitofwork.ProductRepository.Get(c=>c.Deleted.Equals(0));
            for(int i = 0; i <= 100; i++)
            {
                cbopromotion.Items.Add(i.ToString());
            }

            
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bntUpdate.IsEnabled = true;
            Product pro = lvData.SelectedItem as Product;

            txtID.Text = pro.ProductId;
            txtName.Text = pro.Name;
            //txtPrice.Text = pro.Price.ToString();
            txtPrice.Text=string.Format("{0:0.000}", pro.Price);
            cbopromotion.SelectedItem = pro.Discount.ToString();
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (bntUpdate.Content.Equals("Update")){
                txtName.IsEnabled = true;
                txtPrice.IsEnabled = true;
                cbopromotion.IsEnabled = true;
                bntUpdate.Content = "Save";
            }else if (bntUpdate.Content.Equals("Save"))
            {
                Product p = _cloudPosUnitofwork.ProductRepository.GetById(txtID.Text);
                p.Discount= int.Parse(cbopromotion.SelectedValue.ToString());
                _cloudPosUnitofwork.ProductRepository.Update(p);
                _cloudPosUnitofwork.Save();


                txtName.IsEnabled = false;
                txtPrice.IsEnabled = false;
                cbopromotion.IsEnabled = false;
                bntUpdate.Content = "Update";
            }
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
