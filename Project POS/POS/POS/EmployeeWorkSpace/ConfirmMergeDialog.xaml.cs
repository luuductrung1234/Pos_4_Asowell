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
using System.Windows.Shapes;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for ConfirmMergeDialog.xaml
    /// </summary>
    public partial class ConfirmMergeDialog : Window
    {
        EmployeewsOfAsowell _unitofwork;
        Entities.Table _first;
        Entities.Table _second;

        public ConfirmMergeDialog(EmployeewsOfAsowell unitofwork, Entities.Table first, Entities.Table second)
        {
            _unitofwork = unitofwork;
            _first = first;
            _second = second;
            InitializeComponent();

            initTableData();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initTableData()
        {
            txtMessage.Text = "Please choose a table want to merge. Table " + _first.TableNumber + " or " + _second.TableNumber + "?";

            btnFirst.Name = "id" + _first.TableId;
            btnSecond.Name = "id" + _second.TableId;
            btnFirst.Content = _first.TableNumber;
            btnSecond.Content = _second.TableNumber;
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Name);
        }

        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Name);
        }
    }
}
