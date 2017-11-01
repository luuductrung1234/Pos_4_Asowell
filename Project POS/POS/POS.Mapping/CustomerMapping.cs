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

    // Customer
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class CustomerMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
            : this("dbo")
        {
        }

        public CustomerMapping(string schema)
        {
            ToTable("Customer", schema);
            HasKey(x => x.CusId);

            Property(x => x.CusId).HasColumnName(@"cus_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            Property(x => x.Phone).HasColumnName(@"phone").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(20);
            Property(x => x.Email).HasColumnName(@"email").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Discount).HasColumnName(@"discount").HasColumnType("int").IsRequired();
            Property(x => x.Deleted).HasColumnName(@"deleted").HasColumnType("int").IsRequired();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
