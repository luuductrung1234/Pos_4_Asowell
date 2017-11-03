using System.Windows.Controls;
using POS.Entities;

namespace POS.EmployeeWorkSpace
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
