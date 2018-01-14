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
        AdminwsOfCloudPOS _unitofwork;
        List<Ingredient> _ingrelist;
        List<ReceiptNote> _relist;
        List<ReceiptNoteDetail> _rnlist;
        public ReceiptNotePage(AdminwsOfCloudPOS unitofwork, AdminRe admin)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            _relist = _unitofwork.ReceiptNoteRepository.Get(includeProperties: "Employee").ToList();
            _relist = _relist.Where(x => x.Employee.Manager.Equals(admin.AdId)).ToList();
            lvReceptNote.ItemsSource = _relist;
            _rnlist = _unitofwork.ReceiptNoteDsetailsRepository.Get(includeProperties: "Ingredient").ToList();
            List<ReceiptNoteDetail> _rnTempList = new List<ReceiptNoteDetail>();
            foreach (var receiptdetails in _rnlist)
            {
                bool found = false;
                foreach (var receiptnote in _relist)
                {
                    if (receiptdetails.RnId.Equals(receiptnote.RnId))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    _rnTempList.Add(receiptdetails);
            }
            _rnlist = _rnTempList;
            lvReceiptNoteDetail.ItemsSource = _rnlist;

            this.Loaded += ReceiptNotePage_Loaded;
        }

        private void ReceiptNotePage_Loaded(object sender, RoutedEventArgs e)
        {
            _ingrelist = _unitofwork.IngredientRepository.Get(x => x.Deleted == 0).ToList();
            initData();
        }

        private void initData()
        {
            isRaiseEvent = false;
            List<dynamic> ingl = new List<dynamic>();
            ingl.Add(new { Id = "--", Name = "--" });
            foreach (var p in _ingrelist)
            {
                ingl.Add(new { Id = p.IgdId, Name = p.Name });
            }

            cboIngre.ItemsSource = ingl;
            cboIngre.SelectedValuePath = "Id";
            cboIngre.DisplayMemberPath = "Name";
            cboIngre.SelectedValue = "--";
            isRaiseEvent = true;
        }

        private void lvReceptNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReceiptNote rn = lvReceptNote.SelectedItem as ReceiptNote;
            if (rn != null)
            {
                lvReceiptNoteDetail.ItemsSource = _unitofwork.ReceiptNoteDsetailsRepository.Get(c => c.RnId.Equals(rn.RnId));
            }
            else
            {
                lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
            }
        }

        List<ReceiptNoteDetail> filterrn = new List<ReceiptNoteDetail>();
        List<ReceiptNote> filterre = new List<ReceiptNote>();
        bool isRaiseEvent = true;
        private void cboIngre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterre = new List<ReceiptNote>();
            lvReceptNote.UnselectAll();
            lvReceiptNoteDetail.UnselectAll();
            if (isRaiseEvent)
            {
                ComboBox cboi = sender as ComboBox;
                string ingid = cboi.SelectedValue.ToString();
                if (!ingid.Equals("--"))
                {
                    filterrn = _rnlist.Where(x => x.IgdId.Equals(ingid)).ToList();
                    var odd = filterrn.GroupBy(x => x.RnId).Select(y => y.ToList()).ToList();

                    foreach (var i in odd)
                    {
                        foreach (var j in i)
                        {
                            filterre.Add(_relist.Where(x => x.RnId.Equals(j.RnId)).FirstOrDefault());
                            break;
                        }
                    }

                    if (filterre.Count != 0 && pickOrderDate.SelectedDate == null)
                    {
                        lvReceptNote.ItemsSource = filterre;
                        lvReceptNote.Items.Refresh();
                        lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                        lvReceiptNoteDetail.Items.Refresh();
                    }
                    else if (filterre.Count != 0 && pickOrderDate.SelectedDate != null)
                    {
                        lvReceptNote.ItemsSource = filterre.Where(x => x.Inday.ToShortDateString().Equals(((DateTime)pickOrderDate.SelectedDate).ToShortDateString())).ToList();
                        lvReceptNote.Items.Refresh();
                        lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                        lvReceiptNoteDetail.Items.Refresh();
                    }
                    else
                    {
                        lvReceptNote.ItemsSource = new List<ReceiptNote>();
                        lvReceptNote.Items.Refresh();
                        lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                        lvReceiptNoteDetail.Items.Refresh();
                    }
                }
                else
                {
                    if (pickOrderDate.SelectedDate == null)
                    {
                        lvReceptNote.ItemsSource = _relist;
                        lvReceptNote.Items.Refresh();
                        lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                        lvReceiptNoteDetail.Items.Refresh();
                    }
                    else
                    {
                        lvReceptNote.ItemsSource = _relist.Where(x => x.Inday.ToShortDateString().Equals(((DateTime)pickOrderDate.SelectedDate).ToShortDateString())).ToList();
                        lvReceptNote.Items.Refresh();
                        lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                        lvReceiptNoteDetail.Items.Refresh();
                    }
                }
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pickOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker pick = sender as DatePicker;
            if (pick.SelectedDate == null)
            {
                return;
            }

            if (cboIngre.SelectedValue.Equals("--"))
            {
                lvReceptNote.ItemsSource = _relist.Where(x => x.Inday.ToShortDateString().Equals(((DateTime)pick.SelectedDate).ToShortDateString()));
                lvReceptNote.Items.Refresh();
                lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                lvReceiptNoteDetail.Items.Refresh();
            }
            else
            {
                if (filterre.Count != 0)
                {
                    lvReceptNote.ItemsSource = filterre.Where(x => x.Inday.ToShortDateString().Equals(((DateTime)pick.SelectedDate).ToShortDateString()));
                    lvReceptNote.Items.Refresh();
                }
                else
                {
                    lvReceptNote.ItemsSource = new List<ReceiptNote>();
                    lvReceptNote.Items.Refresh();
                }

                lvReceiptNoteDetail.ItemsSource = new List<ReceiptNoteDetail>();
                lvReceiptNoteDetail.Items.Refresh();
            }
        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportOptionDialog(new ReceiptNoteReport(), _unitofwork);
            optionDialog.Show();
        }

    }
}
