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
    /// Interaction logic for SalaryPage.xaml
    /// </summary>
    public partial class SalaryPage : Page
    {
        AdminwsOfAsowell _unitofwork;
        public SalaryPage(AdminwsOfAsowell unitofwork)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            var SalList = unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories");
            var WhList = unitofwork.WorkingHistoryRepository.Get(includeProperties: "Employee");
            lvSalary.ItemsSource = SalList;
            lvWokingHistory.ItemsSource = WhList;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SalaryNote emp = lvSalary.SelectedItem as SalaryNote;
            lvWokingHistory.ItemsSource=_unitofwork.WorkingHistoryRepository.Get(c=>c.EmpId.Equals(emp.EmpId));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
