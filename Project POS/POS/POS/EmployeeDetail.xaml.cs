using POS.Model;
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

namespace POS
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        public EmployeeDetail(string UserName)
        {
            InitializeComponent();
            loadData(UserName);
        }
        private void loadData(string UserName)
        {
            Employee em = new Employee();
            foreach (var item in EmployeeData.EmpList)
            {
                if (item.Username.Equals(UserName))
                {
                    em.Name = item.Name;
                    em.Phone = item.Phone;
                    em.Email = item.Email;
                    em.Birth = item.Birth;
                    em.Addr = item.Addr;
                    em.Startday = item.Startday;
                    em.Hour_wage = item.Hour_wage;
                    em.Username = item.Username;
                    
                }
            }
            this.EmployeeInfo.DataContext = em;
        }
    }
}
