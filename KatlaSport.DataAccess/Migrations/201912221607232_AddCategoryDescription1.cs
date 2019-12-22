namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCategoryDescription1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.product_categories", "category_description1", c => c.String(maxLength: 300));
        }

        public override void Down()
        {
            DropColumn("dbo.product_categories", "category_description1");
        }
    }
}
