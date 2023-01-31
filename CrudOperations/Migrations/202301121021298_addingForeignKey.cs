namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CatagoryId_Id", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "CatagoryId_Id" });
            CreateTable(
                "dbo.CatagoryLists",
                c => new
                    {
                        PId = c.Int(nullable: false),
                        CId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PId, t.CId })
                .ForeignKey("dbo.Catagories", t => t.CId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.PId, cascadeDelete: true)
                .Index(t => t.PId)
                .Index(t => t.CId);
            
            DropColumn("dbo.Products", "CatagoryId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CatagoryId_Id", c => c.Int());
            DropForeignKey("dbo.CatagoryLists", "PId", "dbo.Products");
            DropForeignKey("dbo.CatagoryLists", "CId", "dbo.Catagories");
            DropIndex("dbo.CatagoryLists", new[] { "CId" });
            DropIndex("dbo.CatagoryLists", new[] { "PId" });
            DropTable("dbo.CatagoryLists");
            CreateIndex("dbo.Products", "CatagoryId_Id");
            AddForeignKey("dbo.Products", "CatagoryId_Id", "dbo.Catagories", "Id");
        }
    }
}
