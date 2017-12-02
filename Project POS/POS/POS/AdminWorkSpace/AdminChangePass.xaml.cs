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
using System.Windows.Shapes;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for AdminChangePass.xaml
    /// </summary>
    public partial class AdminChangePass : Window
    {
        private AdminwsOfCloudPOS _unitofwork;
        private AdminRe _admin;

        public AdminChangePass(AdminwsOfCloudPOS unitofwork, AdminRe admin)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            _admin = admin;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string oldPass = txtPass.Password.Trim();
            if(!oldPass.Equals(_admin.Pass))
            {
                MessageBox.Show("Your old password is incorrect!");
                txtPass.Focus();
                return;
            }

            string newPass = txtNewPass.Password.Trim();
            if (newPass.Length == 0 || newPass.Length > 50)
            {
                MessageBox.Show("New password is not valid!");
                txtNewPass.Focus();
                return;
            }

            string passcon = txtConNew.Password.Trim();
            if (!passcon.Equals(newPass))
            {
                MessageBox.Show("Confirm new password is not match!");
                txtConNew.Focus();
                return;
            }

            _admin.Pass = newPass;
            _unitofwork.AdminreRepository.Update(_admin);
            _unitofwork.Save();
            MessageBox.Show("Your password was changed!");
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
