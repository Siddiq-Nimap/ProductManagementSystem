namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClassMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "Fruits");
            RenameTable(name: "dbo.Product1", newName: "Vegetables");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Vegetables", newName: "Product1");
            RenameTable(name: "dbo.Fruits", newName: "Products");
        }
    }
}
