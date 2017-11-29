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

        OrderTemp orderOfFirst;
        List<OrderDetailsTemp> orderDetailsOfFirst;
        List<List<OrderDetailsTemp>> chairOrderDetailsOfFirst;
        OrderTemp orderOfSecond;
        List<OrderDetailsTemp> orderDetailsOfSecond;
        List<List<OrderDetailsTemp>> chairOrderDetailsOfSecond;

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
                if(t.IsPinned == 0)
                {
                    continue;
                }

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

            loadData();

            //swap ordertemp
            orderOfFirst.TableOwned = second.TableId;
            orderOfSecond.TableOwned = first.TableId;

            _unitofwork.OrderTempRepository.Update(orderOfFirst);
            _unitofwork.OrderTempRepository.Update(orderOfSecond);
            _unitofwork.Save();

            _currentChairList = _unitofwork.ChairRepository.Get().ToList();
            _orderTempList = _unitofwork.OrderTempRepository.Get().ToList();
            _orderDetailsTempList = _unitofwork.OrderDetailsTempRepository.Get().ToList();

            loadData();

            var chairOfFirst = _currentChairList.Where(x => x.TableOwned.Equals(first.TableId)).ToList();
            var chairOfSecond = _currentChairList.Where(x => x.TableOwned.Equals(second.TableId)).ToList();

            first.ChairAmount = chairOfSecond.Count;
            second.ChairAmount = chairOfFirst.Count;

            _unitofwork.TableRepository.Update(first);
            _unitofwork.TableRepository.Update(second);

            foreach(var ch in chairOfFirst)
            {
                ch.TableOwned = second.TableId;
                _unitofwork.ChairRepository.Update(ch);
            }

            foreach(var ch in chairOfSecond)
            {
                ch.TableOwned = first.TableId;
                _unitofwork.ChairRepository.Update(ch);
            }

            //temporary first table to swap
            Entities.Table tempFirst = new Entities.Table
            {
                TableId = first.TableId,
                TableNumber = first.TableNumber,
                ChairAmount = first.ChairAmount,
                PosX = first.PosX,
                PosY = first.PosY,
                IsPinned = first.IsPinned,
                IsOrdered = first.IsOrdered,
                IsLocked = first.IsLocked,
                IsPrinted = first.IsPrinted,
                TableRec = first.TableRec,
            };

            //temporary second table to swap
            Entities.Table tempSecond = new Entities.Table
            {
                TableId = second.TableId,
                TableNumber = second.TableNumber,
                ChairAmount = second.ChairAmount,
                PosX = second.PosX,
                PosY = second.PosY,
                IsPinned = second.IsPinned,
                IsOrdered = second.IsOrdered,
                IsLocked = second.IsLocked,
                IsPrinted = second.IsPrinted,
                TableRec = second.TableRec,                
            };

            //setting new first table
            first.IsOrdered = tempSecond.IsOrdered;
            first.IsLocked = tempSecond.IsLocked;
            first.IsPrinted = tempSecond.IsPrinted;

            //setting new second table
            second.IsOrdered = tempFirst.IsOrdered;
            second.IsLocked = tempFirst.IsLocked;
            second.IsPrinted = tempFirst.IsPrinted;

            _unitofwork.TableRepository.Update(first);
            _unitofwork.TableRepository.Update(second);
            _unitofwork.Save();

            MessageBox.Show("Swapped Successful!");
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            if (first == null || second == null)
            {
                MessageBox.Show("You must be choose two table to swap!");
                return;
            }

            MessageBox.Show("Please choose a table want to merge. Table " + first.TableNumber + " or " + second.TableNumber + "?");

            loadData();

            OrderTemp mergeOrder = new OrderTemp();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loadData()
        {
            orderOfFirst = _orderTempList.Where(x => x.TableOwned.Equals(first.TableId)).First();
            orderOfSecond = _orderTempList.Where(x => x.TableOwned.Equals(second.TableId)).First();

            orderDetailsOfFirst = _orderDetailsTempList.Where(x => x.OrdertempId.Equals(orderOfFirst.OrdertempId)).ToList();
            orderDetailsOfSecond = _orderDetailsTempList.Where(x => x.OrdertempId.Equals(orderOfSecond.OrdertempId)).ToList();

            chairOrderDetailsOfFirst = orderDetailsOfFirst.GroupBy(x => x.ChairId).Select(a => a.ToList()).ToList();
            chairOrderDetailsOfSecond = orderDetailsOfSecond.GroupBy(x => x.ChairId).Select(x => x.ToList()).ToList();
        }
    }

    //public class CustomMessageBox : System.Windows.Forms.Form
    //{
    //    Label message = new Label();
    //    Button b1 = new Button();
    //    Button b2 = new Button();

    //    public CustomMessageBox()
    //    {

    //    }

    //    public CustomMessageBox(string title, string body, string button1, string button2)
    //    {
    //        this.ClientSize = new System.Drawing.Size(490, 150);
    //        this.Text = title;

    //        b1.Location = new System.Drawing.Point(411, 112);
    //        b1.Size = new System.Drawing.Size(75, 23);
    //        b1.Text = button1;
    //        b1.BackColor = Control.DefaultBackColor;

    //        b2.Location = new System.Drawing.Point(311, 112);
    //        b2.Size = new System.Drawing.Size(75, 23);
    //        b2.Text = button2;
    //        b2.BackColor = Control.DefaultBackColor;

    //        message.Location = new System.Drawing.Point(10, 10);
    //        message.Text = body;
    //        message.Font = Control.DefaultFont;
    //        message.AutoSize = true;

    //        this.BackColor = Color.White;
    //        this.ShowIcon = false;

    //        this.Controls.Add(b1);
    //        this.Controls.Add(b2);
    //        this.Controls.Add(message);
    //    }
    //}
}
