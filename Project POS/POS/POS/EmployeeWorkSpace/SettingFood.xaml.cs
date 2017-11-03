using POS.Model;
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

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SettingFood.xaml
    /// </summary>
    public partial class SettingFood : Page
    {
        public SettingFood()
        {
            InitializeComponent();
            lvData.ItemsSource = ProductData.PList;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
