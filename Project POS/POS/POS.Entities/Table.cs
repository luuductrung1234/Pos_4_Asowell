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


using System.ComponentModel.DataAnnotations;
using System.Windows.Shapes;

namespace POS.Entities
{

    // Table
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class Table
    {
        [Key]
        public int TableId { get; set; } // table_id (Primary key) (length: 10)
        public int TableNumber { get; set; } // table_number
        public int ChairAmount { get; set; } // chair_amount
        public int PosX { get; set; } // pos_X
        public int PosY { get; set; } // pos_Y
        public int IsPinned { get; set; } // is_Pinned
        public int IsOrdered { get; set; } // is_Ordered
        public int IsLocked { get; set; } // is_Locked
        public Rectangle TableRec { get; set; }

        // Reverse navigation

        /// <summary>
        /// Child Chairs where [Chair].[table_owned] point to this entity (fk_table_owned)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Chair> Chairs { get; set; } // Chair.fk_table_owned
        /// <summary>
        /// Child OrderTemps where [OrderTemp].[table_owned] point to this entity (fk_table_owned_order)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OrderTemp> OrderTemps { get; set; } // OrderTemp.fk_table_owned_order

        public Table()
        {
            Chairs = new System.Collections.Generic.List<Chair>();
            OrderTemps = new System.Collections.Generic.List<OrderTemp>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>