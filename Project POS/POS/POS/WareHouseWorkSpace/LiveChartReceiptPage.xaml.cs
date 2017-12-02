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
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for LiveChartReceiptPage.xaml
    /// </summary>
    public partial class LiveChartReceiptPage : Page
    {
        AdminwsOfCloudPOS _unitofwork;
        private ChartValues<decimal> Average1;
        private ChartValues<decimal> Average2;
        private ChartValues<decimal> ValueExpense;
        private ChartValues<decimal> ValueRevenue;
        public LiveChartReceiptPage(AdminwsOfCloudPOS unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();

            DispatcherTimer RefreshTimer = new DispatcherTimer();
            RefreshTimer.Tick += Refresh_Tick;
            RefreshTimer.Interval = new TimeSpan(0, 5, 0);
            RefreshTimer.Start();
            Loaded += LiveChartReceiptPage_Load;


            Average2 = new ChartValues<decimal>();
            Average1 = new ChartValues<decimal>();
            ValueExpense = new ChartValues<decimal>();
            ValueRevenue = new ChartValues<decimal>();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Expense",
                    Values = ValueExpense
                }
               

            };
            Formatter = value => value.ToString();

            DataContext = this;
            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May ", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            decimal totalReceipt = 0;
            decimal totalOrder = 0;
            var receiptList = _unitofwork.ReceiptNoteRepository.Get();
            decimal AverageReceipt = 0;
            if (receiptList != null && receiptList.Any())
            {
                foreach (var n in receiptList)
                {
                    totalReceipt += n.TotalAmount;
                }
                AverageReceipt = totalReceipt / (receiptList.Count());
            }

            var OrderList = _unitofwork.OrderRepository.Get();
            decimal AveragOrder = 0;
            if (OrderList != null && OrderList.Any())
            {
                foreach (var n in OrderList)
                {
                    totalOrder += n.TotalPrice;
                }
                AveragOrder = totalOrder / (OrderList.Count());
            }

            ValueRevenue.Clear();
            ValueExpense.Clear();
            Average1.Clear();
            Average2.Clear();

            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);

            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);


            loadDataExpense(_unitofwork, ValueExpense);
            loadDataRevenue(_unitofwork, ValueRevenue);
        }

        private void LiveChartReceiptPage_Load(object sender, RoutedEventArgs e)
        {
            decimal totalReceipt = 0;
            decimal totalOrder = 0;
            var receiptList = _unitofwork.ReceiptNoteRepository.Get();
            decimal AverageReceipt = 0;
            if (receiptList != null && receiptList.Any())
            {
                foreach (var n in receiptList)
                {
                    totalReceipt += n.TotalAmount;
                }
                AverageReceipt = totalReceipt / (receiptList.Count());
            }

            var OrderList = _unitofwork.OrderRepository.Get();
            decimal AveragOrder = 0;
            if (OrderList != null && OrderList.Any())
            {
                foreach (var n in OrderList)
                {
                    totalOrder += n.TotalPrice;
                }
                AveragOrder = totalOrder / (OrderList.Count());
            }

            ValueRevenue.Clear();
            ValueExpense.Clear();
            Average1.Clear();
            Average2.Clear();

            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);
            Average1.Add(AverageReceipt);

            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);
            Average2.Add(AveragOrder);


            loadDataExpense(_unitofwork, ValueExpense);
            loadDataRevenue(_unitofwork, ValueRevenue);
        }

        private void loadDataExpense(AdminwsOfCloudPOS unitofwork, ChartValues<decimal> ValueExpense)
        {
            
            decimal totalMonthAmount1 = 0;
            decimal totalMonthAmount2 = 0;
            decimal totalMonthAmount3 = 0;
            decimal totalMonthAmount4 = 0;
            decimal totalMonthAmount5 = 0;
            decimal totalMonthAmount6 = 0;
            decimal totalMonthAmount7 = 0;
            decimal totalMonthAmount8 = 0;
            decimal totalMonthAmount9 = 0;
            decimal totalMonthAmount10 = 0;
            decimal totalMonthAmount11 = 0;
            decimal totalMonthAmount12 = 0;

            var valueM1 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 1);
            var valueM2 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 2);
            var valueM3 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 3);
            var valueM4 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 4);
            var valueM5 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 5);
            var valueM6 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 6);
            var valueM7 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 7);
            var valueM8 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 8);
            var valueM9 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 9);
            var valueM10 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 10);
            var valueM11 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 11);
            var valueM12 = unitofwork.ReceiptNoteRepository.Get(c => c.Inday.Month == 12);
            FindValueInMonthReceiptNote(ValueExpense, valueM1, totalMonthAmount1, valueM2, totalMonthAmount2, valueM3, totalMonthAmount3, valueM4, totalMonthAmount4, valueM5, totalMonthAmount5, valueM6, totalMonthAmount6, valueM7, totalMonthAmount7, valueM8, totalMonthAmount8, valueM9, totalMonthAmount9, valueM10, totalMonthAmount10, valueM11, totalMonthAmount11, valueM12, totalMonthAmount12);
        }
        private void loadDataRevenue(AdminwsOfCloudPOS unitofwork, ChartValues<decimal> ValueExpense)
        {
            decimal totalMonthAmount1 = 0;
            decimal totalMonthAmount2 = 0;
            decimal totalMonthAmount3 = 0;
            decimal totalMonthAmount4 = 0;
            decimal totalMonthAmount5 = 0;
            decimal totalMonthAmount6 = 0;
            decimal totalMonthAmount7 = 0;
            decimal totalMonthAmount8 = 0;
            decimal totalMonthAmount9 = 0;
            decimal totalMonthAmount10 = 0;
            decimal totalMonthAmount11 = 0;
            decimal totalMonthAmount12 = 0;

            var valueM1 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 1);
            var valueM2 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 2);
            var valueM3 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 3);
            var valueM4 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 4);
            var valueM5 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 5);
            var valueM6 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 6);
            var valueM7 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 7);
            var valueM8 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 8);
            var valueM9 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 9);
            var valueM10 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 10);
            var valueM11 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 11);
            var valueM12 = unitofwork.OrderRepository.Get(c => c.Ordertime.Month == 12);
            FindValueInMonthOrderNote(ValueExpense, valueM1, totalMonthAmount1, valueM2, totalMonthAmount2, valueM3, totalMonthAmount3, valueM4, totalMonthAmount4, valueM5, totalMonthAmount5, valueM6, totalMonthAmount6, valueM7, totalMonthAmount7, valueM8, totalMonthAmount8, valueM9, totalMonthAmount9, valueM10, totalMonthAmount10, valueM11, totalMonthAmount11, valueM12, totalMonthAmount12);
        }

        private static void FindValueInMonthReceiptNote(ChartValues<decimal> ValueExpense, IEnumerable<ReceiptNote> valueM1, decimal totalMonthAmount1,
            IEnumerable<ReceiptNote> valueM2, decimal totalMonthAmount2, IEnumerable<ReceiptNote> valueM3, decimal totalMonthAmount3, IEnumerable<ReceiptNote> valueM4,
            decimal totalMonthAmount4, IEnumerable<ReceiptNote> valueM5, decimal totalMonthAmount5, IEnumerable<ReceiptNote> valueM6,
            decimal totalMonthAmount6, IEnumerable<ReceiptNote> valueM7, decimal totalMonthAmount7, IEnumerable<ReceiptNote> valueM8,
            decimal totalMonthAmount8, IEnumerable<ReceiptNote> valueM9, decimal totalMonthAmount9, IEnumerable<ReceiptNote> valueM10,
            decimal totalMonthAmount10, IEnumerable<ReceiptNote> valueM11, decimal totalMonthAmount11, IEnumerable<ReceiptNote> valueM12,
            decimal totalMonthAmount12)
        {
            if (valueM1 == null)
            {
                totalMonthAmount1 = 0;
            }
            else
            {
                foreach (var item in valueM1)
                {
                    totalMonthAmount1 += item.TotalAmount;
                }
            }
            if (valueM2 == null)
            {
                totalMonthAmount2 = 0;
            }
            else
            {
                foreach (var item in valueM2)
                {
                    totalMonthAmount2 += item.TotalAmount;
                }
            }
            if (valueM3 == null)
            {
                totalMonthAmount3 = 0;
            }
            else
            {
                foreach (var item in valueM3)
                {
                    totalMonthAmount3 += item.TotalAmount;
                }
            }
            if (valueM4 == null)
            {
                totalMonthAmount4 = 0;
            }
            else
            {
                foreach (var item in valueM4)
                {
                    totalMonthAmount4 += item.TotalAmount;
                }
            }
            if (valueM5 == null)
            {
                totalMonthAmount5 = 0;
            }
            else
            {
                foreach (var item in valueM5)
                {
                    totalMonthAmount5 += item.TotalAmount;
                }
            }
            if (valueM6 == null)
            {
                totalMonthAmount6 = 0;
            }
            else
            {
                foreach (var item in valueM6)
                {
                    totalMonthAmount6 += item.TotalAmount;
                }
            }
            if (valueM7 == null)
            {
                totalMonthAmount7 = 0;
            }
            else
            {
                foreach (var item in valueM7)
                {
                    totalMonthAmount7 += item.TotalAmount;
                }
            }
            if (valueM8 == null)
            {
                totalMonthAmount8 = 0;
            }
            else
            {
                foreach (var item in valueM8)
                {
                    totalMonthAmount8 += item.TotalAmount;
                }
            }
            if (valueM9 == null)
            {
                totalMonthAmount9 = 0;
            }
            else
            {
                foreach (var item in valueM9)
                {
                    totalMonthAmount9 += item.TotalAmount;
                }
            }
            if (valueM10 == null)
            {
                totalMonthAmount10 = 0;
            }
            else
            {
                foreach (var item in valueM10)
                {
                    totalMonthAmount10 += item.TotalAmount;
                }
            }
            if (valueM11 == null)
            {
                totalMonthAmount11 = 0;
            }
            else
            {
                foreach (var item in valueM11)
                {
                    totalMonthAmount11 += item.TotalAmount;
                }
            }
            if (valueM12 == null)
            {
                totalMonthAmount12 = 0;
            }
            else
            {
                foreach (var item in valueM12)
                {
                    totalMonthAmount12 += item.TotalAmount;
                }
            }
            ValueExpense.Add(totalMonthAmount1);
            ValueExpense.Add(totalMonthAmount2);
            ValueExpense.Add(totalMonthAmount3);
            ValueExpense.Add(totalMonthAmount4);
            ValueExpense.Add(totalMonthAmount5);
            ValueExpense.Add(totalMonthAmount6);
            ValueExpense.Add(totalMonthAmount7);
            ValueExpense.Add(totalMonthAmount8);
            ValueExpense.Add(totalMonthAmount9);
            ValueExpense.Add(totalMonthAmount10);
            ValueExpense.Add(totalMonthAmount11);
            ValueExpense.Add(totalMonthAmount12);
        }
        private static void FindValueInMonthOrderNote(ChartValues<decimal> ValueRevenue, IEnumerable<OrderNote> valueM1, decimal totalMonthAmount1,
            IEnumerable<OrderNote> valueM2, decimal totalMonthAmount2, IEnumerable<OrderNote> valueM3, decimal totalMonthAmount3, IEnumerable<OrderNote> valueM4,
            decimal totalMonthAmount4, IEnumerable<OrderNote> valueM5, decimal totalMonthAmount5, IEnumerable<OrderNote> valueM6,
            decimal totalMonthAmount6, IEnumerable<OrderNote> valueM7, decimal totalMonthAmount7, IEnumerable<OrderNote> valueM8,
            decimal totalMonthAmount8, IEnumerable<OrderNote> valueM9, decimal totalMonthAmount9, IEnumerable<OrderNote> valueM10,
            decimal totalMonthAmount10, IEnumerable<OrderNote> valueM11, decimal totalMonthAmount11, IEnumerable<OrderNote> valueM12,
            decimal totalMonthAmount12)
        {
            if (valueM1 == null)
            {
                totalMonthAmount1 = 0;
            }
            else
            {
                foreach (var item in valueM1)
                {
                    totalMonthAmount1 += item.TotalPrice;
                }
            }
            if (valueM2 == null)
            {
                totalMonthAmount2 = 0;
            }
            else
            {
                foreach (var item in valueM2)
                {
                    totalMonthAmount2 += item.TotalPrice;
                }
            }
            if (valueM3 == null)
            {
                totalMonthAmount3 = 0;
            }
            else
            {
                foreach (var item in valueM3)
                {
                    totalMonthAmount3 += item.TotalPrice;
                }
            }
            if (valueM4 == null)
            {
                totalMonthAmount4 = 0;
            }
            else
            {
                foreach (var item in valueM4)
                {
                    totalMonthAmount4 += item.TotalPrice;
                }
            }
            if (valueM5 == null)
            {
                totalMonthAmount5 = 0;
            }
            else
            {
                foreach (var item in valueM5)
                {
                    totalMonthAmount5 += item.TotalPrice;
                }
            }
            if (valueM6 == null)
            {
                totalMonthAmount6 = 0;
            }
            else
            {
                foreach (var item in valueM6)
                {
                    totalMonthAmount6 += item.TotalPrice;
                }
            }
            if (valueM7 == null)
            {
                totalMonthAmount7 = 0;
            }
            else
            {
                foreach (var item in valueM7)
                {
                    totalMonthAmount7 += item.TotalPrice;
                }
            }
            if (valueM8 == null)
            {
                totalMonthAmount8 = 0;
            }
            else
            {
                foreach (var item in valueM8)
                {
                    totalMonthAmount8 += item.TotalPrice;
                }
            }
            if (valueM9 == null)
            {
                totalMonthAmount9 = 0;
            }
            else
            {
                foreach (var item in valueM9)
                {
                    totalMonthAmount9 += item.TotalPrice;
                }
            }
            if (valueM10 == null)
            {
                totalMonthAmount10 = 0;
            }
            else
            {
                foreach (var item in valueM10)
                {
                    totalMonthAmount10 += item.TotalPrice;
                }
            }
            if (valueM11 == null)
            {
                totalMonthAmount11 = 0;
            }
            else
            {
                foreach (var item in valueM11)
                {
                    totalMonthAmount11 += item.TotalPrice;
                }
            }
            if (valueM12 == null)
            {
                totalMonthAmount12 = 0;
            }
            else
            {
                foreach (var item in valueM12)
                {
                    totalMonthAmount12 += item.TotalPrice;
                }
            }
            ValueRevenue.Add(totalMonthAmount1);
            ValueRevenue.Add(totalMonthAmount2);
            ValueRevenue.Add(totalMonthAmount3);
            ValueRevenue.Add(totalMonthAmount4);
            ValueRevenue.Add(totalMonthAmount5);
            ValueRevenue.Add(totalMonthAmount6);
            ValueRevenue.Add(totalMonthAmount7);
            ValueRevenue.Add(totalMonthAmount8);
            ValueRevenue.Add(totalMonthAmount9);
            ValueRevenue.Add(totalMonthAmount10);
            ValueRevenue.Add(totalMonthAmount11);
            ValueRevenue.Add(totalMonthAmount12);
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
