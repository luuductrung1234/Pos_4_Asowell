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


namespace POS.Mapping
{
    using POS.Entities;

    // SalaryNote
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class SalaryNoteMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<SalaryNote>
    {
        public SalaryNoteMapping()
            : this("dbo")
        {
        }

        public SalaryNoteMapping(string schema)
        {
            ToTable("SalaryNote", schema);
            HasKey(x => x.SnId);

            Property(x => x.SnId).HasColumnName(@"sn_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.EmpId).HasColumnName(@"emp_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.DatePay).HasColumnName(@"date_pay").HasColumnType("date").IsOptional();
            Property(x => x.SalaryValue).HasColumnName(@"salary_value").HasColumnType("money").IsRequired().HasPrecision(19,4);
            Property(x => x.WorkHour).HasColumnName(@"work_hour").HasColumnType("float").IsRequired();
            Property(x => x.ForMonth).HasColumnName(@"for_month").HasColumnType("int").IsRequired();
            Property(x => x.ForYear).HasColumnName(@"for_year").HasColumnType("int").IsRequired();
            Property(x => x.IsPaid).HasColumnName(@"is_paid").HasColumnType("tinyint").IsRequired();

            // Foreign keys
            HasRequired(a => a.Employee).WithMany(b => b.SalaryNotes).HasForeignKey(c => c.EmpId).WillCascadeOnDelete(false); // fk_employeeid
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
