using POS.Entities;
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

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeWorkingHistoryDialog.xaml
    /// </summary>
    public partial class EmployeeWorkingHistoryDialog : Window
    {
        public EmployeeWorkingHistoryDialog(WorkingHistory wh)
        {
            InitializeComponent();
            
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            txbToday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txbStartHour.Text = wh.Starthour + ":" + wh.Startminute;
            txbWorkingHour.Text = (DateTime.Now.Hour - wh.Starthour) + (DateTime.Now.Minute - wh.Startminute)/60 + " hour";
        }
    }
}
