namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Users_Id", "dbo.Logins");
            DropIndex("dbo.Products", new[] { "Users_Id" });
            RenameColumn(table: "dbo.Products", name: "Users_Id", newName: "UserID");
            AlterColumn("dbo.Products", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "UserID");
            AddForeignKey("dbo.Products", "UserID", "dbo.Logins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UserID", "dbo.Logins");
            DropIndex("dbo.Products", new[] { "UserID" });
            AlterColumn("dbo.Products", "UserID", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "UserID", newName: "Users_Id");
            CreateIndex("dbo.Products", "Users_Id");
            AddForeignKey("dbo.Products", "Users_Id", "dbo.Logins", "Id");
        }
    }
}
