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
    /// Interaction logic for OrderNotePage.xaml
    /// </summary>
    public partial class OrderNotePage : Page
    {
        AdminwsOfCloudPOS _unitofwork;
        List<Product> _proList;
        public OrderNotePage(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            lvOrderNote.ItemsSource = _unitofwork.OrderRepository.Get(includeProperties: "Employee,Customer");
            lvOrderNoteDetails.ItemsSource = _unitofwork.OrderNoteDetailsRepository.Get(includeProperties: "Product");

            this.Loaded += OrderNotePage_Loaded;
        }

        private void OrderNotePage_Loaded(object sender, RoutedEventArgs e)
        {
            _proList = _unitofwork.ProductRepository.Get(x => x.Deleted == 0).ToList();
            initData();
        }

        private void initData()
        {
            isRaiseEvent = false;
            List<dynamic> prol = new List<dynamic>();
            prol.Add(new { Id = "--", Name = "--" });
            cboProduct.Items.Add("--");
            foreach (var p in _proList)
            {
                prol.Add(new { Id = p.ProductId, Name = p.Name });
            }

            cboProduct.ItemsSource = prol;
            cboProduct.SelectedValuePath = "Id";
            cboProduct.DisplayMemberPath = "Name";
            cboProduct.SelectedValue = "--";

            isRaiseEvent = true;
        }

        private void lvOrderNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderNote odn = lvOrderNote.SelectedItem as OrderNote;
            lvOrderNoteDetails.ItemsSource = _unitofwork.OrderNoteDetailsRepository.Get(c => c.OrdernoteId.Equals(odn.OrdernoteId));
        }

        bool isRaiseEvent = true;
        private void cboPrduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isRaiseEvent)
            {

            }
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

        private void pickOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportOptionDialog(new OrderNoteReport(), _unitofwork);
            optionDialog.Show();
        }

    }
}
