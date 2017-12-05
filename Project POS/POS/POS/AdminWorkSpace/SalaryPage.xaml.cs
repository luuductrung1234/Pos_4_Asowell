using POS.Entities;
using POS.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.Helper.PrintHelper.Report;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for SalaryPage.xaml
    /// </summary>
    public partial class SalaryPage : Page
    {
        AdminwsOfCloudPOS _unitofwork;
        IEnumerable<SalaryNote> SalList;
        IEnumerable<WorkingHistory> WhList;
        private AdminRe admin;

        public SalaryPage(AdminwsOfCloudPOS unitofwork, AdminRe curAdmin)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            admin = curAdmin;

            Loaded += SalaryPage_Loaded;
        }

        private void SalaryPage_Loaded(object sender, RoutedEventArgs args)
        {
            SalList = _unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories").Where(x => x.Employee.Manager.Equals(admin.AdId));
            WhList = _unitofwork.WorkingHistoryRepository.Get(includeProperties: "Employee").Where(x => x.Employee.Manager.Equals(admin.AdId));
            lvSalary.ItemsSource = SalList;
            lvWokingHistory.ItemsSource = WhList;
            initMonthYear();
        }

        private void initMonthYear()
        {
            int checkYear = 0;
            cboYear.Items.Clear();
            int year = DateTime.Now.Year;
            cboYear.Items.Add("--");
            cboYear.Items.Add(year);

            var SalList = _unitofwork.SalaryNoteRepository.Get();
            foreach (var s in SalList)
            {
                if (s.ForYear != year)
                {
                    if (checkYear != s.ForYear)
                    {
                        cboYear.Items.Add(s.ForYear);
                        checkYear = s.ForYear;
                    }
                }
            }

            cboMonth.Items.Clear();
            cboMonth.Items.Add("--");
            for (int i = 0; i < 12; i++)
            {
                cboMonth.Items.Add(i + 1);
            }

            cboYear.SelectedItem = "--";
            cboMonth.SelectedItem = "--";
            cboYear.SelectionChanged += cboYear_SelectionChanged;
            cboMonth.SelectionChanged += cboMonth_SelectionChanged;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SalaryNote sln = lvSalary.SelectedItem as SalaryNote;
            if (sln == null)
                return;
            lvWokingHistory.ItemsSource = WhList.Where(c => c.ResultSalary.Equals(sln.SnId));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => Regex.IsMatch(x.Employee.Name, filter, RegexOptions.IgnoreCase)).Where(x => x.Employee.Manager.Equals(admin.AdId));
                lvSalary.ItemsSource = SalList;
            }
            else
            {
                SalList = _unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories").Where(x => x.Employee.Manager.Equals(admin.AdId));
                lvSalary.ItemsSource = SalList;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if(filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter)).Where(x => x.Employee.Manager.Equals(admin.AdId));
                lvSalary.ItemsSource = SalList;
            }
            else
            {
                SalList = _unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories").Where(x => x.Employee.Manager.Equals(admin.AdId));
                lvSalary.ItemsSource = SalList;
            }
        }

        private void cboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();
            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter));
            }

            ComboBox cboM = sender as ComboBox;

            if (cboM.Items.Count == 0 || cboYear.Items.Count == 0 || cboM.SelectedItem == null || cboYear.SelectedItem == null)
            {
                return;
            }

            int month = 0;
            int year = 0;

            try
            {
                month = (int)(cboM.SelectedItem);
                year = (int)(cboYear.SelectedItem);
            }
            catch (Exception ex)
            {
                if (cboM.SelectedItem.Equals("--"))
                {
                    if (cboYear.SelectedItem.Equals("--"))
                    {
                        lvSalary.ItemsSource = SalList;
                        return;
                    }

                    lvSalary.ItemsSource = SalList.Where(x => x.ForYear.Equals((int)cboYear.SelectedItem));
                    return;
                }

                if (cboYear.SelectedItem.Equals("--"))
                {
                    lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboM.SelectedItem));
                    return;
                }
            }

            lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboM.SelectedItem) && x.ForYear.Equals((int)cboYear.SelectedItem));
        }

        private void cboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();
            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter));
            }

            ComboBox cboY = sender as ComboBox;

            if (cboY.Items.Count == 0 || cboMonth.Items.Count == 0 || cboY.SelectedItem == null || cboMonth.SelectedItem == null)
            {
                return;
            }

            int month = 0;
            int year = 0;

            try
            {
                year = (int)(cboY.SelectedItem);
                month = (int)(cboMonth.SelectedItem);
            }
            catch (Exception ex)
            {
                if (cboY.SelectedItem.Equals("--"))
                {
                    if (cboMonth.SelectedItem.Equals("--"))
                    {
                        lvSalary.ItemsSource = SalList;
                        return;
                    }

                    lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboMonth.SelectedItem));
                    return;
                }

                if (cboMonth.SelectedItem.Equals("--"))
                {
                    lvSalary.ItemsSource = SalList.Where(x => x.ForYear.Equals((int)cboY.SelectedItem));
                    return;
                }
            }

            lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboMonth.SelectedItem) && x.ForYear.Equals((int)cboY.SelectedItem));
        }

        //public string ConvertFrom(string str)
        //{
        //    byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
        //    str = Encoding.UTF8.GetString(utf8Bytes);
        //    return str;
        //}

        private string DecodeFromUtf8(string utf8String)
        {
            // read the string as UTF-8 bytes.
            byte[] encodedBytes = Encoding.UTF8.GetBytes(utf8String);

            // convert them into unicode bytes.
            byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);

            // builds the converted string.
            return Encoding.Unicode.GetString(encodedBytes);
        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportSalaryOptionDialog(new SalaryNoteReport(), _unitofwork);
            optionDialog.Show();
        }
    }
}
