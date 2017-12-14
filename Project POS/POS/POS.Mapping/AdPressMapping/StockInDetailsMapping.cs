using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class StockInDetailsMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StockInDetails>
    {
        public StockInDetailsMapping()
            : this("dbo")
        {
        }

        public StockInDetailsMapping(string schema)
        {
            ToTable("StockInDetails", schema);
            HasKey(x => new { x.Si_id, x.Sto_id });

            Property(x => x.Si_id).HasColumnName(@"si_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Sto_id).HasColumnName(@"sto_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Quan).HasColumnName(@"quan").HasColumnType("float").IsRequired();
            Property(x => x.ItemPrice).HasColumnName(@"item_price").HasColumnType("money").IsRequired().HasPrecision(19, 4);
            Property(x => x.Note).HasColumnName(@"note").HasColumnType("nvarchar").IsOptional().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys
            HasRequired(a => a.Stock).WithMany(b => b.StockInDetails).HasForeignKey(c => c.Sto_id).WillCascadeOnDelete(false); 
            HasRequired(a => a.StockIn).WithMany(b => b.StockInDetails).HasForeignKey(c => c.Si_id).WillCascadeOnDelete(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }
}
