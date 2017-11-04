using System;
using System.Windows;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.Interfaces;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        private IEmployeeRepository _employeeRepository;


        public EmployeeDetail(string UserName)
        {
            _employeeRepository = new EmployeeRepository(new AsowellContext());
            InitializeComponent();
            loadData(UserName);

            this.Closing += Closing_EmployeeDetailWindow;
        }

        public EmployeeDetail(string UserName, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            InitializeComponent();
            loadData(UserName);
        }

        private void loadData(string UserName)
        {
            Employee em = new Employee();
            foreach (var item in _employeeRepository.GetAllEmployees())
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
                    
                }
            }
            this.EmployeeInfo.DataContext = em;
        }

        private void Closing_EmployeeDetailWindow(object sender, EventArgs args)
        {
            _employeeRepository.Dispose();
        }
    }
}
