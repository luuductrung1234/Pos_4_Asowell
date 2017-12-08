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
        EmployeewsOfLocalPOS _unitofwork;
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

        int _type = 0;

        public SwapOrMergeTable(EmployeewsOfLocalPOS unitofwork, List<Entities.Table> currentTableList, int type) //1: swap, 2: merge
        {
            _unitofwork = unitofwork;
            _currentTableList = currentTableList;
            _currentChairList = _unitofwork.ChairRepository.Get().ToList();
            _orderTempList = _unitofwork.OrderTempRepository.Get().ToList();
            _orderDetailsTempList = _unitofwork.OrderDetailsTempRepository.Get().ToList();
            _type = type;
            InitializeComponent();

            initTableData();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initTableData()
        {
            if(_type == 1)
            {
                btnSwap.Visibility = Visibility.Visible;
                btnMerge.Visibility = Visibility.Collapsed;
                btnCancel.Visibility = Visibility.Visible;
            }
            else if(_type == 2)
            {
                btnSwap.Visibility = Visibility.Collapsed;
                btnMerge.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Visible;
            }

            foreach (var t in _currentTableList)
            {
                if (t.IsPinned == 0)
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

            foreach (var ch in chairOfFirst)
            {
                ch.TableOwned = second.TableId;
                _unitofwork.ChairRepository.Update(ch);
            }

            foreach (var ch in chairOfSecond)
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

            loadData();

            ConfirmMergeDialog cmd = new ConfirmMergeDialog(_unitofwork, first, second);
            cmd.ShowDialog();

            if (App.Current.Properties["TableMerged"] == null)
            {
                this.Close();
                return;
            }

            var merged = App.Current.Properties["TableMerged"] as Entities.Table;
            if(merged.ChairAmount < (first.ChairAmount + second.ChairAmount))
            {
                //MessageBoxResult Mess = MessageBox.Show("Table " + merged.TableNumber + " didn't have enough chair to merge! Do you want to add chair automatically?", "Warning!", MessageBoxButton.YesNo);
                //if(Mess == MessageBoxResult.Yes)
                //{
                //    for(int i = merged.ChairAmount; i < (first.ChairAmount + second.ChairAmount); i++)
                //    {
                //        Entities.Chair newch = new Entities.Chair();
                //        newch.ChairNumber = i++;
                //        newch.TableOwned = merged.TableId;
                //        _unitofwork.ChairRepository.Insert(newch);
                //        _unitofwork.Save();
                //    }
                //}
                //else
                //{
                //    this.Close();
                //    return;
                //}
            }

            if (merged.TableId == first.TableId)
            {
                orderOfFirst.CusId = App.Current.Properties["TableOwner"] as string;
                orderOfFirst.TableOwned = first.TableId;
                _unitofwork.OrderTempRepository.Update(orderOfFirst);

                checkEmpId(1);

                loadData();

                checkChair(1);

                _unitofwork.Save();
            }
            else
            {
                orderOfSecond.CusId = App.Current.Properties["TableOwner"] as string;
                orderOfSecond.TableOwned = second.TableId;
                _unitofwork.OrderTempRepository.Update(orderOfSecond);

                checkEmpId(2);

                loadData();

                checkChair(2);

                _unitofwork.Save();
            }
        }

        private void checkEmpId(int tab)
        {
            if (tab == 1)
            {
                if (orderOfFirst.SubEmpId != null)
                {
                    string[] subemplistfirst = orderOfFirst.SubEmpId.Split(',');
                    if (orderOfSecond.SubEmpId != null)
                    {
                        string[] subemplistsecond = orderOfSecond.SubEmpId.Split(',');

                        orderOfFirst.SubEmpId += orderOfSecond.SubEmpId;

                        if (!orderOfSecond.EmpId.Equals(orderOfFirst.EmpId))
                        {
                            orderOfFirst.SubEmpId += orderOfSecond.EmpId;
                        }
                    }
                }
                else
                {
                    if (orderOfSecond.SubEmpId != null)
                    {
                        orderOfFirst.SubEmpId = orderOfSecond.SubEmpId;

                        if (!orderOfSecond.EmpId.Equals(orderOfFirst.EmpId))
                        {
                            orderOfFirst.SubEmpId += orderOfSecond.EmpId;
                        }
                    }
                }

                setToOriData(tab);
            }

            if (tab == 2)
            {
                if (orderOfSecond.SubEmpId != null)
                {
                    string[] subemplistsecond = orderOfSecond.SubEmpId.Split(',');
                    if (orderOfFirst.SubEmpId != null)
                    {
                        string[] subemplistfirst = orderOfFirst.SubEmpId.Split(',');

                        orderOfSecond.SubEmpId += orderOfFirst.SubEmpId;

                        if (!orderOfFirst.EmpId.Equals(orderOfSecond.EmpId))
                        {
                            orderOfSecond.SubEmpId += orderOfFirst.EmpId;
                        }
                    }
                }
                else
                {
                    if (orderOfFirst.SubEmpId != null)
                    {
                        orderOfSecond.SubEmpId = orderOfFirst.SubEmpId;

                        if (!orderOfFirst.EmpId.Equals(orderOfSecond.EmpId))
                        {
                            orderOfSecond.SubEmpId += orderOfFirst.EmpId;
                        }
                    }
                }
                
                setToOriData(tab);
            }
        }

        private void checkChair(int tab)
        {
            var chairOfFirst = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(first.TableId)).ToList();
            var chairOfSecond = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(second.TableId)).ToList();

            if (tab == 1)
            {
                int count = 0;
                foreach(var ch in chairOfFirst)
                {
                    var chair = orderDetailsOfFirst.Where(x => x.ChairId.Equals(ch.ChairId)).ToList();
                    if(chair == null)
                    {
                        continue;
                    }

                    count++;
                }

                foreach(var ch in chairOfSecond)
                {
                    Entities.Chair newch = new Entities.Chair();
                    newch.ChairNumber = ch.ChairNumber;
                    newch.TableOwned = second.TableId;
                    _unitofwork.ChairRepository.Insert(newch);

                    var chair = orderDetailsOfSecond.Where(x => x.ChairId.Equals(ch.ChairId)).ToList();
                    if (chair == null)
                    {
                        continue;
                    }

                    foreach(var chod in chair)
                    {
                        _unitofwork.OrderDetailsTempRepository.Delete(chod);
                        _unitofwork.Save();
                        chod.OrdertempId = orderOfFirst.OrdertempId;
                        _unitofwork.OrderDetailsTempRepository.Insert(chod);
                    }

                    _unitofwork.Save();

                    ch.ChairNumber = ++count;
                    ch.TableOwned = first.TableId;
                    _unitofwork.ChairRepository.Update(ch);
                }
            }

            if (tab == 2)
            {
                int count = 0;
                foreach (var ch in chairOfSecond)
                {
                    var chair = orderDetailsOfSecond.Where(x => x.ChairId.Equals(ch.ChairId)).ToList();
                    if (chair == null)
                    {
                        continue;
                    }

                    count++;
                }

                foreach (var ch in chairOfFirst)
                {
                    Entities.Chair newch = new Entities.Chair();
                    newch.ChairNumber = ch.ChairNumber;
                    newch.TableOwned = second.TableId;
                    _unitofwork.ChairRepository.Insert(newch);

                    var chair = orderDetailsOfSecond.Where(x => x.ChairId.Equals(ch.ChairId)).ToList();
                    if (chair == null)
                    {
                        continue;
                    }

                    foreach (var chod in chair)
                    {
                        _unitofwork.OrderDetailsTempRepository.Delete(chod);
                        chod.OrdertempId = orderOfSecond.OrdertempId;
                        _unitofwork.OrderDetailsTempRepository.Insert(chod);
                    }

                    _unitofwork.Save();

                    ch.ChairNumber = count++;
                    ch.TableOwned = second.TableId;
                    _unitofwork.ChairRepository.Update(ch);
                }
            }
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

        private void setToOriData(int tab)
        {
            if (tab == 1)
            {
                second.IsOrdered = 0;
                second.IsPrinted = 0;

                orderOfSecond.CusId = "CUS0000001";
                orderOfSecond.Discount = 0;
                orderOfSecond.TableOwned = second.TableId;
                orderOfSecond.Ordertime = DateTime.Now;
                orderOfSecond.TotalPriceNonDisc = 0;
                orderOfSecond.TotalPrice = 0;
                orderOfSecond.CustomerPay = 0;
                orderOfSecond.PayBack = 0;
                orderOfSecond.SubEmpId = "";

                _unitofwork.TableRepository.Update(second);
                _unitofwork.OrderTempRepository.Update(orderOfFirst);
                _unitofwork.OrderTempRepository.Update(orderOfSecond);
            }

            if (tab == 2)
            {
                first.IsOrdered = 0;
                first.IsPrinted = 0;

                orderOfFirst.CusId = "CUS0000001";
                orderOfFirst.Discount = 0;
                orderOfFirst.TableOwned = first.TableId;
                orderOfFirst.Ordertime = DateTime.Now;
                orderOfFirst.TotalPriceNonDisc = 0;
                orderOfFirst.TotalPrice = 0;
                orderOfFirst.CustomerPay = 0;
                orderOfFirst.PayBack = 0;
                orderOfFirst.SubEmpId = "";

                _unitofwork.TableRepository.Update(first);
                _unitofwork.OrderTempRepository.Update(orderOfFirst);
                _unitofwork.OrderTempRepository.Update(orderOfSecond);
            }
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

    //for (int i = 0; i < subemplistfirst.Count(); i++)
    //{
    //    for (int j = 0; j < subemplistsecond.Count(); j++)
    //    {
    //        if (subemplistsecond[j].Equals(""))
    //        {
    //            continue;
    //        }

    //        if(subemplistfirst[i].Equals(subemplistsecond[j]))
    //        {
    //            continue;
    //        }
    //        else
    //        {

    //        }
    //    }
    //}

    //ordertempcurrenttable.SubEmpId += currentEmp.Emp.EmpId + ",";
    //_unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
    //_unitofwork.Save();
    //return;
}
