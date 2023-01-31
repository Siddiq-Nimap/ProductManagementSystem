namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserColmn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Users_Id", c => c.Int());
            CreateIndex("dbo.Products", "Users_Id");
            AddForeignKey("dbo.Products", "Users_Id", "dbo.Logins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Users_Id", "dbo.Logins");
            DropIndex("dbo.Products", new[] { "Users_Id" });
            DropColumn("dbo.Products", "Users_Id");
        }
    }
}
