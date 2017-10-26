using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Employee
    {
        public string Emp_id { get; set; }
        public string Manager { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Startday { get; set; }
        public int Hour_wage { get; set; }
        public string Addr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Emp_role { get; set; }
        public bool Deleted { get; set; }
    }

    public class EmployeeData
    {
        public static List<Employee> EmpList
        {
            get
            {
                return new List<Employee>()
                {
                    new Employee { Emp_id = "EMP0000001", Manager = "AD00000001", Username="usernameemployee1",
                                    Pass = "password1", Name="Tran Minh Trong", Birth = new DateTime(1997, 10, 1),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 10, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee1email@gmail.com", Phone = "0901111111", Emp_role=1, Deleted = false},
                    new Employee { Emp_id = "EMP0000002", Manager = "AD00000001", Username="usernameemployee2",
                                    Pass = "password2", Name="Nguyen Hoang Uyen", Birth = new DateTime(1997, 11, 20),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 10, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee2email@gmail.com", Phone = "0902222222", Emp_role=1, Deleted = false},
                    new Employee { Emp_id = "EMP0000003", Manager = "AD00000001", Username="usernameemployee3",
                                    Pass = "password3", Name="Le Thi Thao Huyen", Birth = new DateTime(1996, 9, 1),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 12, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee3email@gmail.com", Phone = "0903333333", Emp_role=1, Deleted = false},
                    new Employee { Emp_id = "EMP0000004", Manager = "AD00000001", Username="usernameemployee4",
                                    Pass = "password4", Name="Tran Thao Trang", Birth = new DateTime(1989, 2, 1),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 11, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee4email@gmail.com", Phone = "0904444444", Emp_role=2, Deleted = false},
                    new Employee { Emp_id = "EMP0000005", Manager = "AD00000002", Username="usernameemployee5",
                                    Pass = "password5", Name="Mai Tien Dung", Birth = new DateTime(1997, 10, 1),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 11, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee5email@gmail.com", Phone = "0905555555", Emp_role=2, Deleted = false},
                    new Employee { Emp_id = "EMP0000006", Manager = "AD00000001", Username="usernameemployee6",
                                    Pass = "password6", Name="Le Huu Phat", Birth = new DateTime(1997, 12, 1),
                                    Startday = new DateTime(2017, 10, 1), Hour_wage = 10, Addr="11/16 Nguyen Van Quy, q7",
                                    Email="employee6email@gmail.com", Phone = "0906666666", Emp_role=1, Deleted = false},
                };
            }
        }
    }
}
