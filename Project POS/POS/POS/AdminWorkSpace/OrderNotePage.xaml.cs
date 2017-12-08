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
        List<OrderNote> _ordernotelist;
        List<OrderNoteDetail> _ordernotedetailslist;
        public OrderNotePage(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            _ordernotelist = _unitofwork.OrderRepository.Get(includeProperties: "Employee,Customer").ToList();
            lvOrderNote.ItemsSource = _ordernotelist;
            _ordernotedetailslist = _unitofwork.OrderNoteDetailsRepository.Get(includeProperties: "Product").ToList();
            lvOrderNoteDetails.ItemsSource = _ordernotedetailslist;

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
        private void cboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isRaiseEvent)
            {
                ComboBox cbopro = sender as ComboBox;
                string proid = cbopro.SelectedValue.ToString();
                if (!proid.Equals("--"))
                {
                    var od = _ordernotedetailslist.Where(x => x.ProductId.Equals(proid)).ToList();
                    var odd = od.GroupBy(x => x.OrdernoteId).Select(y => y.ToList()).ToList();
                    List<OrderNote> newlist = new List<OrderNote>();
                    foreach (var i in odd)
                    {
                        foreach (var j in i)
                        {
                            newlist.Add(_ordernotelist.Where(x => x.OrdernoteId.Equals(j.OrdernoteId)).FirstOrDefault());
                            break;
                        }
                    }

                    lvOrderNote.Items.Clear();
                    lvOrderNote.ItemsSource = newlist;
                    lvOrderNoteDetails.Items.Clear();
                    lvOrderNoteDetails.ItemsSource = od;
                }
                else
                {
                    lvOrderNote.Items.Clear();
                    lvOrderNote.ItemsSource = _ordernotelist;
                    lvOrderNoteDetails.Items.Clear();
                    lvOrderNoteDetails.ItemsSource = _ordernotedetailslist;
                }
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
