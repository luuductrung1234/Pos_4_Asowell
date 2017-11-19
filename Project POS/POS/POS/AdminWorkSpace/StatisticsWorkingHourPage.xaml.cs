using LiveCharts;
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
using LiveCharts.Wpf;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for StatisticsWorkingHourPage.xaml
    /// </summary>
 
    public partial class StatisticsWorkingHourPage : Page
    {
        
        public SeriesCollection SeriesCollection { get; set; }
        private AdminwsOfAsowell _unitofwork;
        public Func<int, string> Formatter { get; set; }
        public List<string> Labels { get; set; }
        public StatisticsWorkingHourPage(AdminwsOfAsowell unitofwork)
        {
            _unitofwork =unitofwork ;
            InitializeComponent();
            var EmpList = _unitofwork.EmployeeRepository.Get();
            ChartValues<int> Values = new ChartValues<int>();
            Labels = new List<string>();
            foreach (var item in EmpList)
            {
                Values.Add(item.HourWage);
                Labels.Add(item.Name);

            }
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Working Hour",
                    Values = Values
                }
            };
            Formatter = value => value.ToString();
            DataContext = this;

        }
    }
}
