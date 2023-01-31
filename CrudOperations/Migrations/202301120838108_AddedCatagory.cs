namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCatagory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Fruits", newName: "Products");
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatagoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "CatagoryId_Id", c => c.Int());
            CreateIndex("dbo.Products", "CatagoryId_Id");
            AddForeignKey("dbo.Products", "CatagoryId_Id", "dbo.Catagories", "Id");
            DropTable("dbo.Vegetables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vegetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGenericName = c.String(),
                        ProductDescription = c.String(),
                        ProductTitle = c.String(),
                        ProductWeight = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Products", "CatagoryId_Id", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "CatagoryId_Id" });
            DropColumn("dbo.Products", "CatagoryId_Id");
            DropTable("dbo.Catagories");
            RenameTable(name: "dbo.Products", newName: "Fruits");
        }
    }
}
