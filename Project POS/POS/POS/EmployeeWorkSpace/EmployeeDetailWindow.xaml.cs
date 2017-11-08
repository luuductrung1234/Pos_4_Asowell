using System;
using System.Windows;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.DAL;
using POS.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            lsWH.ItemsSource = _unitofwork.WorkingHistoryRepository.Get(w => w.EmpId.Equals(em.EmpId) && w.StartTime.Month.Equals(DateTime.Now.Month) && w.StartTime.Year.Equals(DateTime.Now.Year));
        }
    }
}
