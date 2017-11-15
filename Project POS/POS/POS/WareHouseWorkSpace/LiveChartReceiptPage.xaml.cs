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
using LiveCharts;
using LiveCharts.Wpf;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for LiveChartReceiptPage.xaml
    /// </summary>
    public partial class LiveChartReceiptPage : Page
    {
        AdminwsOfAsowell _unitofwork;

        private decimal TotalOct = 0;
        public LiveChartReceiptPage(AdminwsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            decimal totalMonthAmount = 0;
            var receiptList = unitofwork.ReceiptNoteRepository.Get();
            ChartValues<decimal> ValueExpense = new ChartValues<decimal>();
            var grReceipt = receiptList.GroupBy(x => x.Inday.Month).Select(x => x.ToList());
            foreach (var re in grReceipt)
            {
                foreach (var total in re.ToArray())
                {
                   
                    totalMonthAmount += total.TotalAmount;
                }

                ValueExpense.Add(totalMonthAmount);
            }
            
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "revenue",
                    Values = new ChartValues<decimal> { 0, 0, 0, 0,0,0,0,0,0, 0,0,0 }

                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "expense",
                Values = ValueExpense//new ChartValues<decimal> { 0, 0, 0, 0, 0, 0, 0, 0, 0, TotalOct, 0, 0 }
            });
            Formatter = value => value.ToString();
            InitializeComponent();
            DataContext = this;
            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May ", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
        }
        public SeriesCollection SeriesCollection { get; set; }
        public Func<decimal, string> Formatter { get; set; }
        public string[] Labels { get; set; }




        private void RdoDoM_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RdoMoY_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
