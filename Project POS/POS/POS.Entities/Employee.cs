// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.6

using System;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace POS.Entities
{

    // Employee
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    [Serializable]
    public partial class Employee
    {
        public string EmpId { get; set; } // emp_id (Primary key) (length: 10)
        public string Manager { get; set; } // manager (length: 10)
        public string Username { get; set; } // username (length: 50)
        public string Pass { get; set; } // pass (length: 50)
        public string Name { get; set; } // name (length: 50)
        public System.DateTime Birth { get; set; } // birth
        public System.DateTime Startday { get; set; } // startday
        public int HourWage { get; set; } // hour_wage
        public string Addr { get; set; } // addr (length: 200)
        public string Email { get; set; } // email (length: 100)
        public string Phone { get; set; } // phone (length: 20)
        public int EmpRole { get; set; } // emp_role
        public int Deleted { get; set; } // deleted

        // Reverse navigation

        /// <summary>
        /// Child OrderNotes where [OrderNote].[emp_id] point to this entity (fk_employeerespond)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OrderNote> OrderNotes { get; set; } // OrderNote.fk_employeerespond
        /// <summary>
        /// Child ReceiptNotes where [ReceiptNote].[emp_id] point to this entity (fk_employee_buy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReceiptNote> ReceiptNotes { get; set; } // ReceiptNote.fk_employee_buy
        /// <summary>
        /// Child SalaryNotes where [SalaryNote].[emp_id] point to this entity (fk_employeeid)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SalaryNote> SalaryNotes { get; set; } // SalaryNote.fk_employeeid
        /// <summary>
        /// Child WorkingHistories where [WorkingHistory].[emp_id] point to this entity (fk_employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkingHistory> WorkingHistories { get; set; } // WorkingHistory.fk_employee

        // Foreign keys

        /// <summary>
        /// Parent AdminRe pointed by [Employee].([Manager]) (fk_manager)
        /// </summary>
        public virtual AdminRe AdminRe { get; set; } // fk_manager

        public Employee()
        {
            OrderNotes = new System.Collections.Generic.List<OrderNote>();
            ReceiptNotes = new System.Collections.Generic.List<ReceiptNote>();
            SalaryNotes = new System.Collections.Generic.List<SalaryNote>();
            WorkingHistories = new System.Collections.Generic.List<WorkingHistory>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
