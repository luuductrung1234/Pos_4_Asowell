using POS.Entities;
using POS.Repository.DAL;
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
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for CustomerAddOrUpdateDialog.xaml
    /// </summary>
    public partial class CustomerAddOrUpdateDialog : Window
    {
        AdminwsOfCloudPOS _unitofwork;
        Customer _cus;

        public CustomerAddOrUpdateDialog(AdminwsOfCloudPOS unitofwork, Customer cus)
        {
            _unitofwork = unitofwork;
            if (cus != null)
            {
                _cus = cus;
            }
            InitializeComponent();

            initData();
        }

        private void initData()
        {
            if (_cus != null)
            {
                txtName.Text = _cus.Name;
                txtPhone.Text = _cus.Phone;
                txtMail.Text = _cus.Email;
                txtDiscount.Text = _cus.Discount.ToString();
            }
        }

        private void bntAddnew_Click(object sender, RoutedEventArgs e)
        {
            //check name
            string namee = txtName.Text.Trim();
            if (namee.Length == 0 || namee.Length > 50)
            {
                MessageBox.Show("Name is not valid!");
                txtName.Focus();
                return;
            }

            //check phone
            string phone = txtPhone.Text.Trim();
            if (phone.Length == 0 || phone.Length > 20)
            {
                MessageBox.Show("Phone is not valid!");
                txtPhone.Focus();
                return;
            }

            //check email
            string email = txtMail.Text.Trim();
            if (!Regex.IsMatch(email, "[\\w\\d]+[@][\\w]+[.][\\w]+"))
            {
                MessageBox.Show("Email is not valid!");
                txtMail.Focus();
                return;
            }

            //check discount
            int discount = int.Parse(txtDiscount.Text.Trim());
            if (discount < 0 || discount > 100)
            {
                MessageBox.Show("Discount value is not valid!");
                txtDiscount.Focus();
                return;
            }

            if (_cus == null) //insert
            {
                Customer checkcus = new Customer
                {
                    CusId = "",
                    Name = namee,
                    Email = email,
                    Phone = phone,
                    Discount = discount,
                    Deleted = 0
                };

                _unitofwork.CustomerRepository.Insert(checkcus);
                _unitofwork.Save();

                MessageBox.Show("Insert " + checkcus.Name + "(" + checkcus.CusId + ") successful!");
                this.Close();
            }
            else //update
            {
                _cus.Name = namee;
                _cus.Email = email;
                _cus.Phone = phone;
                _cus.Discount = discount;
                _cus.Deleted = 0;

                _unitofwork.CustomerRepository.Update(_cus);
                _unitofwork.Save();

                MessageBox.Show("Update " + _cus.Name + "(" + _cus.CusId + ") successful!");
                this.Close();
            }
        }

        private void bntCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }
    }
}
