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
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace POS.Entities
{

    // WorkingHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class WorkingHistory
    {
        public string WhId { get; set; } // wh_id (Primary key) (length: 10)
        public string ResultSalary { get; set; } // result_salary (length: 10)
        public string EmpId { get; set; } // emp_id (length: 10)
        public System.DateTime? Workday { get; set; } // workday
        public int Starthour { get; set; } // starthour
        public int Startminute { get; set; } // startminute
        public int Endhour { get; set; } // endhour
        public int Endminute { get; set; } // endminute

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [WorkingHistory].([EmpId]) (fk_employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // fk_employee

        /// <summary>
        /// Parent SalaryNote pointed by [WorkingHistory].([ResultSalary]) (fk_salarynote)
        /// </summary>
        public virtual SalaryNote SalaryNote { get; set; } // fk_salarynote

        public WorkingHistory()
        {
            Workday = System.DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
