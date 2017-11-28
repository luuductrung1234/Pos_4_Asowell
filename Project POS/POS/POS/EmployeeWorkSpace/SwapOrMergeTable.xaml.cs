using POS.BusinessModel;
using POS.Entities;
using POS.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SwapOrMergeTable.xaml
    /// </summary>
    public partial class SwapOrMergeTable : Window
    {
        EmployeewsOfAsowell _unitofwork;
        List<Entities.Table> _currentTableList;
        List<Entities.Chair> _currentChairList;
        List<OrderTemp> _orderTempList;
        List<OrderDetailsTemp> _orderDetailsTempList;
        Entities.Table first;
        Entities.Table second;

        public SwapOrMergeTable(EmployeewsOfAsowell unitofwork, List<Entities.Table> currentTableList)
        {
            _unitofwork = unitofwork;
            _currentTableList = currentTableList;
            _currentChairList = _unitofwork.ChairRepository.Get().ToList();
            _orderTempList = _unitofwork.OrderTempRepository.Get().ToList();
            _orderDetailsTempList = _unitofwork.OrderDetailsTempRepository.Get().ToList();
            InitializeComponent();

            initTableData();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initTableData()
        {
            foreach (var t in _currentTableList)
            {
                ToggleButton button = new ToggleButton();
                button.Name = "id" + t.TableId.ToString();
                button.Content = (t.TableNumber).ToString();
                button.Width = 40;
                button.Height = 40;
                Thickness m = button.Margin;
                m.Left = 5;
                m.Top = 5;
                button.Margin = m;
                button.SetValue(StyleProperty, FindResource("MaterialDesignActionToggleButton"));
                button.Checked += buttonChair_Checked;
                button.Unchecked += buttonChair_Unchecked;

                if (t.IsPinned == 1)
                {
                    button.Background = Brushes.Red;
                }
                if (t.IsOrdered == 1)
                {
                    button.Background = Brushes.DarkCyan;
                }

                wpTableContainer.Children.Add(button);
            }
        }

        private void buttonChair_Checked(object sender, RoutedEventArgs e)
        {
            int i = 0;
            ToggleButton cur = sender as ToggleButton;
            int id = int.Parse(cur.Name.Substring(2));
            foreach (ToggleButton chTable in wpTableContainer.Children)
            {
                if (chTable.IsChecked == true)
                {
                    i++;
                }
            }

            if (i == 1)
            {
                first = _unitofwork.TableRepository.Get(x => x.TableId.Equals(id)).First();
            }
            else if (i == 2)
            {
                if (second == null)
                {
                    second = _unitofwork.TableRepository.Get(x => x.TableId.Equals(id)).First();
                }
                else
                {
                    first = _unitofwork.TableRepository.Get(x => x.TableId.Equals(id)).First();
                }
            }
            else if (i > 2)
            {
                cur.Checked -= buttonChair_Checked;
                cur.Unchecked -= buttonChair_Unchecked;
                cur.IsChecked = false;
                cur.Checked += buttonChair_Checked;
                cur.Unchecked += buttonChair_Unchecked;
                return;
            }
        }

        private void buttonChair_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton cur = sender as ToggleButton;
            int id = int.Parse(cur.Name.Substring(2));

            if (first == null || second == null)
            {
                return;
            }

            if (first.TableId.Equals(id))
            {
                first = null;
            }

            if (second.TableId.Equals(id))
            {
                second = null;
            }
        }

        private void btnSwap_Click(object sender, RoutedEventArgs e)
        {
            if (first == null || second == null)
            {
                MessageBox.Show("You must be choose two table to swap!");
                return;
            }

            //var chairOfFirst = _currentChairList.Where(x => x.TableOwned.Equals(first.TableId)).ToList();
            //var chairOfSecond = _currentChairList.Where(x => x.TableOwned.Equals(second.TableId)).ToList();

            //foreach (var f in chairOfFirst)
            //{
            //    f.TableOwned = second.TableId;
            //}

            //foreach(var s in chairOfSecond)
            //{
            //    s.TableOwned = first.TableId;
            //}

            var orderOfFirst = _orderTempList.Where(x => x.TableOwned.Equals(first.TableId)).First();
            var orderOfSecond = _orderTempList.Where(x => x.TableOwned.Equals(second.TableId)).First();

            var orderDetailsOfFirst = _orderDetailsTempList.Where(x => x.OrdertempId.Equals(orderOfFirst.OrdertempId)).ToList();
            var orderDetailsOfSecond = _orderDetailsTempList.Where(x => x.OrdertempId.Equals(orderOfSecond.OrdertempId)).ToList();

            //if(orderOfFirst.Count > second.ChairAmount)
            //{
            //    MessageBoxResult mess = MessageBox.Show("Table " + second.TableNumber + " have no enough chair to swap with table " + first.TableNumber +
            //        "! Do you want to set its chair automatically?",
            //        "Warning!", MessageBoxButton.YesNo);
            //    if(mess == MessageBoxResult.Yes)
            //    {
            //        foreach()
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //if(orderOfSecond.Count > first.ChairAmount)
            //{

            //}

            orderOfFirst.TableOwned = second.TableId;
            orderOfFirst.TableOwned = first.TableId;
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
