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
        private AdminwsOfCloudPOS _unitofwork;
        Customer ctm;
        List<Customer> allcus;
        CustomerAddOrUpdateDialog _cusAddOrUpdate;
        public CustomerPage(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            allcus = _unitofwork.CustomerRepository.Get(x => x.Deleted.Equals(0)).ToList();
            lvDataCustomer.ItemsSource = allcus;
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
            _cusAddOrUpdate = new CustomerAddOrUpdateDialog(_unitofwork, null);
            _cusAddOrUpdate.ShowDialog();

            allcus = _unitofwork.CustomerRepository.Get(x => x.Deleted.Equals(0)).ToList();
            lvDataCustomer.ItemsSource = allcus;
            lvDataCustomer.UnselectAll();
            lvDataCustomer.Items.Refresh();
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataCustomer.SelectedItem == null)
            {
                MessageBox.Show("Custoer must be selected to update! Choose again!");
                return;
            }

            _cusAddOrUpdate = new CustomerAddOrUpdateDialog(_unitofwork, ctm);
            _cusAddOrUpdate.ShowDialog();
            lvDataCustomer.UnselectAll();
            lvDataCustomer.Items.Refresh();
        }

        private void bntDel_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataCustomer.SelectedItem == null)
            {
                MessageBox.Show("Customer must be selected to delete! Choose again!");
                return;
            }

            var delCus = lvDataCustomer.SelectedItem as Customer;
            if (delCus != null)
            {
                MessageBoxResult delMess = MessageBox.Show("Do you want to delete " + delCus.Name + "(" + delCus.CusId + ")?", "Warning! Are you sure?", MessageBoxButton.YesNo);
                if (delMess == MessageBoxResult.Yes)
                {
                    delCus.Deleted = 1;
                    _unitofwork.CustomerRepository.Update(delCus);
                    _unitofwork.Save();
                    allcus = _unitofwork.CustomerRepository.Get(x => x.Deleted.Equals(0)).ToList();
                    lvDataCustomer.ItemsSource = allcus;
                    lvDataCustomer.UnselectAll();
                    lvDataCustomer.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please choose customer you want to delete and try again!");
            }
        }
    }
}
