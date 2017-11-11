using POS.Entities;
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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    
    public partial class CustomerPage : Page
    {
        private AdminwsOfAsowell _unitofwork;
        Customer ctm;
        public CustomerPage(AdminwsOfAsowell unitofwork)
        {
            
            _unitofwork = unitofwork;
            InitializeComponent();
            lvDataCustomer.ItemsSource = _unitofwork.CustomerRepository.Get();
            for (int i = 0; i <= 100; i++)
            {
                cbodiscount.Items.Add(i.ToString());
            }
        }

        private void lvDataCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ctm = lvDataCustomer.SelectedItem as Customer;
            if (ctm == null)
            {
                txtID.Text = "";
                txtName.Text = "";                
                txtMail.Text = "";
                txtPhone.Text = "";
                cbodiscount.SelectedIndex = 0;
                return;
            }
            txtID.Text = ctm.CusId;
            txtName.Text = ctm.Name;
            txtMail.Text = ctm.Email;
            txtPhone.Text = ctm.Phone;
            cbodiscount.SelectedItem = ctm.Discount.ToString();
        }

        private void bntAddnew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
