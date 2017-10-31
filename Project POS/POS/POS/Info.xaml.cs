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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Page
    {
        string formatDate = "yyyy-MM-dd";
        Employee emp = App.Current.Properties["EmpLogin"] as Employee;

        public Info()
        {
            InitializeComponent();

          //  initEmployeeInfo();
        }

        //private void initEmployeeInfo()
        //{
        //    txtUsername.Text = emp.Username;
        //    txtPass.Password = emp.Pass.ToString(); ;
        //    txtName.Text = emp.Name;
        //    txtBirth.Text = emp.Birth.ToString(formatDate);
        //    txtAddr.Text = emp.Addr;
        //    txtEmail.Text = emp.Email;
        //    txtPhone.Text = emp.Phone;
        //    txtStartDay.Text = emp.Startday.ToString(formatDate);
        //    txtHourWage.Text = emp.Hour_wage.ToString();
        //}
    }
}
