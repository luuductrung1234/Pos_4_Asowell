namespace POS.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminRes",
                c => new
                    {
                        ad_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        username = c.String(nullable: false, maxLength: 50, unicode: false),
                        pass = c.String(nullable: false, maxLength: 50, unicode: false),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ad_id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        emp_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        manager = c.String(nullable: false, maxLength: 10, unicode: false),
                        username = c.String(nullable: false, maxLength: 50, unicode: false),
                        pass = c.String(nullable: false, maxLength: 50, unicode: false),
                        name = c.String(nullable: false, maxLength: 50),
                        birth = c.DateTime(nullable: false, storeType: "date"),
                        startday = c.DateTime(nullable: false, storeType: "date"),
                        hour_wage = c.Int(nullable: false),
                        addr = c.String(maxLength: 200),
                        email = c.String(maxLength: 100, unicode: false),
                        phone = c.String(maxLength: 20, unicode: false),
                        emp_role = c.Int(nullable: false),
                        deleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.emp_id)
                .ForeignKey("dbo.AdminRes", t => t.manager)
                .Index(t => t.manager);
            
            CreateTable(
                "dbo.OrderNote",
                c => new
                    {
                        ordernote_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        cus_id = c.String(maxLength: 10, unicode: false),
                        emp_id = c.String(maxLength: 10, unicode: false),
                        ordertable = c.Int(nullable: false),
                        ordertime = c.DateTime(nullable: false, storeType: "date"),
                        total_price = c.Decimal(nullable: false, storeType: "money"),
                        customer_pay = c.Decimal(nullable: false, storeType: "money"),
                        pay_back = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.ordernote_id)
                .ForeignKey("dbo.Customer", t => t.cus_id)
                .ForeignKey("dbo.Employee", t => t.emp_id)
                .Index(t => t.cus_id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        cus_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 50),
                        phone = c.String(maxLength: 20, unicode: false),
                        email = c.String(maxLength: 100, unicode: false),
                        discount = c.Int(nullable: false),
                        deleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cus_id);
            
            CreateTable(
                "dbo.OrderNoteDetails",
                c => new
                    {
                        ordernote_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        product_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        SelectedStats = c.String(),
                        quan = c.Int(nullable: false),
                        note = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => new { t.ordernote_id, t.product_id })
                .ForeignKey("dbo.OrderNote", t => t.ordernote_id)
                .ForeignKey("dbo.Product", t => t.product_id)
                .Index(t => t.ordernote_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        product_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 50),
                        info = c.String(maxLength: 100),
                        price = c.Decimal(nullable: false, storeType: "money"),
                        is_todrink = c.Byte(nullable: false),
                        deleted = c.Int(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.product_id);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        pdetail_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        product_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        igd_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        quan = c.Double(nullable: false),
                        unit_use = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.pdetail_id)
                .ForeignKey("dbo.Ingredient", t => t.igd_id)
                .ForeignKey("dbo.Product", t => t.product_id)
                .Index(t => t.product_id)
                .Index(t => t.igd_id);
            
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        igd_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        warehouse_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100),
                        info = c.String(maxLength: 300),
                        usefor = c.Byte(nullable: false),
                        igd_type = c.String(nullable: false, maxLength: 100),
                        unit_buy = c.String(nullable: false, maxLength: 100),
                        standard_price = c.Decimal(nullable: false, storeType: "money"),
                        deleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.igd_id)
                .ForeignKey("dbo.WareHouse", t => t.warehouse_id)
                .Index(t => t.warehouse_id);
            
            CreateTable(
                "dbo.ReceiptNoteDetails",
                c => new
                    {
                        rn_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        igd_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        quan = c.Int(nullable: false),
                        item_price = c.Decimal(nullable: false, storeType: "money"),
                        note = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => new { t.rn_id, t.igd_id })
                .ForeignKey("dbo.Ingredient", t => t.igd_id)
                .ForeignKey("dbo.ReceiptNote", t => t.rn_id)
                .Index(t => t.rn_id)
                .Index(t => t.igd_id);
            
            CreateTable(
                "dbo.ReceiptNote",
                c => new
                    {
                        rn_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        emp_id = c.String(maxLength: 10, unicode: false),
                        inday = c.DateTime(nullable: false, storeType: "date"),
                        total_amount = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.rn_id)
                .ForeignKey("dbo.Employee", t => t.emp_id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.WareHouse",
                c => new
                    {
                        warehouse_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        contain = c.Double(),
                    })
                .PrimaryKey(t => t.warehouse_id);
            
            CreateTable(
                "dbo.SalaryNote",
                c => new
                    {
                        sn_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        emp_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        date_pay = c.DateTime(storeType: "date"),
                        salary_value = c.Decimal(nullable: false, storeType: "money"),
                        work_hour = c.Double(nullable: false),
                        for_month = c.Int(nullable: false),
                        for_year = c.Int(nullable: false),
                        is_paid = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.sn_id)
                .ForeignKey("dbo.Employee", t => t.emp_id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.WorkingHistory",
                c => new
                    {
                        wh_id = c.String(nullable: false, maxLength: 10, unicode: false),
                        result_salary = c.String(maxLength: 10, unicode: false),
                        emp_id = c.String(maxLength: 10, unicode: false),
                        workday = c.DateTime(storeType: "date"),
                        starthour = c.Int(nullable: false),
                        startminute = c.Int(nullable: false),
                        endhour = c.Int(nullable: false),
                        endminute = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.wh_id)
                .ForeignKey("dbo.Employee", t => t.emp_id)
                .ForeignKey("dbo.SalaryNote", t => t.result_salary)
                .Index(t => t.result_salary)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.ApplicationLog",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date_added = c.DateTime(nullable: false),
                        comment = c.String(nullable: false, storeType: "ntext"),
                        application_name = c.String(maxLength: 100),
                        last_updated_on = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        extra_data = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkingHistory", "result_salary", "dbo.SalaryNote");
            DropForeignKey("dbo.WorkingHistory", "emp_id", "dbo.Employee");
            DropForeignKey("dbo.SalaryNote", "emp_id", "dbo.Employee");
            DropForeignKey("dbo.OrderNoteDetails", "product_id", "dbo.Product");
            DropForeignKey("dbo.ProductDetails", "product_id", "dbo.Product");
            DropForeignKey("dbo.ProductDetails", "igd_id", "dbo.Ingredient");
            DropForeignKey("dbo.Ingredient", "warehouse_id", "dbo.WareHouse");
            DropForeignKey("dbo.ReceiptNoteDetails", "rn_id", "dbo.ReceiptNote");
            DropForeignKey("dbo.ReceiptNote", "emp_id", "dbo.Employee");
            DropForeignKey("dbo.ReceiptNoteDetails", "igd_id", "dbo.Ingredient");
            DropForeignKey("dbo.OrderNoteDetails", "ordernote_id", "dbo.OrderNote");
            DropForeignKey("dbo.OrderNote", "emp_id", "dbo.Employee");
            DropForeignKey("dbo.OrderNote", "cus_id", "dbo.Customer");
            DropForeignKey("dbo.Employee", "manager", "dbo.AdminRes");
            DropIndex("dbo.WorkingHistory", new[] { "emp_id" });
            DropIndex("dbo.WorkingHistory", new[] { "result_salary" });
            DropIndex("dbo.SalaryNote", new[] { "emp_id" });
            DropIndex("dbo.ReceiptNote", new[] { "emp_id" });
            DropIndex("dbo.ReceiptNoteDetails", new[] { "igd_id" });
            DropIndex("dbo.ReceiptNoteDetails", new[] { "rn_id" });
            DropIndex("dbo.Ingredient", new[] { "warehouse_id" });
            DropIndex("dbo.ProductDetails", new[] { "igd_id" });
            DropIndex("dbo.ProductDetails", new[] { "product_id" });
            DropIndex("dbo.OrderNoteDetails", new[] { "product_id" });
            DropIndex("dbo.OrderNoteDetails", new[] { "ordernote_id" });
            DropIndex("dbo.OrderNote", new[] { "emp_id" });
            DropIndex("dbo.OrderNote", new[] { "cus_id" });
            DropIndex("dbo.Employee", new[] { "manager" });
            DropTable("dbo.ApplicationLog");
            DropTable("dbo.WorkingHistory");
            DropTable("dbo.SalaryNote");
            DropTable("dbo.WareHouse");
            DropTable("dbo.ReceiptNote");
            DropTable("dbo.ReceiptNoteDetails");
            DropTable("dbo.Ingredient");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Product");
            DropTable("dbo.OrderNoteDetails");
            DropTable("dbo.Customer");
            DropTable("dbo.OrderNote");
            DropTable("dbo.Employee");
            DropTable("dbo.AdminRes");
        }
    }
}
