using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class StockMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Stock>
    {
        public StockMapping()
            : this("dbo")
        {
        }

        public StockMapping(string schema)
        {
            ToTable("Stock", schema);
            HasKey(x => x.StoId);

            Property(x => x.StoId).HasColumnName(@"sto_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.APWarehouseId).HasColumnName(@"apwarehouse_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Name).HasColumnName(@"name").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(x => x.Group).HasColumnName(@"group").HasColumnType("int").IsRequired();
            Property(x => x.BarterCode).HasColumnName(@"barter_code").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            Property(x => x.BarterName).HasColumnName(@"barter_name").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            Property(x => x.UnitIn).HasColumnName(@"unit_in").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(x => x.UnitOut).HasColumnName(@"unit_out").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(x => x.StandardPrice).HasColumnName(@"standard_price").HasColumnType("money").IsRequired().HasPrecision(19, 4);
            Property(x => x.Info).HasColumnName(@"info").HasColumnType("nvarchar").IsOptional().HasMaxLength(500);
            Property(x => x.Supplier).HasColumnName(@"supplier").HasColumnType("nvarchar").IsOptional().HasMaxLength(500);
            Property(x => x.Deleted).HasColumnName(@"deleted").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.APWareHouse).WithMany(b => b.Stocks).HasForeignKey(c => c.APWarehouseId).WillCascadeOnDelete(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }
}
