using System;
using System.Windows;
using System.Collections.Generic;
using POS.BusinessModel;
using POS.Repository.DAL;
using System.Linq;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for TableSettingDialog.xaml
    /// </summary>
    public partial class TableSettingDialog : Window
    {
        EmployeewsOfLocalPOS _uniofwork;
        Entities.Table curTable = new Entities.Table();

        public TableSettingDialog(EmployeewsOfLocalPOS unitofwork, Entities.Table table)
        {
            InitializeComponent();

            _uniofwork = unitofwork;
            curTable = table;

            this.Loaded += TableSettingDialog_Loaded;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void TableSettingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Convert.ToInt32(curTable.PosX);
            this.Top = Convert.ToInt32(curTable.PosY);
        }

        private void txtChairAmount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int amountChange = int.Parse(txtChairAmount.Text.Trim());
            if(amountChange == 0)
            {
                MessageBox.Show("Chair Amount must be greater than 0!");
                return;
            }

            if(curTable.ChairAmount == 0)
            {
                curTable.ChairAmount = amountChange;

                for (int i = 0; i < amountChange; i++)
                {
                    Entities.Chair newChair = new Entities.Chair();
                    newChair.ChairNumber = i + 1;
                    newChair.TableOwned = curTable.TableId;

                    _uniofwork.ChairRepository.Insert(newChair);
                    _uniofwork.Save();
                }

                this.Close();

                return;
            }

            var chairOfTable = _uniofwork.ChairRepository.Get(x => x.TableOwned.Equals(curTable.TableId)).ToList();

            int orderdChairCount = 0;
            foreach (var chair in chairOfTable)
            {
                var orderDetailsOfChair = _uniofwork.OrderDetailsTempRepository.Get(x => x.ChairId.Equals(chair.ChairId));
                if (orderDetailsOfChair != null && orderDetailsOfChair.Count() > 0)
                {
                    orderdChairCount++;
                }
            }

            if (orderdChairCount > amountChange)
            {
                MessageBox.Show("Can not change Chair Amount now! This table have " + orderdChairCount + " chair(s) on order!");
                return;
            }

            if (chairOfTable.Count() < amountChange)
            {
                // increase
                for (int i = chairOfTable.Count(); i < amountChange; i++)
                {
                    Entities.Chair newChair = new Entities.Chair();
                    newChair.ChairNumber = i + 1;
                    newChair.TableOwned = curTable.TableId;

                    _uniofwork.ChairRepository.Insert(newChair);
                    _uniofwork.Save();
                }

                curTable.ChairAmount = amountChange;
            }
            else
            {
                // decrease
                for (int i = amountChange; i < chairOfTable.Count(); i++)
                {
                    _uniofwork.ChairRepository.Delete(chairOfTable.ElementAt(i));
                    _uniofwork.Save();
                }

                curTable.ChairAmount = amountChange;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
