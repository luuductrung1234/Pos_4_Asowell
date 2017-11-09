using System;
using System.Windows;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.DAL;
using POS.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        private EmployeewsOfAsowell _unitofwork;
        Employee em;

        public EmployeeDetail(string UserName, EmployeewsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            loadData(UserName);
            initlsWH();
            em = new Employee();
        }

        private void loadData(string UserName)
        {
            foreach (var item in  _unitofwork.EmployeeRepository.Get())
            {
                if (item.Username.Equals(UserName))
                {
                    em = item;
                    break;
                    
                }
            }
            this.EmployeeInfo.DataContext = em;
        }

        private void initlsWH()
        {
            var whListAll = _unitofwork.WorkingHistoryRepository.Get(w => w.EmpId.Equals(em.EmpId) && w.StartTime.Month.Equals(DateTime.Now.Month) && w.StartTime.Year.Equals(DateTime.Now.Year)).ToList();
            foreach(var i in whListAll)
            {
                ShowWH newWH = new ShowWH();
                //progress
                newWH.WorkTime = (i.EndTime - i.StartTime).Hours + ":" + (i.EndTime - i.StartTime).Minutes + ":" + (i.EndTime - i.StartTime).Seconds;
                newWH.WorkDate = i.StartTime;

                ShowWHData.showWHList.Add(newWH);
            }

            lsWH.ItemsSource = ShowWHData.showWHList;
        }
    }

    public class ShowWH
    {
        public ProgressBar TimePercent { get; set; }
        public string WorkTime { get; set; }
        public DateTime WorkDate { get; set; }
    }

    public class ShowWHData
    {
        public static List<ShowWH> showWHList = new List<ShowWH>();
    }
}
