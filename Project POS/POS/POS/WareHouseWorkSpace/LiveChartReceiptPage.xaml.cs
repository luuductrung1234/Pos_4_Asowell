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

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for LiveChartReceiptPage.xaml
    /// </summary>
    public partial class LiveChartReceiptPage : Page
    {
        public LiveChartReceiptPage()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "revenue",
                    Values = new ChartValues<decimal> { 10, 50, 39, 50,100,200,300,400,400, 390,150,230 }
                    
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "expense",
                Values = new ChartValues<decimal> { 11, 56, 42,50,60,70,80,150,200,300,300,100}
            });
            Formatter = value => value.ToString();
            InitializeComponent();
            DataContext = this;
            Labels = new[] {"Jan", "Feb", "Mar", "Apr", "May ", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"};
        }
        public SeriesCollection SeriesCollection { get; set; }
        public Func<decimal, string> Formatter { get; set; }
        public string[] Labels { get; set; }


        private void BntChangelvc_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
