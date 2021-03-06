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
using System.Collections.ObjectModel;
using System.ComponentModel;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace POS.Entities
{

    // OrderNoteDetails
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class OrderNoteDetail :  INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        
        private string _ordernoteId { get; set; } // ordernote_id (Primary key) (length: 10)
        private string _productId { get; set; } // product_id (Primary key) (length: 10)
        private int _quan { get; set; } // quan
        

        public string OrdernoteId
        {
            get
            {
                return _ordernoteId;
            }
            set
            {
                _ordernoteId = value;
                OnPropertyChanged("Ordernote_id");
            }
        }
        public string ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                _productId = value;
                OnPropertyChanged("Product_id");
            }
        }
        
        public int Quan
        {
            get
            {
                return _quan;
            }
            set
            {
                _quan = value;
                OnPropertyChanged("Quan");
                //System.Windows.MessageBox.Show("New Quan setting");
            }
        }


        // Foreign keys

        /// <summary>
        /// Parent OrderNote pointed by [OrderNoteDetails].([OrdernoteId]) (FK_dbo.OrderNoteDetails_dbo.OrderNote_ordernote_id)
        /// </summary>
        public virtual OrderNote OrderNote { get; set; } // FK_dbo.OrderNoteDetails_dbo.OrderNote_ordernote_id

        /// <summary>
        /// Parent Product pointed by [OrderNoteDetails].([ProductId]) (FK_dbo.OrderNoteDetails_dbo.Product_product_id)
        /// </summary>
        public virtual Product Product { get; set; } // FK_dbo.OrderNoteDetails_dbo.Product_product_id

        public OrderNoteDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
// </auto-generated>
