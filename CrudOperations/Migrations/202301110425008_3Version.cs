namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3Version : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductGenericName", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImagePath", c => c.String());
            AlterColumn("dbo.Products", "ProductTitle", c => c.String());
            AlterColumn("dbo.Products", "ProductDescription", c => c.String());
            AlterColumn("dbo.Products", "ProductGenericName", c => c.String());
        }
    }
}
