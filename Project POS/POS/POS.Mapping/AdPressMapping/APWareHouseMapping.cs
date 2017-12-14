using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Mapping.AdPressMapping
{
    public partial class APWareHouseMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<APWareHouse>
    {
        public APWareHouseMapping()
            : this("dbo")
        {
        }

        public APWareHouseMapping(string schema)
        {
            ToTable("APWareHouse", schema);
            HasKey(x => x.APWarehouseId);

            Property(x => x.APWarehouseId).HasColumnName(@"apwarehouse_id").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"name").HasColumnType("nvarchar").HasMaxLength(500).IsRequired();
            Property(x => x.Contain).HasColumnName(@"contain").HasColumnType("float").IsOptional();
            Property(x => x.StandardContain).HasColumnName(@"std_contain").HasColumnType("float").IsRequired();

            InitializePartial();
        }
        partial void InitializePartial();
    }
}
