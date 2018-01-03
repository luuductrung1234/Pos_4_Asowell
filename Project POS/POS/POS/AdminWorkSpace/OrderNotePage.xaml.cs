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
        public OrderNotePage(AdminwsOfCloudPOS unitofwork, AdminRe admin)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            _ordernotelist = _unitofwork.OrderRepository.Get(includeProperties: "Employee,Customer").ToList();
            _ordernotelist = _ordernotelist.Where(x => x.Employee.Manager.Equals(admin.AdId)).ToList();
            lvOrderNote.ItemsSource = _ordernotelist;
            _ordernotedetailslist = _unitofwork.OrderNoteDetailsRepository.Get(includeProperties: "Product").ToList();
            List<OrderNoteDetail> _orderdetailsTempList = new List<OrderNoteDetail>();
            foreach (var orderdetails in _ordernotedetailslist)
            {
                bool found = false;
                foreach(var order in _ordernotelist)
                {
                    if (orderdetails.OrdernoteId.Equals(order.OrdernoteId))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    _orderdetailsTempList.Add(orderdetails);
                }
            }
            _ordernotedetailslist = _orderdetailsTempList;
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
            if(odn != null)
            {
                lvOrderNoteDetails.ItemsSource = _unitofwork.OrderNoteDetailsRepository.Get(c => c.OrdernoteId.Equals(odn.OrdernoteId)).ToList();
            }
            else
            {
                lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
            }
        }

        List<OrderNoteDetail> filterod = new List<OrderNoteDetail>();
        List<OrderNote> filtero = new List<OrderNote>();
        bool isRaiseEvent = true;
        private void cboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtero = new List<OrderNote>();
            lvOrderNote.UnselectAll();
            lvOrderNoteDetails.UnselectAll();
            if (isRaiseEvent)
            {
                ComboBox cbopro = sender as ComboBox;
                string proid = cbopro.SelectedValue.ToString();
                if (!proid.Equals("--"))
                {
                    filterod = _ordernotedetailslist.Where(x => x.ProductId.Equals(proid)).ToList();
                    var odd = filterod.GroupBy(x => x.OrdernoteId).Select(y => y.ToList()).ToList();

                    foreach (var i in odd)
                    {
                        foreach (var j in i)
                        {
                            filtero.Add(_ordernotelist.Where(x => x.OrdernoteId.Equals(j.OrdernoteId)).FirstOrDefault());
                            break;
                        }
                    }
                    
                    if(filtero.Count != 0 && pickOrderDate.SelectedDate == null)
                    {
                        lvOrderNote.ItemsSource = filtero;
                        lvOrderNote.Items.Refresh();
                        lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                        lvOrderNoteDetails.Items.Refresh();
                    }
                    else if(filtero.Count != 0 && pickOrderDate.SelectedDate != null)
                    {
                        lvOrderNote.ItemsSource = filtero.Where(x => x.Ordertime.ToShortDateString().Equals(((DateTime)pickOrderDate.SelectedDate).ToShortDateString())).ToList();
                        lvOrderNote.Items.Refresh();
                        lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                        lvOrderNoteDetails.Items.Refresh();
                    }
                    else
                    {
                        lvOrderNote.ItemsSource = new List<OrderNote>();
                        lvOrderNote.Items.Refresh();
                        lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                        lvOrderNoteDetails.Items.Refresh();
                    }
                }
                else
                {
                    if(pickOrderDate.SelectedDate == null)
                    {
                        lvOrderNote.ItemsSource = _ordernotelist;
                        lvOrderNote.Items.Refresh();
                        lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                        lvOrderNoteDetails.Items.Refresh();
                    }
                    else
                    {
                        lvOrderNote.ItemsSource = _ordernotelist.Where(x => x.Ordertime.ToShortDateString().Equals(((DateTime)pickOrderDate.SelectedDate).ToShortDateString())).ToList();
                        lvOrderNote.Items.Refresh();
                        lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                        lvOrderNoteDetails.Items.Refresh();
                    }
                }
            }
        }

        private void txtSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lvOrderNoteDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void pickOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker pick = sender as DatePicker;
            if(pick.SelectedDate == null)
            {
                return;
            }

            if(cboProduct.SelectedValue.Equals("--"))
            {
                lvOrderNote.ItemsSource = _ordernotelist.Where(x => x.Ordertime.ToShortDateString().Equals(((DateTime)pick.SelectedDate).ToShortDateString()));
                lvOrderNote.Items.Refresh();
                lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                lvOrderNoteDetails.Items.Refresh();
            }
            else
            {
                if(filtero.Count != 0)
                {
                    lvOrderNote.ItemsSource = filtero.Where(x => x.Ordertime.ToShortDateString().Equals(((DateTime)pick.SelectedDate).ToShortDateString()));
                    lvOrderNote.Items.Refresh();
                }
                else
                {
                    lvOrderNote.ItemsSource = new List<OrderNote>();
                    lvOrderNote.Items.Refresh();
                }
                
                lvOrderNoteDetails.ItemsSource = new List<OrderNoteDetail>();
                lvOrderNoteDetails.Items.Refresh();
            }
        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportOptionDialog(new OrderNoteReport(), _unitofwork);
            optionDialog.Show();
        }

    }
}
