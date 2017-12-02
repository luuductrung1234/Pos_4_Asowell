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
    /// Interaction logic for AdminDetailWindow.xaml
    /// </summary>
    
    public partial class AdminDetailWindow : Window
    {
        private AdminwsOfCloudPOS _unitofwork;
        private AdminRe admin;
        private List<Employee> empwithad;
        public AdminDetailWindow(AdminwsOfCloudPOS unitofwork, AdminRe ad)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            admin = ad;
            empwithad = _unitofwork.EmployeeRepository.Get(x => x.Manager.Equals(admin.AdId) && x.Deleted.Equals(0)).ToList();
            lvDataEmployee.ItemsSource = empwithad;
            loadAdData();
        }

        private void loadAdData()
        {
            this.AdminInfo.DataContext = admin;
            //txtName.IsEnabled = false;
        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            //check name
            string namee = txtName.Text.Trim();
            if (namee.Length == 0 || namee.Length > 50)
            {
                MessageBox.Show("Name is not valid!");
                txtName.Focus();
                return;
            }

            admin.Name = namee;
            //admin.Employees.Clear();
            _unitofwork.AdminreRepository.Update(admin);
            _unitofwork.Save();
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePass adPass = new AdminChangePass(_unitofwork, admin);
            adPass.ShowDialog();
        }
    }
}
