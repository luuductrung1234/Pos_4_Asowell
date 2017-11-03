using System;
using System.Windows;
using System.Collections.Generic;
using POS.BusinessModel;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for TableSettingDialog.xaml
    /// </summary>
    public partial class TableSettingDialog : Window
    {
        BusinessModel.Table curTable = new BusinessModel.Table();

        public TableSettingDialog(BusinessModel.Table table)
        {
            InitializeComponent();

            curTable = table;

            this.Loaded += TableSettingDialog_Loaded;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void TableSettingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Convert.ToInt32(curTable.Position.X);
            this.Top = Convert.ToInt32(curTable.Position.Y);
        }

        private void txtChairAmount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }

        List<Chair> chList = new List<Chair>();
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach(BusinessModel.Table t in TableTempData.TbList)
            {
                if(t.TableNumber == curTable.TableNumber)
                {
                    foreach(Chair ch in t.ChairData)
                    {
                        if(ch.ChairOrderDetails.Count != 0)
                        {
                            chList.Add(ch);
                        }
                    }

                    if(chList.Count > int.Parse(txtChairAmount.Text.Trim()))
                    {
                        MessageBox.Show("Can not change Chair Amount now! This table have " + chList.Count + " chair(s) on order!");
                        return;
                    }

                    ReadWriteData.writeOnUpdateChair(t, chList, int.Parse(txtChairAmount.Text.Trim()));
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
