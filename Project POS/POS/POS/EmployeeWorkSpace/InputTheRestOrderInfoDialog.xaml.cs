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
        public InputTheRestOrderInfoDialog(OrderNote currentOrder)
        {
            InitializeComponent();

            KbInput.currentOrder = currentOrder;
            KbInput.parent = this;
            KbInput.Type = KeyboardControl.ORDER_PAYMENT_TYPE;
            KbInput.payMethod = "";

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
            KbInput.payMethod = cboPayment.SelectedValue.ToString();
        }
    }
}
