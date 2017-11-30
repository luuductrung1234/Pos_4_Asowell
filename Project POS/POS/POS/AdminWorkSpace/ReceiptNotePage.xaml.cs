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
using POS.Helper.PrintHelper.Report;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ReceiptNotePage.xaml
    /// </summary>
    public partial class ReceiptNotePage : Page
    {
        AdminwsOfCloud _unitofwork;
        public ReceiptNotePage(AdminwsOfCloud unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            lvReceptNote.ItemsSource = unitofwork.ReceiptNoteRepository.Get(includeProperties: "Employee");
            lvReceiptNoteDetail.ItemsSource = unitofwork.ReceiptNoteDsetailsRepository.Get(includeProperties: "Ingredient");
        }

        private void lvReceptNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReceiptNote rn = lvReceptNote.SelectedItem as ReceiptNote;
            lvReceiptNoteDetail.ItemsSource = _unitofwork.ReceiptNoteDsetailsRepository.Get(c => c.RnId.Equals(rn.RnId));
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportOptionDialog(new ReceiptNoteReport(), _unitofwork);
            optionDialog.Show();
        }
    }
}
