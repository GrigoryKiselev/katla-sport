namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Name2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.orders", "order_code", c => c.String(nullable: false));
            AddColumn("dbo.orders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.transactions", "IsDeleted", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.transactions", "IsDeleted");
            DropColumn("dbo.orders", "IsDeleted");
            DropColumn("dbo.orders", "order_code");
        }
    }
}
