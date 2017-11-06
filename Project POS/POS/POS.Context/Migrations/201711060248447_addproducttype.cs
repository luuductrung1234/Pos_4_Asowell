namespace POS.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproducttype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "type", c => c.Int(nullable: false));
            DropColumn("dbo.Product", "is_todrink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "is_todrink", c => c.Byte(nullable: false));
            DropColumn("dbo.Product", "type");
        }
    }
}
