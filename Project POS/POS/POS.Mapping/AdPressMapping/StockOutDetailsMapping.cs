using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class StockOutDetailsMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StockOutDetails>
    {
        public StockOutDetailsMapping()
            : this("dbo")
        {
        }

        public StockOutDetailsMapping(string schema)
        {
            ToTable("StockOutDetails", schema);
            HasKey(x => new { x.StockoutId, x.StockId });

            Property(x => x.StockoutId).HasColumnName(@"stockout_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.StockId).HasColumnName(@"stock_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Quan).HasColumnName(@"quan").HasColumnType("int").IsRequired();
            Property(x => x.Discount).HasColumnName(@"discount").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.StockOut).WithMany(b => b.StockOutDetails).HasForeignKey(c => c.StockoutId).WillCascadeOnDelete(false); 
            HasRequired(a => a.Stock).WithMany(b => b.StockOutDetails).HasForeignKey(c => c.StockId).WillCascadeOnDelete(false); 
            InitializePartial();
        }
        partial void InitializePartial();
    }
}
