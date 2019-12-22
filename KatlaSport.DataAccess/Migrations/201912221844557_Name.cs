namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Name : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        updated = c.DateTime(nullable: false),
                        transaction_id = c.Int(nullable: false),
                        order_price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.order_id);

            CreateTable(
                "dbo.transactions",
                c => new
                    {
                        transaction_id = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        payment_date = c.DateTime(nullable: false),
                        total_summ = c.Int(nullable: false),
                        StoreOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.transaction_id)
                .ForeignKey("dbo.orders", t => t.StoreOrderId, cascadeDelete: true)
                .Index(t => t.StoreOrderId);

            DropColumn("dbo.product_categories", "category_description1");
        }

        public override void Down()
        {
            AddColumn("dbo.product_categories", "category_description1", c => c.String(maxLength: 300));
            DropForeignKey("dbo.transactions", "StoreOrderId", "dbo.orders");
            DropIndex("dbo.transactions", new[] { "StoreOrderId" });
            DropTable("dbo.transactions");
            DropTable("dbo.orders");
        }
    }
}
