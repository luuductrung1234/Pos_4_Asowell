using POS.Entities;
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

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SetOrderModeDialog.xaml
    /// </summary>
    public partial class SetOrderModeDialog : Window
    {

        private OrderTemp _ordertemp;

        public SetOrderModeDialog(OrderTemp ordertempTable)
        {
            InitializeComponent();

            _ordertemp = ordertempTable;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int pax;
            decimal price;

            if (string.IsNullOrEmpty(txtPax.Text))
            {
                MessageBox.Show("Please input Pax!");
                return;
            }
            else
            {
                pax = int.Parse(txtPax.Text);
            }

            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Please input price per person!");
                return;
            }
            else
            {
                price = decimal.Parse(txtPrice.Text);
            }

            _ordertemp.Pax = pax;
            decimal Total = price * pax;

            if (Total == null || Total == 0)
            {
                _ordertemp.TotalPrice = 0;

                _ordertemp.TotalPriceNonDisc = 0;
                _ordertemp.Svc = 0;
                _ordertemp.Vat = 0;
                _ordertemp.SaleValue = 0;
            }
            else
            {
                decimal SaleValue = Total;
                decimal Svc = (Total * 5) / 100;
                decimal Vat = ((Total + (Total * 5) / 100) * 10) / 100;
                Total = (Total + (Total * 5) / 100) + (((Total + (Total * 5) / 100) * 10) / 100);

                _ordertemp.TotalPrice = Total;
                _ordertemp.TotalPriceNonDisc = (decimal)Math.Round(Total, 3);
                _ordertemp.Svc = Math.Round(Svc, 3);
                _ordertemp.Vat = Math.Round(Vat, 3);
                _ordertemp.SaleValue = Math.Round(SaleValue, 3);
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }
    }
}
