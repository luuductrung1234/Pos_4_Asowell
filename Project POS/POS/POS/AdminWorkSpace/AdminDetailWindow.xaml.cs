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
        private AdminwsOfAsowell _unitofwork;
        AdminRe ad;
        public AdminDetailWindow(string UserName, AdminwsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            loadData(UserName);
            ad = new AdminRe();
            lvDataEmployee.ItemsSource = unitofwork.EmployeeRepository.Get();
        }

        private void loadData(string UserName)
        {
            foreach (var item in _unitofwork.AdminreRepository.Get())
            {
                if (item.Username.Equals(UserName))
                {
                    ad = item;
                    break;

                }
            }
            this.EmployeeInfo.DataContext = ad;
        }
    }
}
