using POS.Entities;
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
        EmployeewsOfLocalPOS _unitofwork;
        Entities.Table _first;
        Entities.Table _second;
        
        OrderTemp orderOfFirst;
        OrderTemp orderOfSecond;

        public ConfirmMergeDialog(EmployeewsOfLocalPOS unitofwork, Entities.Table first, Entities.Table second)
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

            setControl(true);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["TableMerged"] = _first;
            checkCus();
        }

        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["TableMerged"] = _second;
            checkCus();
        }

        private void btnFirstCus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSecondCus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["TableMerged"] = null;
            this.Close();
        }

        private void setControl(bool b)
        {
            btnFirst.Visibility = Visibility.Visible;
            btnSecond.Visibility = Visibility.Visible;
            btnFirstCus.Visibility = Visibility.Visible;
            btnSecondCus.Visibility = Visibility.Visible;

            if (b)
            {
                btnFirstCus.Visibility = Visibility.Collapsed;
                btnSecondCus.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnFirst.Visibility = Visibility.Collapsed;
                btnSecond.Visibility = Visibility.Collapsed;
            }
            
            btnCancel.Visibility = Visibility.Visible;
        }

        private void checkCus()
        {
            orderOfFirst = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(_first.TableId)).First();
            orderOfSecond = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(_second.TableId)).First();

            if (orderOfFirst.CusId.Equals("CUS0000001") && orderOfSecond.CusId.Equals("CUS0000001"))
            {
                App.Current.Properties["TableOwner"] = "CUS0000001";
                this.Close();
            }
            else if (orderOfFirst.CusId.Equals("CUS0000001") && !orderOfSecond.CusId.Equals("CUS0000001"))
            {
                App.Current.Properties["TableOwner"] = orderOfSecond.CusId;
                this.Close();
            }
            else if (!orderOfFirst.CusId.Equals("CUS0000001") && orderOfSecond.CusId.Equals("CUS0000001"))
            {
                App.Current.Properties["TableOwner"] = orderOfFirst.CusId;
                this.Close();
            }
            else
            {
                txtMessage.Text = "Who will become owner of table after merged? ";
                setControl(false);
                btnFirstCus.Name = orderOfFirst.CusId;
                btnSecondCus.Name = orderOfSecond.CusId;
            }
        }

    }
}
