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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for statisticsFoodPage.xaml
    /// </summary>
    public partial class statisticsFoodPage : Page
    {
        AdminwsOfAsowell _unitofwork;
        public SeriesCollection SeriesCollection { get; set; }
        public Dictionary<string, int> CountList;
        public Func<decimal, string> Formatter { get; set; }
        public List<string> Labels { get; set; }
        public statisticsFoodPage(AdminwsOfAsowell unitofwork)
        {
            CountList=new Dictionary<string, int>();
            InitializeComponent();
            //var OrderList = unitofwork.OrderNoteDetailsRepository.Get();
            var ProductList = unitofwork.ProductRepository.Get();
           // var td = from o in OrderList join pr in ProductList on o.ProductId equals pr.ProductId select o;
            int count = 0;
            foreach (var item in ProductList)
            {
                foreach (var item2 in unitofwork.OrderNoteDetailsRepository.Get(c=>c.ProductId.Equals(item.ProductId)))
                {
                    count += item2.Quan;
                    
                }
                CountList.Add(item.Name, count);
                count = 0;
            }
            ChartValues<int> Values = new ChartValues<int>();
            Labels = new List<string>();
            foreach (var item in CountList)
            {
                Values.Add(item.Value);
                Labels.Add(item.Key);
            }
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "amount",
                    Values = Values
                }
            };
            Formatter = value => value.ToString();
            DataContext = this;
           
        }
    }
}
