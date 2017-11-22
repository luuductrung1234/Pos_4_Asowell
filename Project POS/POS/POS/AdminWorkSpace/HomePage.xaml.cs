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
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using POS.Repository.DAL;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        AdminwsOfAsowell _unitofwork;
        public Func<ChartPoint, string> PointLabel { get; set; }
        public Dictionary<string, decimal> PriceList;
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollectionTime { get; set; }
        public HomePage(AdminwsOfAsowell unitofwork)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            InitializeComponent();
            ChartDataFilling();
            ChartDataFillingByTime();
        }

        private void ChartDataFillingByTime()
        {
            decimal TotalPrice1 = 0;
            decimal TotalPrice2 = 0;
            decimal TotalPrice3 = 0;
            foreach (var item in _unitofwork.OrderRepository.Get(c => c.Ordertime.Hour >= 0 && c.Ordertime.Hour < 12))
            {
                TotalPrice1 += item.TotalPrice;
            }
            foreach (var item in _unitofwork.OrderRepository.Get(c => c.Ordertime.Hour >= 12 && c.Ordertime.Hour < 18))
            {
                TotalPrice2 += item.TotalPrice;
            }
            foreach (var item in _unitofwork.OrderRepository.Get(c =>
                c.Ordertime.Hour >= 18 && (c.Ordertime.Hour <= 23 && c.Ordertime.Minute <= 59)))
            {
                TotalPrice3 += item.TotalPrice;
            }
            SeriesCollectionTime = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "0h-12h",
                    Values = new ChartValues<ObservableValue> {new ObservableValue((double) TotalPrice1)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "12h-18h",
                    Values = new ChartValues<ObservableValue> {new ObservableValue((double) TotalPrice2)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "18h-0h",
                    Values = new ChartValues<ObservableValue> {new ObservableValue((double) TotalPrice2)},
                    DataLabels = true
                }
            };
        }

        private void ChartDataFilling()
        {
            decimal count = 0;
            PriceList = new Dictionary<string, decimal>();
            foreach (var item in _unitofwork.EmployeeRepository.Get().ToList())
            {
                foreach (var item2 in _unitofwork.OrderRepository.Get(c => c.EmpId.Equals(item.EmpId)))
                {
                    count += item2.TotalPrice;
                }
                PriceList.Add(item.Name, count);
                count = 0;
            }
            SeriesCollection = new SeriesCollection();
            foreach (var item in PriceList)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<ObservableValue> {new ObservableValue((double) item.Value)},
                    DataLabels = true
                });
            }
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        private void RdToday_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RdWeek_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RdMonth_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
