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

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ProductDetailPage.xaml
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        private EmployeewsOfAsowell _unitofwork;
        public ProductDetailPage()
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            InitializeComponent();
            lvData.ItemsSource = _unitofwork.ProductRepository.Get();
            for (int i = 0; i <= 100; i++)
            {
                cbopromotion.Items.Add(i.ToString());
            }
        }
    }
}
