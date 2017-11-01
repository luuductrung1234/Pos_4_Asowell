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

    // ProductDetails
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class ProductDetailMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProductDetail>
    {
        public ProductDetailMapping()
            : this("dbo")
        {
        }

        public ProductDetailMapping(string schema)
        {
            ToTable("ProductDetails", schema);
            HasKey(x => x.PdetailId);

            Property(x => x.PdetailId).HasColumnName(@"pdetail_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ProductId).HasColumnName(@"product_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.IgdId).HasColumnName(@"igd_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Quan).HasColumnName(@"quan").HasColumnType("float").IsRequired();
            Property(x => x.UnitUse).HasColumnName(@"unit_use").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.Ingredient).WithMany(b => b.ProductDetails).HasForeignKey(c => c.IgdId).WillCascadeOnDelete(false); // fk_ingredientid
            HasRequired(a => a.Product).WithMany(b => b.ProductDetails).HasForeignKey(c => c.ProductId).WillCascadeOnDelete(false); // fk_productid
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
