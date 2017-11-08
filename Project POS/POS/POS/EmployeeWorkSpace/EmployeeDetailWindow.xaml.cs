using System;
using System.Windows;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.DAL;
using POS.Repository.Interfaces;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        private EmployeewsOfAsowell _unitofwork;
        
        public EmployeeDetail(string UserName, EmployeewsOfAsowell unitofwork)
        {
            _unitofwork = unitofwork;
            InitializeComponent();
            loadData(UserName);
        }

        private void loadData(string UserName)
        {
            Employee em = new Employee();
            foreach (var item in  _unitofwork.EmployeeRepository.Get())
            {
                if (item.Username.Equals(UserName))
                {
                    em.Name = item.Name;
                    em.Phone = item.Phone;
                    em.Email = item.Email;
                    em.Birth = item.Birth;
                    em.Addr = item.Addr;
                    em.Startday = item.Startday;
                    em.HourWage = item.HourWage;
                    em.Username = item.Username;
                    break;
                    
                }
            }
            this.EmployeeInfo.DataContext = em;
        }
    }
}
