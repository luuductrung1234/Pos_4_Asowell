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

    // ReceiptNoteDetails
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class StockInDetails
    {
        public string Si_id { get; set; } // si_id (Primary key) (length: 10)
        public string Sto_id { get; set; } // sto_id (Primary key) (length: 10)
        public double Quan { get; set; } // quan
        public decimal ItemPrice { get; set; } // item_price
        public string Note { get; set; } // note (length: 500)

        // Foreign keys
        
        public virtual Stock Stock { get; set; }
        
        public virtual StockIn StockIn { get; set; }

        public StockInDetails()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
