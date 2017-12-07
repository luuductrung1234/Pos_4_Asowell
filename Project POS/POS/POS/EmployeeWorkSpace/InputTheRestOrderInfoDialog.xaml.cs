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
using NPOI.SS.Formula.Functions;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Support;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for InputTheRestOrderInfoDialog.xaml
    /// </summary>
    public partial class InputTheRestOrderInfoDialog : Window
    {
        private Entities.OrderNote currentOrder;
        private string _payMethod;
        public bool IsSuccess { get; set; }

        public InputTheRestOrderInfoDialog(OrderNote currentOrder)
        {
            InitializeComponent();

            this.currentOrder = currentOrder;
            TbTotalPrice.Text = TbTotalPrice.Text + string.Format("{0:0.000}", currentOrder.TotalPrice) + " VND";
            _payMethod = "";
            IsSuccess = false;

            CboPaymentMethod.ItemsSource = new List<string>()
            {
                "Cash",
                "Cheque",
                "Deferred",
                "International",
                "Credit",
                "OnAcount"
            };
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cboPayment = sender as ComboBox;
            this._payMethod = cboPayment.SelectedValue.ToString();
        }

        public bool MyShowDialog()
        {
            this.ShowDialog();
            return IsSuccess;
        }

        private void DoPayment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_payMethod))
            {
                MessageBox.Show("please choose Payment Method!");
                return;
            }


            if (_payMethod == PaymentMethod.Cash.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.Cash;
            }
            else if (_payMethod == PaymentMethod.Cheque.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.Cheque;
            }
            else if (_payMethod == PaymentMethod.Credit.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.Credit;
            }
            else if (_payMethod == PaymentMethod.Deferred.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.Deferred;
            }
            else if (_payMethod == PaymentMethod.International.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.International;
            }
            else if (_payMethod == PaymentMethod.OnAcount.ToString())
            {
                currentOrder.paymentMethod = (int)PaymentMethod.OnAcount;
            }


            try
            {
                decimal cusPay = decimal.Parse(KbInput.InputValue);

                if (cusPay < currentOrder.TotalPrice)
                {
                    MessageBox.Show("All payment ground up to higher number!");
                    return;
                }

                currentOrder.CustomerPay = cusPay;
                currentOrder.PayBack = currentOrder.CustomerPay - currentOrder.TotalPrice;
                IsSuccess = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect input!");
                return;
            }
        }


        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
