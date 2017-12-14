using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class StockOutMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StockOut>
    {
        public StockOutMapping()
            : this("dbo")
        {
        }

        public StockOutMapping(string schema)
        {
            ToTable("StockOut", schema);
            HasKey(x => x.StockOutId);

            Property(x => x.StockOutId).HasColumnName(@"stockout_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Adid).HasColumnName(@"ad_id").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Outtime).HasColumnName(@"outTime").HasColumnType("date").IsRequired();
            Property(x => x.TotalAmount).HasColumnName(@"total_amount").HasColumnType("money").IsRequired().HasPrecision(19, 4);
            Property(x => x.Vat).HasColumnName(@"Vat").HasColumnType("money").HasPrecision(19, 4);
            Property(x => x.Discount).HasColumnName(@"discount").HasColumnType("int").IsRequired();


            // Foreign keys
            HasOptional(a => a.AdminRe).WithMany(b => b.StockOuts).HasForeignKey(c => c.Adid).WillCascadeOnDelete(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }
}
