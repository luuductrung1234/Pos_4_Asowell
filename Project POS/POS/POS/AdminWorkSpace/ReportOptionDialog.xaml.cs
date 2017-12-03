using System;
using System.Windows;
using System.Windows.Controls;
using POS.Helper.PrintHelper.Report;
using POS.Repository.DAL;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ReportOptionDialog.xaml
    /// </summary>
    public partial class ReportOptionDialog : Window
    {
        private DateTime startTime;
        private DateTime endTime;
        private IListPdfReport _reportHelper;
        private AdminwsOfCloudPOS _unitofwork;
        private static string folderPath = AppPath.ApplicationPath + "\\SerializedData";

        public ReportOptionDialog(IListPdfReport reportHelper, AdminwsOfCloudPOS unitofwork)
        {
            InitializeComponent();

            _unitofwork = unitofwork;
            _reportHelper = reportHelper;

            DpFrom.SelectedDate = DateTime.Now;
            DpTo.SelectedDate = DateTime.Now;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_reportHelper != null && DpFrom.SelectedDate.Value != null && DpTo.SelectedDate.Value != null)
                {
                    // generate report
                    if (ChbOverviewReport.IsChecked == true)
                    {
                        _reportHelper.CreatePdfReport(_unitofwork, DpFrom.SelectedDate.Value.Date,
                            DpTo.SelectedDate.Value.Date, folderPath);
                    }
                    else if (ChbDetailsReport.IsChecked == true)
                    {
                        _reportHelper.CreateDetailsPdfReport(_unitofwork, DpFrom.SelectedDate.Value.Date,
                            DpTo.SelectedDate.Value.Date, folderPath);
                    }
                    else
                    {
                        _reportHelper.CreateEntityPdfReport(_unitofwork, DpFrom.SelectedDate.Value.Date,
                            DpTo.SelectedDate.Value.Date, folderPath);
                    }

                    MessageBox.Show("new report was generated, please check your folder (path):\n\n" + folderPath);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the duration of time that you want to create Report!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Generate new report fail! Something went wrong.");
            }
        }

        private void BtnFastChoiceMonthRpt_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_reportHelper != null && DpFrom.SelectedDate.Value != null && DpTo.SelectedDate.Value != null)
                {
                    // generate report
                    if (ChbOverviewReport.IsChecked == true)
                    {
                        _reportHelper.CreateMonthPdfReport(_unitofwork, folderPath);
                    }

                    MessageBox.Show("new report was generated, please check your folder (path):\n\n" + folderPath);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the duration of time that you want to create Report!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Generate new report fail! Something went wrong.");
            }
        }

        private void BtnFastChoiceDayRpt_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_reportHelper != null && DpFrom.SelectedDate.Value != null && DpTo.SelectedDate.Value != null)
                {
                    // generate report
                    if (ChbOverviewReport.IsChecked == true)
                    {
                        _reportHelper.CreateDayPdfReport(_unitofwork, folderPath);
                    }

                    MessageBox.Show("new report was generated, please check your folder (path):\n\n" + folderPath);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the duration of time that you want to create Report!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Generate new report fail! Something went wrong.");
            }
        }

        private void BtnFastChoiceYearRpt_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_reportHelper != null && DpFrom.SelectedDate.Value != null && DpTo.SelectedDate.Value != null)
                {
                    // generate report
                    if (ChbOverviewReport.IsChecked == true)
                    {
                        _reportHelper.CreateYearPdfReport(_unitofwork, folderPath);
                    }

                    MessageBox.Show("new report was generated, please check your folder (path):\n\n" + folderPath);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the duration of time that you want to create Report!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Generate new report fail! Something went wrong.");
            }
        }



        /// <summary>
        /// Select Directory to store Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath = dialog.SelectedPath;
                }
            }
        }
    }
}
