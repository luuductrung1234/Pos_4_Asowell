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
    /// Interaction logic for OrderNotePage.xaml
    /// </summary>
    public partial class OrderNotePage : Page
    {
        AdminwsOfAsowell _unitofwork;
        public OrderNotePage(AdminwsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            lvOrderNote.ItemsSource = unitofwork.OrderRepository.Get(includeProperties: "Employee,Customer");
            lvOrderNoteDetails.ItemsSource = unitofwork.OrderNoteDetailsRepository.Get(includeProperties: "Product");
        }

        private void lvOrderNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderNote odn = lvOrderNote.SelectedItem as OrderNote;
            lvOrderNoteDetails.ItemsSource = _unitofwork.OrderNoteDetailsRepository.Get(c => c.OrdernoteId.Equals(odn.OrdernoteId));
        }

        private void cboPrduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lvOrderNoteDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
