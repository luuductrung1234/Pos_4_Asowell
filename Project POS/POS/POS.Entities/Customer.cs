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

    // Customer
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class Customer
    {
        public string CusId { get; set; } // cus_id (Primary key) (length: 10)
        public string Name { get; set; } // name (length: 50)
        public string Phone { get; set; } // phone (length: 20)
        public string Email { get; set; } // email (length: 100)
        public int Discount { get; set; } // discount
        public int Deleted { get; set; } // deleted

        // Reverse navigation

        /// <summary>
        /// Child OrderNotes where [OrderNote].[cus_id] point to this entity (fk_customerowner)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OrderNote> OrderNotes { get; set; } // OrderNote.fk_customerowner

        public Customer()
        {
            OrderNotes = new System.Collections.Generic.List<OrderNote>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
