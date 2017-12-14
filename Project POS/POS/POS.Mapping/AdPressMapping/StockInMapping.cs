using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class StockInMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StockIn>
    {
        public StockInMapping()
            : this("dbo")
        {
        }

        public StockInMapping(string schema)
        {
            ToTable("StockIn", schema);
            HasKey(x => x.Si_id);

            Property(x => x.Si_id).HasColumnName(@"si_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.AdId).HasColumnName(@"ad_id").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.InTime).HasColumnName(@"intime").HasColumnType("datetime").IsRequired();
            Property(x => x.TotalAmount).HasColumnName(@"total_amount").HasColumnType("money").IsRequired().HasPrecision(19, 4);
            
            // Foreign keys
            HasOptional(a => a.AdminRe).WithMany(b => b.StockIns).HasForeignKey(c => c.AdId).WillCascadeOnDelete(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }
}
